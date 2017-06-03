using System;
using System.IO;
using System.Runtime.InteropServices;
using Map.Window.Structs;

namespace Map.Window
{
    /// <summary>
    /// Adapt a System.IO.Stream to be usable as an InputStream
    /// </summary>
    public class StreamAdaptor : IDisposable
    {
        #region Attributes

        /// <summary>
        /// System.IO.Stream object
        /// </summary>
        private readonly Stream _stream;

        #endregion

        #region Properties

        /// <summary>
        /// Input stream pointer
        /// </summary>
        public IntPtr InputStreamPtr { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct from a System.IO.Stream
        /// </summary>
        /// <param name="stream">Strean to adapt</param>
        public StreamAdaptor(Stream stream)
        {
            _stream = stream;

            var inputStream = new InputStream
            {
                Read = Read,
                Seek = Seek,
                Tell = Tell,
                GetSize = GetSize
            };

            InputStreamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(inputStream));
            Marshal.StructureToPtr(inputStream, InputStreamPtr, false);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Dispose the object
        /// </summary>
        ~StreamAdaptor()
        {
            Dispose(false);
        }

        /// <summary>
        /// Explicitely dispose the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing or is it an explicit call?</param>
        public void Dispose(bool disposing)
        {
            Marshal.FreeHGlobal(InputStreamPtr);
        }

        /// <summary>
        /// Called to read from the stream
        /// </summary>
        /// <param name="data">Where to copy the read bytes</param>
        /// <param name="size">Size to read (in bytes)</param>
        /// <param name="userData">User data (unused)</param>
        /// <returns>Number of bytes read</returns>
        private long Read(IntPtr data, long size, IntPtr userData)
        {
            var buffer = new byte[size];
            var count = _stream.Read(buffer, 0, (int) size);

            Marshal.Copy(buffer, 0, data, count);

            return count;
        }

        /// <summary>
        /// Called to seek the read position in the stream
        /// </summary>
        /// <param name="position">New read position</param>
        /// <param name="userData">User data (unused)</param>
        /// <returns>Actual position</returns>
        private long Seek(long position, IntPtr userData)
        {
            return _stream.Seek(position, SeekOrigin.Begin);
        }

        /// <summary>
        /// Called to get the current read position in the stream
        /// </summary>
        /// <param name="userData">User data (unused)</param>
        /// <returns>Current position in the stream</returns>
        private long Tell(IntPtr userData)
        {
            return _stream.Position;
        }

        /// <summary>
        /// Called to get total size of the stream
        /// </summary>
        /// <param name="userData">User data (unused)</param>
        /// <returns>Number of bytes in the stream</returns>
        private long GetSize(IntPtr userData)
        {
            return _stream.Length;
        }

        #endregion
    }
}
