using System.Runtime.InteropServices;

namespace Map.Window.Events
{
    /// <summary>
    /// Text event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TextEvent
    {
        /// <summary>
        /// UTF32 value of the character
        /// </summary>
        public int Unicode;
    }
}
