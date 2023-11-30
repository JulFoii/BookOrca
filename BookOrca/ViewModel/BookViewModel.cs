using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BookOrca.Core;
using BookOrca.DataAccess;
using BookOrca.Models;
using BookOrca.Resources;

namespace BookOrca.ViewModel;

public class BookViewModel : ViewModelBase
{
    public BookViewModel(Book book)
    {
        Book = book;
        CoverSource = new BitmapImage(Paths.GetAbsoluteUri(Paths.GetImagePath(book.FileName)));

        DeleteBookCommand = new RelayCommand(DeleteBook);
        OpenBookCommand = new RelayCommand(OpenBook);
    }

    #region Commands

    public RelayCommand OpenBookCommand { get; }
    public RelayCommand DeleteBookCommand { get; }

    #endregion

    #region Command Implementation

    private void OpenBook()
    {
        Process.Start(new ProcessStartInfo(Paths.GetAbsoluteBookPath(Book.FileName))
        {
            UseShellExecute = true
        });
    }

    private void DeleteBook()
    {
        IBookDataAccess.Instance.DeleteBook(Book);
        MainViewModel.Instance.BookList.Remove(this);
    }

    #endregion

    #region Properties

    public ImageSource CoverSource { get; set; }
    public Book Book { get; set; }

    #endregion

    #region Operator

    public static implicit operator Book(BookViewModel bookViewModel)
    {
        return bookViewModel.Book;
    }

    public static implicit operator BookViewModel(Book book)
    {
        var bookViewModel = MainViewModel.Instance.BookList
                                .FirstOrDefault(x => x.Book == book)
                            ?? new BookViewModel(book);

        return bookViewModel;
    }

    #endregion
}