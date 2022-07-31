namespace BCEdit180.Core.Messaging.Messages.ErrorReporting {
    public class AddMessage {
        public string Message { get; set; }

        public AddMessage() {
        }

        public AddMessage(string message) {
            this.Message = message;
        }
    }
}