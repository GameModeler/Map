using System.Runtime.InteropServices;
using Map.Window.Enums;

namespace Map.Window.Events
{
    /// <summary>
    /// Mouse move event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseMoveEvent
    {
        /// <summary>
        /// X coordinate of the cursor
        /// </summary>
        public int X;

        /// <summary>
        /// Y coordinate of the cursor
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// Mouse buttons event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseButtonEvent
    {
        /// <summary>
        /// Button code
        /// </summary>
        public MouseButton Button;

        /// <summary>
        /// X coordinate of the cursor
        /// </summary>
        public int X;

        /// <summary>
        /// Y coordinate of the cursor
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// Mouse wheel event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseWheelEvent
    {
        /// <summary>
        /// Scroll amount
        /// </summary>
        public int Delta;

        /// <summary>
        /// X coordinate of the cursor
        /// </summary>
        public int X;

        /// <summary>
        /// Y coordinate of the cursor
        /// </summary>
        public int Y;
    }
}
