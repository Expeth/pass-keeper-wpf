using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PassKeeper_WPF
{
    public class Config
    {
        private string _theme;
        private string _language;

        public string Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                ChangeTheme();
            }
        }
        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                ChangeLanguage();
            }
        }

        public Config()
        {
            Theme = "LightTheme";
            Language = "English";
        }

        public Config(string theme, string language)
        {
            Theme = theme;
            Language = language;
        }

        public void Configure()
        {
            ChangeTheme();
            ChangeLanguage();
        }

        public void ChangeLanguage()
        {
            Application.Current.Resources.MergedDictionaries[6].Source = new Uri($@"Languages\{Language}.xaml", UriKind.RelativeOrAbsolute);
        }

        public void ChangeTheme()
        {
            string tm = Theme.Contains("Dark") ? "Dark" : "Light";
            Application.Current.Resources.MergedDictionaries[5].Source = new Uri($@"Style\{Theme}.xaml", UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries[1].Source = new Uri($@"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{tm}.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
