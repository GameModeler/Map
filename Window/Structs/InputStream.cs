using System;
using System.Runtime.InteropServices;

namespace Map.Window.Structs
{
    /// <summary>
    /// Input stram callbacks
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct InputStream
    {
        /// <summary>
        /// Callback to read data from the current stream
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long ReadCallbackType(IntPtr data, long size, IntPtr userData);

        /// <summary>
        /// Callback to seek the current stream
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long SeekCallbackType(long position, IntPtr userData);

        /// <summary>
        /// Callback to return the current stream's position
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long TellCallbackType(IntPtr userData);

        /// <summary>
        /// Callback to return the current stream's size
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long GetSizeCallbackType(IntPtr userData);

        /// <summary>
        /// Called to read data from the stream
        /// </summary>
        public ReadCallbackType Read;

        /// <summary>
        /// Called to seek the stream
        /// </summary>
        public SeekCallbackType Seek;

        /// <summary>
        /// Called to return the position
        /// </summary>
        public TellCallbackType Tell;

        /// <summary>
        /// Called to return the size
        /// </summary>
        public GetSizeCallbackType GetSize;
    }
}
