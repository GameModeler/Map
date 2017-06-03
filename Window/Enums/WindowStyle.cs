using System;

namespace Map.Window.Enums
{
    /// <summary>
    /// Window styles
    /// </summary>
    [Flags]
    public enum WindowStyle
    {
        /// <summary>
        /// No border, no title bar
        /// </summary>
        None = 0,

        /// <summary>
        /// Title bar, fixed border
        /// </summary>
        TitleBar = 1 << 0,

        /// <summary>
        /// Title bar, resizable border, maximize button
        /// </summary>
        Resize = 1 << 1,

        /// <summary>
        /// Title bar, close button
        /// </summary>
        Close = 1 << 2,

        /// <summary>
        /// Fullscreen mode
        /// </summary>
        Fullscreen = 1 << 3,

        /// <summary>
        /// Default style (title bar, resize and close buttons)
        /// </summary>
        Default = TitleBar | Resize | Close
    }
}
