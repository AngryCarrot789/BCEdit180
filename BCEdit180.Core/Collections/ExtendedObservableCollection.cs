using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;

namespace BCEdit180.Core.Collections {
    public class ExtendedObservableCollection<T> : ObservableCollection<T> {
        public ExtendedObservableCollection() {
        }

        public ExtendedObservableCollection(IEnumerable<T> collection) : base(collection) {
        }

        public ExtendedObservableCollection(List<T> list) : base(list) {
        }

        public void AddRange(IEnumerable<T> collection) {
            CheckReentrancy();
            foreach (T item in collection) {
                this.Items.Add(item);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void RemoveRange(IEnumerable<T> collection) {
            CheckReentrancy();
            foreach (T item in collection) {
                this.Items.Remove(item);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}