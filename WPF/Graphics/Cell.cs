using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Point = System.Drawing.Point;

namespace Map.WPF.Graphics
{
    public class Cell : Button
    {
        public static readonly DependencyProperty MouseEnterCommandProperty = DependencyProperty.RegisterAttached("MouseEnter",
            typeof(ICommand), typeof(Cell),
            new PropertyMetadata(null, MouseEnterChanged));

        public Point Position { get; set; }

        public static ICommand GetMouseEnterCommand(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(MouseEnterCommandProperty);
        }

        public static void SetMouseEnterCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseEnterCommandProperty, value);
        }

        private static void MouseEnterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = obj as UIElement;

            if (uiElement != null)
            {
                uiElement.MouseEnter += MouseEnterHandler;
            }
        }

        private static void MouseEnterHandler(object sender, MouseEventArgs e)
        {
            var uiElement = sender as UIElement;

            if (uiElement != null)
            {
                var command = GetMouseEnterCommand(uiElement);
                command.Execute(uiElement);
            }
        }
    }
}
