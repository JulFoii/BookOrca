using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using BookOrca.Core;
using BookOrca.Models;

namespace BookOrca.ViewModel;

public class MainViewModel : ViewModelBase
{
    #region Properties

    public ObservableCollection<Book> Bücherliste { get; set; }
    public Book Buch { get; set; }

    #endregion
    
    
    #region Commands

    public RelayCommand OpenFolerCommand { get; }
    
    public RelayCommand DeleteBookCommand { get; }
    
    public RelayCommand EditBookCommand { get; }

    #endregion
    
    
    public MainViewModel()
    {
        OpenFolerCommand = new RelayCommand(OpenFolder);
        DeleteBookCommand = new RelayCommand(DeleteBook);
        EditBookCommand = new RelayCommand(EditBook);

        Bücherliste = new ObservableCollection<Book>();
        Buch = new Book();

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