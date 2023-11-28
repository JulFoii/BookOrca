using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using BookOrca.Core;
using ControlzEx.Theming;

namespace BookOrca.ViewModel;

public class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel()
    {
        SaveCommand = new RelayCommand(() =>
        {
            ThemeManager.Current.ChangeTheme(Application.Current, $"{SelectedTheme}.{SelectedColor}");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Theme"].Value = SelectedTheme;
            config.AppSettings.Settings["Color"].Value = SelectedColor;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        });

        CancelCommand = new RelayCommand(obj =>
        {
            var closeableObject = (dynamic)obj!;
            closeableObject.Close();
        });
    }

    public ReadOnlyObservableCollection<string> Themes { get; } = ThemeManager.Current.BaseColors;
    public string SelectedTheme { get; set; } = ConfigurationManager.AppSettings["Theme"] ?? "Light";
    public ReadOnlyObservableCollection<string> Colors { get; } = ThemeManager.Current.ColorSchemes;
    public string SelectedColor { get; set; } = ConfigurationManager.AppSettings["Color"] ?? "Blue";
    public RelayCommand SaveCommand { get; }
    public RelayCommand CancelCommand { get; }
}