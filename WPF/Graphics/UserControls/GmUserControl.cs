using System.Windows;
using System.Windows.Controls;
using Point = System.Drawing.Point;

namespace Map.WPF.Graphics.UserControls
{
    public class GmUserControl : UserControl
    {
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.RegisterAttached("Position", typeof(Point?), typeof(GmUserControl),
                new PropertyMetadata(null, PositionChanged));

        public Point? Position
        {
            get => (Point?) GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        private static void PositionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var gameObj = obj as GmUserControl;
            var oldValue = e.OldValue as Point?;
            var newValue = e.NewValue as Point?;

            if (gameObj == null || oldValue == null || newValue == null || oldValue == newValue)
            {
                return;
            }

            Grid.SetColumn(gameObj, ((Point) newValue).Y);
            Grid.SetRow(gameObj, ((Point) newValue).X);
        }
    }
}
