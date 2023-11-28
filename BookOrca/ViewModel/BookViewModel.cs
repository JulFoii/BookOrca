using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BookOrca.Core;
using BookOrca.Models;

namespace BookOrca.ViewModel;

public class BookViewModel : ViewModelBase
{
    public BookViewModel(Book book)
    {
        Book = book;
        CoverSource =
            new BitmapImage(new Uri(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books/metadata/images/Best Loser Wins.pdf.png"),
                UriKind.Absolute));

        OpenFolerCommand = new RelayCommand(OpenFolder);
        DeleteBookCommand = new RelayCommand(DeleteBook);
    }

    private void DeleteBook()
    {
        MainViewModel.Instance.BookList.Remove(this);
    }

    private void OpenFolder(object? parameter)
    {
        if (parameter is Book buch) Process.Start("explorer.exe", buch.Path);
    }
    
    #region Properties

    public ImageSource CoverSource { get; set; }
    public Book Book { get; set; }

    #endregion

    #region Commands
    public RelayCommand OpenFolerCommand { get; }
    public RelayCommand DeleteBookCommand { get; }

    #endregion
}