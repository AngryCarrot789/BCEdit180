using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace BCEdit180.Windows {
    public class WindowBase : Window {
        public static readonly DependencyProperty TitlebarColourProperty = 
            DependencyProperty.Register(
                "TitlebarColour",
                typeof(Brush),
                typeof(WindowBase),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Gray)));

        [Category("Brush")]
        public Brush TitlebarColour {
            get => (Brush) GetValue(TitlebarColourProperty);
            set => SetValue(TitlebarColourProperty, value);
        }

        protected WindowBase() {
            // TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
        }

        // protected override void OnPreviewKeyDown(KeyEventArgs e) {
        //     base.OnPreviewKeyDown(e);
        //     if (e.Key == Key.Escape) {
        //         this.DialogResult = false;
        //         Close();
        //     }
        // }
    }
}