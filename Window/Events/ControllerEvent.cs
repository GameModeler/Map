using System.Runtime.InteropServices;
using Map.Window.Enums;

namespace Map.Window.Events
{
    /// <summary>
    /// Controller connect event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerConnectEvent
    {
        /// <summary>
        /// Index of the controller wich triggered the event
        /// </summary>
        public int ControllerId;
    }

    /// <summary>
    /// Controller move event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerMoveEvent
    {
        /// <summary>
        /// Index of the controller wich triggered the event
        /// </summary>
        public int ControllerId;

        /// <summary>
        /// Controller axis
        /// </summary>
        public ControllerAxis Axis;

        /// <summary>
        /// Current position of the axis
        /// </summary>
        public float Position;
    }

    /// <summary>
    /// Controller buttons event parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerButtonEvent
    {
        /// <summary>
        /// Index of the controller wich triggered the event
        /// </summary>
        public int ControllerId;

        /// <summary>
        /// Index of the button
        /// </summary>
        public int Button;
    }
}
