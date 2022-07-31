using System.Windows;
using System.Windows.Controls;

namespace BCEdit180.Controls.Bars {
    public class VerticalButtonBar : ItemsControl {
        // public static readonly DependencyProperty OrientationProperty =
        //     DependencyProperty.Register(
        //         "Orientation",
        //         typeof(Orientation),
        //         typeof(ButtonBar),
        //         new FrameworkPropertyMetadata(
        //             Orientation.Horizontal,
        //             FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
        //             OnOrientationPropertyChangedCallback,
        //             CoerceOrientationCallback));
        // private static object CoerceOrientationCallback(DependencyObject d, object basevalue) {
        //     if (basevalue is Orientation orientation) {
        //         if (orientation == Orientation.Horizontal || orientation == Orientation.Vertical) {
        //             return orientation;
        //         }
        //     }
        //     return Orientation.Horizontal;
        // }
        // private static void OnOrientationPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        //     throw new System.NotImplementedException();
        // }

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(
                "Direction",
                typeof(ExpandDirection),
                typeof(VerticalButtonBar),
                new FrameworkPropertyMetadata(ExpandDirection.Left));

        public ExpandDirection Direction {
            get => (ExpandDirection) GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is VerticalButtonBarItem;

        protected override DependencyObject GetContainerForItemOverride() => new VerticalButtonBarItem();
    }
}