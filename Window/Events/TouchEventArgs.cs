using System;

namespace Map.Window.Events
{
    /// <summary>
    /// Touch event parameters
    /// </summary>
    public class TouchEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Index of the finger (multi-touch events)
        /// </summary>
        public int Finger { get; set; }

        /// <summary>
        /// X position of the touch (relative to the left of the owner window)
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y position of the touch (relative to the top of the owner window)
        /// </summary>
        public int Y { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the touch arguments from a touch event
        /// </summary>
        /// <param name="e">Touch event</param>
        public TouchEventArgs(TouchEvent e)
        {
            Finger = e.Finger;
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
            return string.Format("[TouchEventArgs] " +
                                 $"Finger({Finger}) " +
                                 $"X({X}) " +
                                 $"Y({Y})");
        }

        #endregion
    }
}
