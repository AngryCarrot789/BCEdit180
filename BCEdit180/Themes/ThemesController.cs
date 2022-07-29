using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using REghZy.MVVM.Commands;

namespace BCEdit180.Themes {
    public static class ThemesController {
        public static ICommand SetThemeCommand { get; }

        public static ThemeType CurrentTheme { get; set; }

        private static ResourceDictionary ThemeDictionary {
            get => Application.Current.Resources.MergedDictionaries[0];
            set => Application.Current.Resources.MergedDictionaries[0] = value;
        }

        private static ResourceDictionary ControlColours {
            get => Application.Current.Resources.MergedDictionaries[1];
            set => Application.Current.Resources.MergedDictionaries[1] = value;
        }

        private static ResourceDictionary Controls {
            get => Application.Current.Resources.MergedDictionaries[2];
            set => Application.Current.Resources.MergedDictionaries[2] = value;
        }

        static ThemesController() {
            SetThemeCommand = new RelayCommandParam<string>((p)=> {
                if (p == "Dark") {
                    SetTheme(ThemeType.Dark);
                }
                else if (p == "Red") {
                    SetTheme(ThemeType.Red);
                }
                else {
                    SetTheme(ThemeType.Light);
                }
            });
        }

        public static void SetTheme(ThemeType theme) {
            string themeName = theme.GetName();
            CurrentTheme = theme;
            if (string.IsNullOrEmpty(themeName)) {
                return;
            }

            ThemeDictionary = new ResourceDictionary() { Source = new Uri($"Themes/{themeName}.xaml", UriKind.Relative) };
            ControlColours = new ResourceDictionary() { Source = new Uri("Themes/ControlColours.xaml", UriKind.Relative) };
            Controls = new ResourceDictionary() { Source = new Uri("Themes/Controls.xaml", UriKind.Relative) };
        }

        public static object GetResource(object key) {
            return ThemeDictionary[key];
        }

        public static SolidColorBrush GetBrush(string name) {
            return GetResource(name) is SolidColorBrush brush ? brush : new SolidColorBrush(Colors.White);
        }
    }
}