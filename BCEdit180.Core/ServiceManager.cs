using System;
using System.Collections.Generic;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core {
    public class ServiceManager {
        public static ServiceManager Instance { get; } = new ServiceManager();

        private readonly Dictionary<Type, object> serviceMap;

        public ServiceManager() {
            this.serviceMap = new Dictionary<Type, object>();
        }

        public static T GetViewModel<T>() where T : BaseViewModel {
            return Instance.GetViewModel0<T>();
        }

        public static T GetService<T>() {
            return Instance.GetService0<T>();
        }

        public static void SetViewModel<T>(T viewModel) where T : BaseViewModel {
            Instance.SetViewModel0(viewModel);
        }

        public static void SetService<T>(T value) {
            Instance.SetService0(value);
        }

        public T GetViewModel0<T>() where T : BaseViewModel {
            return this.serviceMap.TryGetValue(typeof(T), out object value) && value is T ? (T) value : throw new Exception("ViewModel not found: " + typeof(T).Name);
        }

        public T GetService0<T>() {
            return this.serviceMap.TryGetValue(typeof(T), out object value) && value is T ? (T) value : throw new Exception("Service not found: " + typeof(T).Name);
        }

        public void SetViewModel0<T>(T value) where T : BaseViewModel{
            if (value == null) {
                throw new ArgumentNullException(nameof(value), "Service object cannot be null");
            }

            this.serviceMap[typeof(T)] = value;
        }

        public void SetService0<T>(T value) {
            if (value == null) {
                throw new ArgumentNullException(nameof(value), "Service object cannot be null");
            }

            this.serviceMap[typeof(T)] = value;
        }
    }
}