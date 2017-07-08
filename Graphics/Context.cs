using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Map.Graphics
{
    /// <summary>
    /// Define a context
    /// </summary>
    internal class Context : CriticalFinalizerObject
    {
        #region Imports

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfContext_create();

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfContext_destroy(IntPtr view);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfContext_setActive(IntPtr view, bool active);

        #endregion

        #region Attributes

        private static Context _globalContext;

        private readonly IntPtr _thisContext;

        #endregion

        #region Properties

        public static Context GlobalContext => _globalContext ?? (_globalContext = new Context());

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the context
        /// </summary>
        public Context()
        {
            _thisContext = sfContext_create();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Destroy the context
        /// </summary>
        ~Context()
        {
            sfContext_destroy(_thisContext);
        }

        /// <summary>
        /// Activate or deactivate the context
        /// </summary>
        /// <param name="active">True to activate, false to deactivate</param>
        public void SetActive(bool active)
        {
            sfContext_setActive(_thisContext, active);
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[Context]";
        }

        #endregion
    }
}
