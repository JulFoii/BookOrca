using System.Collections.ObjectModel;
using BookOrca.Core;
using BookOrca.Models;

namespace BookOrca.ViewModel;

public class MainViewModel : ViewModelBase
{
    private static MainViewModel? instance;

    public static MainViewModel Instance => instance ??= new MainViewModel();

    public ObservableCollection<BookViewModel> BookList { get; set; } = new();

	public MainViewModel()
    {
        var book = new Book
        {
            CoverPath = "books/metadata/images/Best Loser Wins.pdf.png"
        };

        var book2 = new Book
        {
            CoverPath = "books/metadata/images/Best Loser Wins.pdf.png"
        };

        BookList.Add(new BookViewModel(book));
        BookList.Add(new BookViewModel(book2));
    }

   
}