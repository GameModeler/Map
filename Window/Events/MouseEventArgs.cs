using System;
using Map.Window.Enums;

namespace Map.Window.Events
{
    /// <summary>
    /// Mouse move event parameters
    /// </summary>
    public class MouseMoveEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// X coordinate of the cursor
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of the cursor
        /// </summary>
        public int Y { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the mouse move arguments from a mouse move event
        /// </summary>
        /// <param name="e">Mouse move event</param>
        public MouseMoveEventArgs(MouseMoveEvent e)
        {
            X = e.X;
            Y = e.Y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String descritption of the object</returns>
        public override string ToString()
        {
            return string.Format("[MouseMoveEventArgs] " +
                                 $"X({X}) " +
                                 $"Y({Y})");
        }

        #endregion
    }

    /// <summary>
    /// Mouse button event parameters
    /// </summary>
    public class MouseButtonEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Button code
        /// </summary>
        public MouseButton Button { get; set; }

        /// <summary>
        /// X coordinate of the cursor
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of the cursor
        /// </summary>
        public int Y { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the mouse button arguments from a mouse button event
        /// </summary>
        /// <param name="e">Mouse button event</param>
        public MouseButtonEventArgs(MouseButtonEvent e)
        {
            Button = e.Button;
            X = e.X;
            Y = e.Y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[MouseButtonEventArgs] " +
                                 $"Button({Button}) " +
                                 $"X({X}) " +
                                 $"Y({Y})");
        }

        #endregion
    }

    /// <summary>
    /// Mouse wheel event parameters
    /// </summary>
    public class MouseWheelEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Scroll amount
        /// </summary>
        public int Delta { get; set; }

        /// <summary>
        /// X coordinate of the cursor
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of the cursor
        /// </summary>
        public int Y { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the mouse wheel arguments from a mouse wheel event
        /// </summary>
        /// <param name="e">Mouse wheel event</param>
        public MouseWheelEventArgs(MouseWheelEvent e)
        {
            Delta = e.Delta;
            X = e.X;
            Y = e.Y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>Strring descriptino of the object</returns>
        public override string ToString()
        {
            return string.Format("[MouseWheelEventArgs] " +
                                 $"Delta({Delta}) " +
                                 $"X({X}) " +
                                 $"Y({Y})");
        }

        #endregion
    }
}
