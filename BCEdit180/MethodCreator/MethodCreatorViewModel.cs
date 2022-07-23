using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.MethodCreator {
    public class MethodCreatorViewModel : BaseViewModel {
        public ICommand AddNewReturnTypeCommand { get; }
    }
}
