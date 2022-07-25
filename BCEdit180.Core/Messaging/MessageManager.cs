using System;
using System.Collections.Generic;
using BCEdit180.Core.Modals;
using BCEdit180.Core.Utils;

namespace BCEdit180.Core {
    public static class MessageManager {
        private static readonly Dictionary<Type, List<IMessageReceiver>> ReceiverMap = new Dictionary<Type, List<IMessageReceiver>>();

        public static void RegisterHandler<TMessage, TReceiver>(TReceiver receiver) where TReceiver : IMessageReceiver<TMessage> {
            if (!ReceiverMap.ContainsKey(typeof(TMessage))) {
                ReceiverMap[typeof(TMessage)] = new List<IMessageReceiver>();
            }

            ReceiverMap[typeof(TMessage)].Add(receiver);
        }

        public static void Publish<T>(T message) {
            if (ReceiverMap.TryGetValue(typeof(T), out List<IMessageReceiver> list)) {
                foreach (IMessageReceiver receiver in list) {
                    ((IMessageReceiver<T>) receiver).Handle(message);
                }
            }
        }

        public static void PublishUI<T>(T message) {
            AppProxy.Proxy.InvokeSync(() => Publish(message));
        }
    }
}