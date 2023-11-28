using System.Windows;
using BookOrca.ViewModel;
using MahApps.Metro.Controls;

namespace BookOrca.View;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenSettings(object sender, RoutedEventArgs e)
    {
        new SettingsWindow
        {
            DataContext = new SettingsViewModel()
        }.ShowDialog();
    }
}