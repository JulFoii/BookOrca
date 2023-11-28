using BookOrca.Core;
using BookOrca.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace BookOrca.ViewModel;

public class MainViewModel : ViewModelBase
{
	public ObservableCollection<BookViewModel> BookList { get; set; } = new();

	public MainViewModel()
	{
		var book = new Book()
		{
			CoverPath = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books/metadata/images/Best Loser Wins.pdf.png"), UriKind.Absolute)
		};

		var book2 = new Book()
		{
			CoverPath = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books/metadata/images/Best Loser Wins.pdf.png"), UriKind.Absolute)
		};

		BookList.Add(new BookViewModel(book));
		BookList.Add(new BookViewModel(book2));

	}
}