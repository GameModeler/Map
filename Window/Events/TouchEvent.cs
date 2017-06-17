using System.Runtime.InteropServices;

namespace Map.Window.Events
{
    /// <summary>
    /// Touch event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TouchEvent
    {
        /// <summary>
        /// Index of the finger (multi-touch events)
        /// </summary>
        public int Finger;

        /// <summary>
        /// X position of the touch (relative to the left of the owner window)
        /// </summary>
        public int X;

        /// <summary>
        /// Y position of the touch (relative to the top of the owner window)
        /// </summary>
        public int Y;
    }
}
