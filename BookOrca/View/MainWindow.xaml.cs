using System.Diagnostics;
using System.Windows;
using BookOrca.ViewModel;
using MahApps.Metro.Controls;

namespace BookOrca.View;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    private readonly MainViewModel viewModel;

    public MainWindow()
    {
        InitializeComponent();
        viewModel = MainViewModel.Instance;
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

    private void BookContextMenuDelete(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine(sender.GetType());
    }
}