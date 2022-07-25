using System.Windows;

namespace BCEdit180.Windows {
    public class WindowBase : Window {
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