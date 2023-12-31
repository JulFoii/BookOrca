﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BookOrca.Core;
using BookOrca.Core.Dispatch;
using BookOrca.Models;
using BookOrca.Resources;

namespace BookOrca.ViewModel;

public class MainViewModel : ViewModelBase
{
    private MainViewModel()
    {
        DropFileCommand = new RelayCommand(ReadFile);
        UpdateBooksCommand = new RelayCommand(UpdateBooks);
        OpenFolderCommand = new RelayCommand(OpenFolder);
    }

    #region Command implementation

    private static void ReadFile(object? filePath)
    {
        if (filePath is not string[] paths) return;

        foreach (var path in paths)
        {
            var newPath = Paths.GetBookPath(Path.GetFileName(path));

            if (File.Exists(newPath)) return;

            File.Copy(path, newPath, true);
        }
    }

    private void UpdateBooks()
    {
        BookList.Clear();
        BackUpBookList.Clear();

        try
        {
            LoadBooks();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            MessageBox.Show(e.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            BookList.AddRange(BackUpBookList);
        }
    }

    private void LoadBooks()
    {
        var bookDataAccess = Singletons.BookDataAccess;

        var bookPaths = bookDataAccess.GetBookPaths();

        foreach (var bookPath in bookPaths)
        {
            var fileName = Path.GetFileName(bookPath);

            var metadataPath = Paths.GetMetadataPath(fileName);

            if (File.Exists(metadataPath))
                try
                {
                    var loadedBook = bookDataAccess.LoadBook(fileName);

                    BackUpBookList.Add(loadedBook);

                    Debug.WriteLine($"Loaded book {fileName}");
                    continue;
                }
                catch (Exception e)
                {
                    File.Delete(metadataPath);
                    File.Delete(Paths.GetImagePath(fileName));
                    Debug.WriteLine(e);
                }

            Task.Run(async () =>
            {
                try
                {
                    var bookQueryName = Path.GetFileNameWithoutExtension(bookPath);

                    var bookResult = await Singletons.BookApi.GetBookInformation(bookQueryName);

                    if (!bookResult.IsSuccessful)
                    {
                        Debug.WriteLine("-----");
                        Debug.WriteLine($"API couldn't find a matching book for {bookQueryName}");

                        if (string.IsNullOrWhiteSpace(bookResult.ErrorMessage))
                            Debug.WriteLine("No error Message.");

                        Debug.Write(bookResult.ErrorMessage!);
                        Debug.WriteLine("-----");

                        var emptyBook = new Book
                        {
                            FileName = fileName
                        };

                        await IDispatcher.Instance.BeginInvoke(() =>
                        {
                            BackUpBookList.Add(emptyBook);
                            BookList.Add(emptyBook);
                        });

                        return;
                    }

                    var book = bookResult.Book!;

                    Debug.WriteLine($"API found a matching book for {bookPath}: {book.Title}");

                    book.FileName = Path.GetFileName(bookPath);

                    if (string.IsNullOrWhiteSpace(book.CoverUrl))
                    {
                        Debug.WriteLine($"Couldn't find an image for {bookQueryName}");
                    }
                    else
                    {
                        Debug.WriteLine($"Started download of {book.CoverUrl}");
                        try
                        {
                            await bookDataAccess.DownloadBookCover(book);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            throw;
                        }

                        Debug.WriteLine($"Finished download of {book.CoverUrl}");
                    }


                    bookDataAccess.SaveBook(book);

                    await IDispatcher.Instance.BeginInvoke(() =>
                    {
                        BackUpBookList.Add(book);
                        BookList.Add(book);
                    });
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Failed to load {fileName}: {e.Message}");
                    throw;
                }
            });
        }
    }

    private void OpenFolder()
    {
        Process.Start(new ProcessStartInfo(Paths.RelativeToAbsolutePath(Paths.BookPath))
        {
            UseShellExecute = true
        });
    }

    #endregion

    #region Singleton

    private static MainViewModel? instance;

    public static MainViewModel Instance => instance ??= new MainViewModel();

    #endregion

    #region Properties

    public ObservableCollection<BookViewModel> BookList { get; } = new();

    public ObservableCollection<BookViewModel> BackUpBookList { get; } = new();

    private string bookfilter = string.Empty;

    public string BookFilter
    {
        get => bookfilter;
        set
        {
            if (value == string.Empty)
            {
                BookList.Clear();
                BookList.AddRange(BackUpBookList);
                bookfilter = value;
            }

            bookfilter = value;
            var newBookList = BackUpBookList.Where(x =>
                x.Book.Title.Contains(value, StringComparison.OrdinalIgnoreCase)
                || x.Book.FileName.Contains(value, StringComparison.OrdinalIgnoreCase)
                || x.Book.Author.Contains(value, StringComparison.OrdinalIgnoreCase)
                || x.Book.Isbn.Contains(value, StringComparison.OrdinalIgnoreCase));

            BookList.Clear();
            foreach (var newBook in newBookList) BookList.Add(newBook);
        }
    }

    #endregion

    #region Commands

    public RelayCommand UpdateBooksCommand { get; }
    public RelayCommand DropFileCommand { get; set; }
    public RelayCommand OpenFolderCommand { get; }

    #endregion
}