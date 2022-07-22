using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BCEdit180.Dialogs {
    public static class Dialog {
        public static void ShowDialog(string title, string message) {
            MessageBox.Show(message, title);
        }
    }
}
