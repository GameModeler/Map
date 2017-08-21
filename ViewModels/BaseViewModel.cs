using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Map.Annotations;
using Map.Commands;
using Map.Graphics;
using Map.Models;
using Map.UserControls;

namespace Map.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Grid layer
        /// </summary>
        public GridLayer GridLayer { get; set; }

        public bool AllowOccupiedPositions { get; set; }

        /// <summary>
        /// Item currently selected
        /// </summary>
        public BaseModel SelectedItem { get; set; }

        /// <summary>
        /// Position currently hovered
        /// </summary>
        public Point PositionInFocus { get; set; }

        /// <summary>
        /// List of valid new positions for a movable model
        /// </summary>
        public List<Point> ValidPositions { get; set; }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Commands

        /// <summary>
        /// Command to set the placeholder in focus
        /// </summary>
        private ICommand FocusOnPlaceholderCommand { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseViewModel()
        {
            AllowOccupiedPositions = false;
            ValidPositions = new List<Point>();

            FocusOnPlaceholderCommand = new RelayCommand(FocusOnPlaceholder, null);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialization
        /// </summary>
        public void Initialize()
        {
            GridLayer.Placeholders
                .ToList()
                .ForEach(p =>
                {
                    Placeholder.SetMouseEnterCommand(p, FocusOnPlaceholderCommand);
                    p.PreviewMouseLeftButtonDown += OnCellMouseLeftPressed;
                    p.PreviewMouseLeftButtonUp += OnCellMouseLeftReleased;
                    p.PreviewMouseRightButtonDown += OnCellMouseRightPressed;
                    p.PreviewMouseRightButtonUp += OnCellMouseRightReleased;
                });

            GridLayer.Children
                .OfType<BaseUserControl>()
                .ToList()
                .ForEach(uc =>
                {
                    uc.MouseLeftButtonDown += OnUserControlMouseLeftPressed;
                    uc.MouseLeftButtonUp += OnUserControlMouseLeftReleased;
                    uc.MouseRightButtonDown += OnUserControlMouseRightPressed;
                    uc.MouseRightButtonUp += OnUserControlMouseRightReleased;
                });
        }

        /// <summary>
        /// Store the position of the cell currently in focus
        /// </summary>
        /// <param name="parameter">Parameter sent by the caller</param>
        private void FocusOnPlaceholder(object parameter)
        {
            var placeholder = parameter as Placeholder;

            if (placeholder != null)
            {
                PositionInFocus = placeholder.Position;
                // MessageBox.Show($"Cell [{placeholder.Position.X}, {placeholder.Position.Y}]");
            }
        }

        /// <summary>
        /// Method executed when the left mouse button is pressed on a cell
        /// </summary>
        /// <param name="sender">Object firing the event</param>
        /// <param name="e">Mouse event arguments</param>
        public virtual void OnCellMouseLeftPressed(object sender, MouseEventArgs e)
        {
            var placeholder = sender as Placeholder;

            if (placeholder != null)
            {
                Console.WriteLine($"Left button pressed on cell [{placeholder.Position.X}, {placeholder.Position.Y}]");
            }
        }

        /// <summary>
        /// Method executed when the left mouse button is released on a cell
        /// </summary>
        /// <param name="sender">Object firing the event</param>
        /// <param name="e">Mouse event arguments</param>
        public virtual void OnCellMouseLeftReleased(object sender, MouseEventArgs e)
        {
            var placeholder = sender as Placeholder;

            if (placeholder != null)
            {
                Console.WriteLine($"Left button released on cell [{placeholder.Position.X}, {placeholder.Position.Y}]");
            }
        }

        /// <summary>
        /// Method executed when the right mouse button is pressed on a cell
        /// </summary>
        /// <param name="sender">Object firing the event</param>
        /// <param name="e">Mouse event arguments</param>
        public virtual void OnCellMouseRightPressed(object sender, MouseEventArgs e)
        {
            var placeholder = sender as Placeholder;

            if (placeholder != null)
            {
                Console.WriteLine($"Right button pressed on cell [{placeholder.Position.X}, {placeholder.Position.Y}]");
            }
        }

        /// <summary>
        /// Method executed when the right mouse button is released on a cell
        /// </summary>
        /// <param name="sender">Object firing the event</param>
        /// <param name="e">Mouse event arguments</param>
        public virtual void OnCellMouseRightReleased(object sender, MouseEventArgs e)
        {
            var placeholder = sender as Placeholder;

            if (placeholder != null)
            {
                Console.WriteLine($"Right button released on cell [{placeholder.Position.X}, {placeholder.Position.Y}]");
            }
        }

        /// <summary>
        /// Method executed when the left mouse button is pressed on a user control
        /// </summary>
        /// <param name="sender">Object firing the event</param>
        /// <param name="e">Mouse event arguments</param>
        public virtual void OnUserControlMouseLeftPressed(object sender, MouseEventArgs e)
        {
            var userControl = sender as BaseUserControl;

            if (userControl != null)
            {
                SelectedItem = userControl.DataContext as BaseModel;
                Console.WriteLine($"Left button pressed on user control [{userControl.Position.X}, {userControl.Position.Y}]");
            }
        }

        /// <summary>
        /// Method executed when the left mouse button is released on a user control
        /// </summary>
        /// <param name="sender">Object firing the event</param>
        /// <param name="e">Mouse event arguments</param>
        public virtual void OnUserControlMouseLeftReleased(object sender, MouseEventArgs e)
        {
            var userControl = sender as BaseUserControl;

            if (userControl != null)
            {
                Console.WriteLine($"Left button released on user control [{userControl.Position.X}, {userControl.Position.Y}]");
            }
        }

        /// <summary>
        /// Method executed when the right mouse button is pressed on a user control
        /// </summary>
        /// <param name="sender">Object firing the event</param>
        /// <param name="e">Mouse event arguments</param>
        public virtual void OnUserControlMouseRightPressed(object sender, MouseEventArgs e)
        {
            var userControl = sender as BaseUserControl;

            if (userControl != null)
            {
                Console.WriteLine($"Right button pressed on user control [{userControl.Position.X}, {userControl.Position.Y}]");
            }
        }

        /// <summary>
        /// Method executed when the right mouse button is released on a user control
        /// </summary>
        /// <param name="sender">Object firing the event</param>
        /// <param name="e">Mouse event arguments</param>
        public virtual void OnUserControlMouseRightReleased(object sender, MouseEventArgs e)
        {
            var userControl = sender as BaseUserControl;

            if (userControl != null)
            {
                Console.WriteLine($"Right button released on user control [{userControl.Position.X}, {userControl.Position.Y}]");
            }
        }

        /// <summary>
        /// Set valid positions as active
        /// </summary>
        public void ValidatePositions()
        {
            ValidPositions
                .Where(vp => vp.X > GridLayer.ColumnDefinitions.Count || 
                    vp.Y > GridLayer.RowDefinitions.Count)
                .ToList()
                .ForEach(vp => ValidPositions.Remove(vp));

            GridLayer.Placeholders
                .ToList()
                .ForEach(p => p.IsActive = false);

            GridLayer.Placeholders
                .Where(p => ValidPositions.Contains(p.Position))
                .ToList()
                .ForEach(p => p.IsActive = true);

            if (!AllowOccupiedPositions)
            {
                GridLayer.Placeholders
                    .Where(p => GetLayerData(p.Position) != null)
                    .ToList()
                    .ForEach(p => p.IsActive = false);
            }
        }

        /// <summary>
        /// Get the data of a user control at a specific position in the grid layer
        /// </summary>
        /// <param name="position">Position to search in</param>
        /// <returns>The data of the user control or null if no user control is found</returns>
        public BaseModel GetLayerData(Point position)
        {
            var userControl = GridLayer
                .Children
                .OfType<BaseUserControl>()
                .FirstOrDefault(uc => uc.Position == position);

            var model = userControl?.DataContext as BaseModel;

            if (userControl == null || model == null)
            {
                return null;
            }

            return model;
        }

        /// <summary>
        /// Check if the given position is legal for the selected item
        /// </summary>
        /// <param name="position">Position to test</param>
        /// <returns>True if the move is legal, false otherwise</returns>
        public bool IsLegalMove(Point position)
        {
            return GridLayer.Placeholders
                .First(p => p.Position == position)
                .IsActive;
        }

        /// <summary>
        /// Update the UI when a view model's property changes
        /// </summary>
        /// <param name="propertyName">Name of the changed property</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
