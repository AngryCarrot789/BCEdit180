using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BCEdit180.AttachedProperties {
    public static class TextBoxAP {
        public static readonly DependencyProperty ClearOnClickEscProperty =
            DependencyProperty.RegisterAttached(
                "ClearOnClickEsc",
                typeof(bool),
                typeof(TextBox),
                new FrameworkPropertyMetadata(PropertyChangedCallback));

        public static void SetClearOnClickEsc(DependencyObject o, bool value) {
            o.SetValue(ClearOnClickEscProperty, value);
        }

        public static bool GetClearOnClickEsc(DependencyObject o) {
            return (bool) o.GetValue(ClearOnClickEscProperty);
        }

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is TextBox textbox) {
                if (e.NewValue == e.OldValue) {
                    return;
                }

                if ((bool) e.NewValue) {
                    textbox.KeyDown += TextboxOnKeyDown;
                }
                else {
                    textbox.KeyDown -= TextboxOnKeyDown;
                }
            }
        }

        private static void TextboxOnKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                ((TextBox) sender).Text = "";
            }
        }
    }
}