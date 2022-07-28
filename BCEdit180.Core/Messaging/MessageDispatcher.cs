using System;
using System.Collections.Generic;
using System.Reflection;
using BCEdit180.Core.Utils;
using BCEdit180.Core.ViewModels;

namespace BCEdit180.Core.Messaging {
    public static class MessageDispatcher {
        private static readonly Dictionary<Type, List<IMessageReceiver>> ReceiverMap = new Dictionary<Type, List<IMessageReceiver>>();

        public static void RegisterHandler<TMessage>(IMessageReceiver<TMessage> receiver) {
            if (!ReceiverMap.ContainsKey(typeof(TMessage))) {
                ReceiverMap[typeof(TMessage)] = new List<IMessageReceiver>();
            }

            ReceiverMap[typeof(TMessage)].Add(receiver);
        }

        public static void UnregisterHandler<TMessage>(IMessageReceiver<TMessage> receiver) {
            if (ReceiverMap.TryGetValue(typeof(TMessage), out List<IMessageReceiver> receivers)) {
                receivers.Remove(receiver);
            }
        }

        private static IMessageReceiver currentObject;
        public static void Publish<T>(T message) {
            if (ReceiverMap.TryGetValue(typeof(T), out List<IMessageReceiver> list)) {
                foreach (IMessageReceiver receiver in list) {
                    if (currentObject == receiver) {
                        throw new Exception($"Prevented stack overflow exception while publishing event. Handler {receiver.GetType()} receiving {message.GetType()} ({message})");
                    }

                    currentObject = receiver;
                    try {
                        ((IMessageReceiver<T>) receiver).HandleMessage(message);
                    }
                    finally {
                        currentObject = null;
                    }
                }
            }
        }

        public static void PublishUI<T>(T message) {
            AppProxy.Proxy.InvokeSync(() => Publish(message));
        }
    }
}