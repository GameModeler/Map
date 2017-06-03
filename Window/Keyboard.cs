using System.Runtime.InteropServices;
using System.Security;
using Map.Window.Enums;

namespace Map.Window
{
    /// <summary>
    /// Access to the keyboard
    /// </summary>
    public static class Keyboard
    {
        #region Imports

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfKeyboard_isKeyPressed(KeyboardKey key);

        #endregion

        #region Methods

        /// <summary>
        /// Check if a key is pressed
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if the key is pressed, false otherwise</returns>
        public static bool IsKeyPressed(KeyboardKey key)
        {
            return sfKeyboard_isKeyPressed(key);
        }

        #endregion
    }
}
