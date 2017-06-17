using System;

namespace Map.Window.Events
{
    /// <summary>
    /// Size event parameters
    /// </summary>
    public class SizeEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// New width of the window
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// New height of the window
        /// </summary>
        public int Height { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the size arguments from a size event
        /// </summary>
        /// <param name="e">Size event</param>
        public SizeEventArgs(SizeEvent e)
        {
            Width = e.Width;
            Height = e.Height;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String descritption of the object</returns>
        public override string ToString()
        {
            return string.Format("[SizeEventArgs] " +
                                 $"Width({Width}) " +
                                 $"Height({Height})");
        }

        #endregion
    }
}
