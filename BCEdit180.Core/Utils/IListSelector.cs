using System.Collections.Generic;

namespace BCEdit180.Core.Utils {
    public interface IListSelector<T> {
        IEnumerable<T> SelectedItems { get; }

        void BringIntoView(T value);

        void ScrollToSelectedItem();
    }
}
