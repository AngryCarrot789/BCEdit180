namespace BCEdit180.Core.Dialogs {
    public static class Dialog {
        public static IDialogManager DD => Services.GetService<IDialogManager>();
        public static ITypeEditors TypeEditor => Services.GetService<ITypeEditors>();

    }
}