using System;
using System.Runtime.InteropServices;
using System.Security;
using Map.Window.Enums;

namespace Map.Window
{
    /// <summary>
    /// Access to the controller
    /// Tested successfully with Sony's PlayStation DS4 controller
    /// Testing needed for controllers from other manufacturers
    /// </summary>
    public static class Controller
    {
        /// <summary>
        /// Internal struct used for marshaling the controller identification struct from unmanaged code
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct IdentificationMarshalData
        {
            public IntPtr Name;
            public int VendorId;
            public int ProductId;
        }

        /// <summary>
        /// Identification holds a controller's identification
        /// </summary>
        public struct Identification
        {
            /// <summary>
            /// Name of the controller
            /// </summary>
            public string Name;

            /// <summary>
            /// Manufacturer identifier
            /// </summary>
            public int VendorId;

            /// <summary>
            /// Product identifier
            /// </summary>
            public int ProductId;
        }

        #region Imports

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfJoystick_isConnected(int joystick);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int sfJoystick_getButtonCount(int joystick);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfJoystick_hasAxis(int joystick, ControllerAxis axis);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfJoystick_isButtonPressed(int joystick, int button);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfJoystick_getAxisPosition(int joystick, ControllerAxis axis);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfJoystick_update();

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IdentificationMarshalData sfJoystick_getIdentification(int joystick);

        #endregion

        #region Properties

        /// <summary>
        /// Max number of supported controllers
        /// </summary>
        public static readonly int Count = 8;

        /// <summary>
        /// Max number of supported buttons
        /// </summary>
        public static readonly int ButtonCount = 32;

        /// <summary>
        /// Max number of supported axes
        /// </summary>
        public static readonly int AxisCount = 8;

        #endregion

        #region Methods

        /// <summary>
        /// Check if a controller is connected
        /// </summary>
        /// <param name="controller">Index of the controller</param>
        /// <returns>True if the controller is connected, false otherwise</returns>
        public static bool IsConnected(int controller)
        {
            return sfJoystick_isConnected(controller);
        }

        /// <summary>
        /// Get the number of buttons supported by a controller
        /// Returns 0 if no controller connected
        /// </summary>
        /// <param name="controller">Index of the controller</param>
        /// <returns>Number of buttons supported by the controller</returns>
        public static int GetButtonCount(int controller)
        {
            return sfJoystick_getButtonCount(controller);
        }

        /// <summary>
        /// Check if the controller supports a given axis
        /// </summary>
        /// <param name="controller">Index of the controller</param>
        /// <param name="axis">Axis to check</param>
        /// <returns>True if the axis is supported, false otherwise</returns>
        public static bool HasAxis(int controller, ControllerAxis axis)
        {
            return sfJoystick_hasAxis(controller, axis);
        }

        /// <summary>
        /// Check if a controller button is pressed
        /// Returns false if no controller connected
        /// </summary>
        /// <param name="controller">Index of the controller</param>
        /// <param name="button">Button to check</param>
        /// <returns>True if the button is pressed, false otherwise</returns>
        public static bool IsButtonPressed(int controller, int button)
        {
            return sfJoystick_isButtonPressed(controller, button);
        }

        /// <summary>
        /// Get the current position of a controller axis
        /// Returns 0 if no controller connected
        /// </summary>
        /// <param name="controller">Index of the controller</param>
        /// <param name="axis">Axis to check</param>
        /// <returns>Current position of the axis in range [-100 .. 100]</returns>
        public static float GetAxisPosition(int controller, ControllerAxis axis)
        {
            return sfJoystick_getAxisPosition(controller, axis);
        }

        /// <summary>
        /// Update the state of all controllers
        /// </summary>
        public static void Update()
        {
            sfJoystick_update();
        }

        /// <summary>
        /// Get the controller information
        /// </summary>
        /// <param name="controller">Index of the controller</param>
        /// <returns>Structure containing the controller information</returns>
        public static Identification GetIdentification(int controller)
        {
            var identification = sfJoystick_getIdentification(controller);
            var retidentification = new Identification()
            {
                Name = Marshal.PtrToStringAnsi(identification.Name),
                VendorId = identification.VendorId,
                ProductId = identification.ProductId
            };

            return retidentification;
        }

        #endregion
    }
}
