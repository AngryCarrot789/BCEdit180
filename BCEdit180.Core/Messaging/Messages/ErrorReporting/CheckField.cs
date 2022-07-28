using BCEdit180.Core.ViewModels;

namespace BCEdit180.Core.Messaging.Messages.ErrorReporting {
    public class CheckField {
        public FieldInfoViewModel Field { get; }

        public CheckField(FieldInfoViewModel field) {
            this.Field = field;
        }
    }
}