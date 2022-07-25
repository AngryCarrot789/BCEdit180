using System.Collections.Generic;

namespace BCEdit180.Core.Utils {
    public interface IMultiSelector<T> {
        IEnumerable<T> SelectedItems { get; }
    }
}
