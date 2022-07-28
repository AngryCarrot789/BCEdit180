using BCEdit180.Core.ViewModels;

namespace BCEdit180.Core.Messaging {
    public class CheckField {
        public FieldInfoViewModel Field { get; }

        public CheckField(FieldInfoViewModel field) {
            this.Field = field;
        }
    }
}