using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using BookOrca.ApiAccess;
using BookOrca.Core;
using BookOrca.Core.Dispatch;
using BookOrca.DataAccess;
using BookOrca.Resources;

namespace BookOrca.ViewModel;

public class MainViewModel : ViewModelBase
{
    private MainViewModel()
    {
        DropFileCommand = new RelayCommand(ReadFile);
        UpdateBooksCommand = new RelayCommand(UpdateBooks);
    }

    #region Command implementation

    private void ReadFile(object? filePath)
    {
        var paths = filePath as string[];
        if (paths == null) return;

        foreach (var path in paths)
        {
	        var newPath = Paths.GetBookPath(Path.GetFileName(path));

	        if (File.Exists(newPath))
	        {
                return;
	        }

			File.Copy(path,newPath , true);
        }
    }

    private void UpdateBooks()
    {
        BookList.Clear();

        var bookDataAccess = IBookDataAccess.Instance;

        var bookPaths = bookDataAccess.GetBookPaths();

        foreach (var bookPath in bookPaths)
        {
            var fileName = Path.GetFileName(bookPath);
            
            var metadataPath = Paths.GetMetadataPath(fileName);

            if (File.Exists(metadataPath))
                try
                {
                    var loadedBook = bookDataAccess.LoadBook(fileName);

                    BookList.Add(loadedBook);
                    
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
                
                    var bookResult = await IBookApi.Instance.GetBookInformation(bookQueryName);
                
                    if (!bookResult.IsSuccessful)
                    {
                        Debug.WriteLine("-----");
                        Debug.WriteLine($"API couldn't find a matching book for {bookQueryName}");
                    
                        if (string.IsNullOrWhiteSpace(bookResult.ErrorMessage))
                        {
                            Debug.WriteLine("No error Message.");
                        }
                    
                        Debug.Write(bookResult.ErrorMessage!);
                        Debug.WriteLine("-----");
                        return;
                    }

                    var book = bookResult.Book!;
                
                    Debug.WriteLine($"API found a matching book for {bookPath}: {book.Title}");
                
                    book.FileName = Path.GetFileName(bookPath);

                    if (string.IsNullOrWhiteSpace(book.CoverUrl))
                    {
                        await bookDataAccess.DownloadBookCover(book);
                    }
                    else
                    {
                        Debug.WriteLine($"Couldn't find an image for {bookQueryName}");
                    }
                
                    bookDataAccess.SaveBook(book);

                    await IDispatcher.Instance.BeginInvoke(() => { BookList.Add(book); });
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Failed to load {fileName}: {e.Message}");
                    throw;
                }
            });
        }
    }

    #endregion

    #region Singleton

    private static MainViewModel? instance;

    public static MainViewModel Instance => instance ??= new MainViewModel();

    #endregion

    #region Properties

    public ObservableCollection<BookViewModel> BookList { get; set; } = new();

    public string BookFilterName { get; set; } = string.Empty;

    #endregion

    #region Commands

    public RelayCommand UpdateBooksCommand { get; }

    public RelayCommand DropFileCommand { get; set; }

    #endregion
}