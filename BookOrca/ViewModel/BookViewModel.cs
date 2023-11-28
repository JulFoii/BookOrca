using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using BookOrca.Core;
using BookOrca.Models;

namespace BookOrca.ViewModel
{

	public class BookViewModel : ViewModelBase
	{
		public string? ImagePath { get; set; }

		public BookViewModel(Book book)
		{
			ImagePath = book.CoverPath;
		}
	}
}
