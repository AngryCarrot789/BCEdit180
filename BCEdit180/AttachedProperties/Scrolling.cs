using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BCEdit180.AttachedProperties {
    public static class Scrolling {
        public static readonly DependencyProperty ScrollHorizontallyWithShiftWheelProperty =
            DependencyProperty.RegisterAttached(
                "ScrollHorizontallyWithShiftWheel",
                typeof(bool),
                typeof(Scrolling),
                new PropertyMetadata(false, UseHorizontalScrollingChangedCallback));

        public static void SetScrollHorizontallyWithShiftWheel(UIElement element, bool value) {
            element.SetValue(ScrollHorizontallyWithShiftWheelProperty, value);
        }

        public static bool GetScrollHorizontallyWithShiftWheel(UIElement element) {
            return (bool) element.GetValue(ScrollHorizontallyWithShiftWheelProperty);
        }

        private static void UseHorizontalScrollingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is UIElement element) {
                element.PreviewMouseWheel -= OnPreviewMouseWheel;
                if ((bool) e.NewValue) {
                    element.PreviewMouseWheel += OnPreviewMouseWheel;
                }
            }
            else {
                throw new Exception("Attached property must be used with UIElement.");
            }
        }

        private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs args) {
            if (Keyboard.Modifiers != ModifierKeys.Shift)
                return;

            ScrollViewer scrollViewer = sender is ScrollViewer ? ((ScrollViewer) sender) : ((UIElement) sender).FindDescendant<ScrollViewer>();
            if (scrollViewer == null) {
                return;
            }

            // by default, windows scrolls 3 times per horizontal shift (there's a way
            // to get the count dynamically using windows forms but i forgot how to)
            if (args.Delta < 0) {
                scrollViewer.LineRight();
                scrollViewer.LineRight();
                scrollViewer.LineRight();
            }
            else {
                scrollViewer.LineLeft();
                scrollViewer.LineLeft();
                scrollViewer.LineLeft();
            }

            args.Handled = true;
        }

        private static T FindDescendant<T>(this DependencyObject obj) where T : DependencyObject {
            if (obj == null) {
                return null;
            }

            int childCount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childCount; i++) {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                T result = child as T ?? FindDescendant<T>(child);

                if (result != null) {
                    return result;
                }
            }

            return null;
        }
    }
}