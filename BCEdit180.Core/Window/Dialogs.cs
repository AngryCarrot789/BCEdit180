namespace BCEdit180.Core.Dialogs {
    public static class Dialog {
        public static IDialogManager Message => ServiceManager.GetService<IDialogManager>();

        public static ITypeEditors TypeEditor => ServiceManager.GetService<ITypeEditors>();

        public static IAccessEditor AccessEditor => ServiceManager.GetService<IAccessEditor>();

        public static IFileDialog File => ServiceManager.GetService<IFileDialog>();
    }
}