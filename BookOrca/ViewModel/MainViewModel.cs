using System.Collections.ObjectModel;
using System.IO;
using BookOrca.Core;
using BookOrca.Models;
using BookOrca.Resources;

namespace BookOrca.ViewModel;

public class MainViewModel : ViewModelBase
{
    private static MainViewModel? instance;

    public static MainViewModel Instance => instance ??= new MainViewModel();

    public RelayCommand DropFileCommand { get; set; }

    public ObservableCollection<BookViewModel> BookList { get; set; } = new();
    public RelayCommand UpdateBooks { get; }

    public MainViewModel()
	{
		DropFileCommand = new RelayCommand(ReadFile);
    }

	private void ReadFile(object filePath)
	{
		var paths = filePath as string[];
		if (paths == null)
		{
			return;
		}

		foreach (var path in paths)
		{
			File.Copy(path, Paths.GetBookPath(Path.GetFileName(path)));
		}

		
	}
   
}