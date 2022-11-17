namespace BCEdit180.Core.Window {
    public interface IDialog {
        bool DialogResult { get; set; }

        void Close();
    }
}