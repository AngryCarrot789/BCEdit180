namespace BCEdit180.Core.Window {
    public static class Dialogs {
        public static IDialogManager Message => ServiceManager.GetService<IDialogManager>();

        public static ITypeEditors TypeEditor => ServiceManager.GetService<ITypeEditors>();

        public static IAccessEditor AccessEditor => ServiceManager.GetService<IAccessEditor>();

        public static IFileDialog File => ServiceManager.GetService<IFileDialog>();
    }
}