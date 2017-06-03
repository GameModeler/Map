using System;
using Map.Window.Enums;

namespace Map.Window.Events
{
    /// <summary>
    /// Keyboard event parameters
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Key code
        /// </summary>
        public KeyboardKey Code { get; set; }

        /// <summary>
        /// Alt modifier state
        /// </summary>
        public bool Alt { get; set; }

        /// <summary>
        /// Control modifier state
        /// </summary>
        public bool Control { get; set; }

        /// <summary>
        /// Shift modifier state
        /// </summary>
        public bool Shift { get; set; }

        /// <summary>
        /// System modifier state
        /// </summary>
        public bool System { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the key arguments from a key event
        /// </summary>
        /// <param name="e">Key event</param>
        public KeyEventArgs(KeyEvent e)
        {
            Code = e.Code;
            Alt = e.Alt != 0;
            Control = e.Control != 0;
            Shift = e.Shift != 0;
            System = e.System != 0;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[KeyEventArgs] " +
                                 $"Code({Code}) " +
                                 $"Alt({Alt}) " +
                                 $"Control({Control}) " +
                                 $"Shift({Shift}) " +
                                 $"System({System})");
        }

        #endregion
    }
}
