using System.Windows;
using System.Windows.Media;
using BookOrca.ViewModel;
using ControlzEx.Theming;
using MahApps.Metro.Controls;

namespace BookOrca.View;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
	private MainViewModel viewModel;

    public MainWindow()
    {
        InitializeComponent();
        viewModel = (MainViewModel)DataContext;
    }

    private void OpenSettings(object sender, RoutedEventArgs e)
    {
        new SettingsWindow
        {
            DataContext = new SettingsViewModel()
        }.ShowDialog();
    }

    private void UIElement_OnDropFile(object sender, DragEventArgs e)
    {
	    if (e.Data.GetDataPresent(DataFormats.FileDrop))
	    {
		    var droppedFile = e.Data.GetData(DataFormats.FileDrop);

		    // Hier rufst du das Command im MainViewModel auf
		    viewModel.DropFileCommand.Execute(droppedFile);
	    }

	}

}
