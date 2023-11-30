using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
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

        foreach (var path in paths) File.Copy(path, Paths.GetBookPath(Path.GetFileName(path)));
    }

    private void UpdateBooks()
    {
        BookList.Clear();

        var bookDataAccess = IBookDataAccess.Instance;

        var bookPaths = bookDataAccess.GetBookPaths();

        foreach (var bookPath in bookPaths)
        {
            var metadataPath = Paths.GetMetadataPath(bookPath);

            if (File.Exists(metadataPath))
                try
                {
                    var loadedBook = bookDataAccess.LoadBook(bookPath);

                    BookList.Add(loadedBook);
                }
                catch (Exception e)
                {
                    var fileName = Path.GetFileName(bookPath);
                    File.Delete(Paths.GetMetadataPath(fileName));
                    File.Delete(Paths.GetImagePath(fileName));
                    Debug.WriteLine(e);
                }

            Task.Run(async () =>
            {
                var book = await IBookApi.Instance.GetBookInformation(Path.GetFileNameWithoutExtension(bookPath));

                if (book == null)
                {
                    Debug.WriteLine($"API couldn't find a matching book for {bookPath}");
                    return;
                }

                await bookDataAccess.DownloadBookCover(book);

                bookDataAccess.SaveBook(book);

                await IDispatcher.Instance.BeginInvoke(() => { BookList.Add(book); });
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