using System.Runtime.InteropServices;

namespace Map.Window.Events
{
    /// <summary>
    /// Size event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SizeEvent
    {
        /// <summary>
        /// New width of the window
        /// </summary>
        public int Width;

        /// <summary>
        /// New height of the window
        /// </summary>
        public int Height;
    }
}
