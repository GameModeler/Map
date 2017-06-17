using System;
using System.Runtime.InteropServices;
using System.Security;
using Map.Core.Structs;

namespace Map.Window
{
    /// <summary>
    /// Access to the real-time state of touches
    /// </summary>
    public static class Touch
    {
        #region Imports

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfTouch_isDown(int finger);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2Di sfTouch_getPosition(int finger, IntPtr relativeTo);

        #endregion

        #region Methods

        /// <summary>
        /// Check if a touch event is happening
        /// </summary>
        /// <param name="finger">Finger</param>
        /// <returns>True if the finger is currently touching the screen, false otherwise</returns>
        public static bool IsDown(int finger)
        {
            return sfTouch_isDown(finger);
        }

        /// <summary>
        /// Get the current touch position
        /// </summary>
        /// <param name="finger">Finger index</param>
        /// <returns>Current position of the finger</returns>
        public static Vector2Di GetPosition(int finger)
        {
            return GetPosition(finger, null);
        }

        /// <summary>
        /// Get the current touch position relative to the given window
        /// </summary>
        /// <param name="finger">Finger index</param>
        /// <param name="relativeTo">Reference window</param>
        /// <returns>Current position of the finger</returns>
        public static Vector2Di GetPosition(int finger, Window relativeTo)
        {
            return relativeTo?.InternalGetTouchPosition(finger) ?? sfTouch_getPosition(finger, IntPtr.Zero);
        }

        #endregion
    }
}
