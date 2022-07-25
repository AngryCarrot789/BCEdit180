namespace BCEdit180.Core.Messaging.Messages {
    public class BusyStateMessage {
        public bool IsBusy { get; set; }

        public BusyStateMessage(bool isBusy = true) {
            this.IsBusy = isBusy;
        }
    }
}