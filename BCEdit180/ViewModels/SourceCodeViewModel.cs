using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class SourceCodeViewModel : BaseViewModel {
        private string text;
        public string Text {
            get => this.text;
            set => RaisePropertyChanged(ref this.text, value);
        }

        private bool isGenerating;
        public bool IsGenerating {
            get => this.isGenerating;
            set => RaisePropertyChanged(ref this.isGenerating, value);
        }

        public ClassViewModel Class { get; }

        public ICommand GenerateCommand { get; }

        public SourceCodeViewModel(ClassViewModel vm) {
            this.Class = vm;
            this.GenerateCommand = new RelayCommand(GenerateCode);
        }

        public void GenerateCode() {
            this.IsGenerating = true;
            Task.Run(async () => {
                string code = await GenerateCodeAsync();
                this.IsGenerating = false;
                this.Text = code;
            });
        }

        private async Task<string> GenerateCodeAsync() {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }
    }
}
