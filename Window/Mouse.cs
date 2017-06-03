using System;
using System.Runtime.InteropServices;
using System.Security;
using Map.Core.Structs;
using Map.Window.Enums;

namespace Map.Window
{
    /// <summary>
    /// Access to the mouse
    /// </summary>
    public static class Mouse
    {
        #region Imports

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfMouse_isButtonPressed(MouseButton button);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2Di sfMouse_getPosition(IntPtr relativeTo);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMouse_setPosition(Vector2Di position, IntPtr relativeTo);

        #endregion

        #region Methods

        /// <summary>
        /// Check if a mouse button is pressed
        /// </summary>
        /// <param name="button">Button to check</param>
        /// <returns>True if the button is pressed, false otherwise</returns>
        public static bool IsButtonPressed(MouseButton button)
        {
            return sfMouse_isButtonPressed(button);
        }

        /// <summary>
        /// Get the current position of the mouse in desktop coordinates
        /// </summary>
        /// <returns>Current position of the mouse</returns>
        public static Vector2Di GetPosition()
        {
            return GetPosition(null);
        }

        /// <summary>
        /// Get the current position of the mouse relative to a window
        /// </summary>
        /// <param name="relativeTo">Reference window</param>
        /// <returns>Current position of the mouse in the window</returns>
        public static Vector2Di GetPosition(Window relativeTo)
        {
            return relativeTo?.InternalGetMousePosition() ?? sfMouse_getPosition(IntPtr.Zero);
        }

        /// <summary>
        /// Set the current position of the mouse in desktop coordinates
        /// </summary>
        /// <param name="position">New position of the mouse</param>
        public static void SetPosition(Vector2Di position)
        {
            SetPosition(position, null);
        }

        /// <summary>
        /// Set the current position of the mouse relative to a window
        /// </summary>
        /// <param name="position">New position of the mouse</param>
        /// <param name="relativeTo">Reference window</param>
        public static void SetPosition(Vector2Di position, Window relativeTo)
        {
            if (relativeTo != null)
            {
                relativeTo.InternalSetMousePosition(position);
            }
            else
            {
                sfMouse_setPosition(position, IntPtr.Zero);
            }
        }

        #endregion
    }
}
