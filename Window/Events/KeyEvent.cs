using System.Runtime.InteropServices;
using Map.Window.Enums;

namespace Map.Window.Events
{
    /// <summary>
    /// Keyboard event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyEvent
    {
        /// <summary>
        /// Key code
        /// </summary>
        public KeyboardKey Code;

        /// <summary>
        /// Alt modifier state
        /// </summary>
        public int Alt;

        /// <summary>
        /// Control modifier state
        /// </summary>
        public int Control;

        /// <summary>
        /// Shift modifier state
        /// </summary>
        public int Shift;

        /// <summary>
        /// System modifier state
        /// </summary>
        public int System;
    }
}
