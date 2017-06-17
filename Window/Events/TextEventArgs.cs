using System;

namespace Map.Window.Events
{
    /// <summary>
    /// Text event paramaters
    /// </summary>
    public class TextEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// UTF16 value of the character
        /// </summary>
        public string Unicode { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the text arguments from a text event
        /// </summary>
        /// <param name="e">Text event</param>
        public TextEventArgs(TextEvent e)
        {
            Unicode = Char.ConvertFromUtf32((int) e.Unicode);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[TextEventArgs] " +
                                 $"Unicode({Unicode})");
        }

        #endregion
    }
}
