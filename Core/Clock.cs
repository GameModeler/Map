using System;
using System.Runtime.InteropServices;
using System.Security;
using Map.Core.Structs;

namespace Map.Core
{
    /// <summary>
    /// Utility class measuring the elapsed time
    /// </summary>
    public class Clock : ObjectBase
    {
        #region Imports

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfClock_create();

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfClock_destroy(IntPtr cPtr);

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfClock_getElapsedTime(IntPtr clock);

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfClock_restart(IntPtr clock);

        #endregion

        #region Properties

        /// <summary>
        /// Elapsed time since the last call to Restart
        /// </summary>
        public Time ElapsedTime => sfClock_getElapsedTime(CPtr);

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Clock() : base(sfClock_create()) {}

        #endregion

        #region Methods

        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            sfClock_destroy(CPtr);
        }

        /// <summary>
        /// Put the counter back to zero
        /// </summary>
        /// <returns>Time elapsed since the clock was started</returns>
        public Time Restart()
        {
            return sfClock_restart(CPtr);
        }

        #endregion
    }
}
