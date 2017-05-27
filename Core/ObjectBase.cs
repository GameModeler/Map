using System;

namespace Map.Core
{
    /// <summary>
    /// The ObjectBase class is an abstract base for every object of the map.
    /// </summary>
    public abstract class ObjectBase : IDisposable
    {
        #region Properties

        /// <summary>
        /// Pointer of the object (Use of a C library)
        /// </summary>
        public IntPtr CPtr { get; protected set; }

        #endregion

        #region Constructors

        protected ObjectBase(IntPtr cPtr)
        {
            CPtr = cPtr;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Dispose the object
        /// </summary>
        ~ObjectBase()
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
        /// <param name="disposing">Is the garbage collector disposing the object or is it an explicit call?</param>
        private void Dispose(bool disposing)
        {
            if (CPtr == IntPtr.Zero) return;
            Destroy(disposing);
            CPtr = IntPtr.Zero;
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object or is it an explicit call?</param>
        protected abstract void Destroy(bool disposing);

        #endregion
    }
}
