namespace BCEdit180.Core.Editors {
    public class MethodEditorViewModel : MethodDescEditorViewModel {
        private string methodName;
        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

        public MethodEditorViewModel() : base() {
            this.MethodName = "methodName";
        }
    }
}
