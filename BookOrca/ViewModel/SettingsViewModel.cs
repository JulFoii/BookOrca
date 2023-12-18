using System.Collections.ObjectModel;
using System.Windows;
using BookOrca.Core;
using BookOrca.DataAccess;
using ControlzEx.Theming;

namespace BookOrca.ViewModel;

public class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel()
    {
        SaveCommand = new RelayCommand(() =>
        {
            ThemeManager.Current.ChangeTheme(Application.Current, $"{SelectedTheme}.{SelectedColor}");

            var config = ConfigurationManager.LoadConfiguration();


            config["Theme"] = SelectedTheme;
            config["Color"] = SelectedColor;

            ConfigurationManager.WriteConfiguration(config);
        });
    }

    public ReadOnlyObservableCollection<string> Themes { get; } = ThemeManager.Current.BaseColors;
    public string SelectedTheme { get; set; } = ConfigurationManager.LoadConfiguration()["Theme"];
    public ReadOnlyObservableCollection<string> Colors { get; } = ThemeManager.Current.ColorSchemes;
    public string SelectedColor { get; set; } = ConfigurationManager.LoadConfiguration()["Color"];
    public RelayCommand SaveCommand { get; }
}