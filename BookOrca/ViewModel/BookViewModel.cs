using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BookOrca.Core;
using BookOrca.Models;

namespace BookOrca.ViewModel
{

	public class BookViewModel : ViewModelBase
	{
		#region Properties

		public ImageSource CoverSource { get; set; }
		public Book Book { get; set; }

		#endregion
		#region Commands

		public RelayCommand OpenFolerCommand { get; }
    
		public RelayCommand DeleteBookCommand { get; }
    
		public RelayCommand EditBookCommand { get; }

		#endregion
    
    
		public BookViewModel(Book book)
		{
			this.Book = book;
			this.CoverSource = new BitmapImage(this.Book.CoverPath!);
			Debug.WriteLine(this.Book.CoverPath.AbsolutePath);

			OpenFolerCommand = new RelayCommand(OpenFolder);
			DeleteBookCommand = new RelayCommand(DeleteBook);
			EditBookCommand = new RelayCommand(EditBook);

			
		}
    
		private void DeleteBook()
		{
			throw new System.NotImplementedException();
		}

		private void OpenFolder(object? parameter)
		{
			if (parameter is Book buch) Process.Start("explorer.exe", buch.Path);
		}
    
		private void EditBook()
		{
			throw new System.NotImplementedException();
		}
	}
}
