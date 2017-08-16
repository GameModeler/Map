using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Map.Graphics
{
    internal class Placeholder : Button
    {
        #region Dependency properties

        /// <summary>
        /// Mouse enter command dependency property
        /// </summary>
        public static readonly DependencyProperty MouseEnterCommandProperty =
            DependencyProperty.RegisterAttached("MouseEnter", typeof(ICommand), typeof(Placeholder),
                new PropertyMetadata(null, MouseEnterPropertyChanged));

        public static ICommand GetMouseEnterCommand(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(MouseEnterCommandProperty);
        }

        public static void SetMouseEnterCommand(DependencyObject obj, ICommand command)
        {
            obj.SetValue(MouseEnterCommandProperty, command);
        }

        #endregion

        private bool _isActive;

        #region Properties

        /// <summary>
        /// Placeholder's position
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Placeholder's state
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;

                if (value)
                {
                    var grid = Utilities.FindParent<GridLayer>(this);

                    if (grid == null)
                    {
                        return;
                    }

                    BorderThickness = new Thickness(grid.ValidPositionStrokeThickness);
                    BorderBrush = grid.ValidPositionStrokeColor;
                    Background = grid.ValidPositionBackground;
                }
                else
                {
                    BorderThickness = new Thickness(0);
                    Background = Brushes.Transparent;
                    BorderBrush = Brushes.Transparent;
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Placeholder()
        {
            BorderThickness = new Thickness(0);
            Background = Brushes.Transparent;
            BorderBrush = Brushes.Transparent;
            IsActive = false;
        }

        /// <summary>
        /// Construct a placeholder with a position
        /// </summary>
        /// <param name="position">Position of the placeholder</param>
        public Placeholder(Point position)
            : this()
        {
            Position = position;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Callback method called when the mouse enter command dependency property changes
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <param name="e">Dependency changes arguments</param>
        private static void MouseEnterPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = obj as UIElement;

            if (uiElement != null)
            {
                uiElement.MouseEnter += MouseEnterHandler;
            }
        }

        /// <summary>
        /// Handler of the mouse enter event
        /// </summary>
        /// <param name="sender">Element calling the handler</param>
        /// <param name="e">Mouse event arguments</param>
        private static void MouseEnterHandler(object sender, MouseEventArgs e)
        {
            var uiElement = sender as UIElement;

            if (uiElement != null)
            {
                GetMouseEnterCommand(uiElement).Execute(uiElement);
            }
        }

        #endregion
    }
}
