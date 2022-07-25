namespace BCEdit180.Core.Messaging {
    /// <summary>
    /// A class that can receive messages. This is the base class, without a receiver method, and shouldn't be used
    /// </summary>
    public interface IMessageReceiver {

    }

    /// <summary>
    /// A class that can receive a message
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IMessageReceiver<in TMessage> : IMessageReceiver {
        void HandleMessage(TMessage message);
    }
}