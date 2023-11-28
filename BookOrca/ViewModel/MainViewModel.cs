using System.Collections.ObjectModel;
using BookOrca.Core;
using BookOrca.Models;

namespace BookOrca.ViewModel;

public class MainViewModel : ViewModelBase
{
	public ObservableCollection<BookViewModel> Books { get; set; }

	public MainViewModel()
	{
		Books = new ObservableCollection<BookViewModel>();
		Books.Add(new BookViewModel(new Book()
		{
			CoverPath = "books/metadata/images/Best Loser Wins.pdf.png"
		}));
	}
}