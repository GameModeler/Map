using System.Windows;
using System.Windows.Controls;

namespace Map.UserControls
{
    public class BaseUserControl : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached("Position",
            typeof(Point), typeof(BaseUserControl), new PropertyMetadata(default(Point), PositionPropertyChanged));

        /// <summary>
        /// User control's position
        /// </summary>
        public Point Position
        {
            get => (Point) GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Callback method called when the position dependency property changes
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <param name="e">Dependency changes arguments</param>
        private static void PositionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var userControl = obj as BaseUserControl;
            var oldValue = e.OldValue as Point?;
            var newValue = e.NewValue as Point?;

            if (userControl == null || oldValue == null || newValue == null || oldValue == newValue)
            {
                return;
            }

            Grid.SetRow(userControl, (int) ((Point) newValue).Y - 1);
            Grid.SetColumn(userControl, (int) ((Point) newValue).X - 1);
        }

        #endregion
    }
}
