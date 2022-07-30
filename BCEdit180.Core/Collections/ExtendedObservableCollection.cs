using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BCEdit180.Core.Collections {
    public class ExtendedObservableCollection<T> : ObservableCollection<T> {
        public ExtendedObservableCollection() {
        }

        public ExtendedObservableCollection(IEnumerable<T> collection) : base(collection) {
        }

        public ExtendedObservableCollection(List<T> list) : base(list) {
        }

        public void AddRange(IEnumerable<T> collection) {
            foreach (T item in collection) {
                this.Add(item);
            }
            // CheckReentrancy();
            // foreach (T item in collection) {
            //     this.Items.Add(item);
            // }
            // 
            // OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void InsertRange(int index, IEnumerable<T> collection) {
            // CheckReentrancy();

            int i = index;
            foreach (T item in collection) {
                this.Insert(i++, item);
            }

            // OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void RemoveRange(IEnumerable<T> collection) {
            // CheckReentrancy();
            foreach (T item in collection) {
                this.Remove(item);
            }

            // OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}