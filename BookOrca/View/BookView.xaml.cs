using System.Windows;
using System.Windows.Controls;
using BookOrca.ViewModel;

namespace BookOrca.View;

public partial class BookView : UserControl
{
    public BookView()
    {
        InitializeComponent();
    }

    private void BookContextMenuDelete(object sender, RoutedEventArgs e)
    {
        var bookViewModel = (BookViewModel)DataContext;
        
        bookViewModel.DeleteBookCommand.Execute();
    }
}