using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing {
    public class TryCatchBlockViewModel : BaseViewModel {
        private long startIndex;
        private long endIndex;
        private long handlerIndex;
        private string handlerType;

        public TryCatchNode Node { get; set; }

        public long StartIndex {
            get => this.startIndex;
            set => RaisePropertyChanged(ref this.startIndex, value);
        }

        public long EndIndex {
            get => this.endIndex;
            set => RaisePropertyChanged(ref this.endIndex, value);
        }

        public long HandlerIndex {
            get => this.handlerIndex;
            set => RaisePropertyChanged(ref this.handlerIndex, value);
        }

        public string HandlerType {
            get => this.handlerType;
            set => RaisePropertyChanged(ref this.handlerType, value);
        }

        public TryCatchBlockViewModel(TryCatchNode node) {
            this.Node = node;
            Load(node);
        }

        public void Load(TryCatchNode node) {
            this.StartIndex = node.Start.Index;
            this.EndIndex = node.End.Index;
            this.HandlerIndex = node.Handler.Index;
            this.HandlerType = node.ExceptionClassName.Name;
        }

        public void Save(TryCatchNode node) {
            node.ExceptionClassName = new ClassName(this.HandlerType);
            // node.Start.Index = this.StartIndex;
            // node.End.Index = this.EndIndex;
            // node.Handler.Index = this.HandlerIndex;
            // node.ExceptionClassName.Name = this.HandlerType;
        }

        public TryCatchNode SaveAndGetNode() {
            Save(this.Node);
            return this.Node;
        }
    }
}
