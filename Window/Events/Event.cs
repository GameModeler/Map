using System.Runtime.InteropServices;
using Map.Window.Enums;

namespace Map.Window.Events
{
    /// <summary>
    /// Define a system event and its parameters
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 20)]
    public struct Event
    {
        /// <summary>
        /// Type of event
        /// </summary>
        [FieldOffset(0)]
        public EventType Type;

        /// <summary>
        /// Arguments for size event (resize)
        /// </summary>
        [FieldOffset(4)]
        public SizeEvent Size;

        /// <summary>
        /// Arguments for key events (key pressed, key released)
        /// </summary>
        [FieldOffset(4)]
        public KeyEvent Key;

        /// <summary>
        /// Arguments for text events (text entered)
        /// </summary>
        [FieldOffset(4)]
        public TextEvent Text;

        /// <summary>
        /// Arguments for mouse move events (mouse moved)
        /// </summary>
        [FieldOffset(4)]
        public MouseMoveEvent MouseMove;

        /// <summary>
        /// Arguments for mouse button events (mouse button pressed, mouse button released)
        /// </summary>
        [FieldOffset(4)]
        public MouseButtonEvent MouseButton;

        /// <summary>
        /// Arguments for mouse wheel events (mouse wheel moved)
        /// </summary>
        [FieldOffset(4)]
        public MouseWheelEvent MouseWheel;

        /// <summary>
        /// Arguments for controller connect events (controller connected, controller disconnected)
        /// </summary>
        [FieldOffset(4)]
        public ControllerConnectEvent ControllerConnect;

        /// <summary>
        /// Arguments for controller move events (controller axis moved)
        /// </summary>
        [FieldOffset(4)]
        public ControllerMoveEvent ControllerMove;

        /// <summary>
        /// Arguments for controller button events (controller button pressed, controller button released)
        /// </summary>
        [FieldOffset(4)]
        public ControllerButtonEvent ControllerButton;

        /// <summary>
        /// Arguments for touch events (touch began, touch moved, touch ended)
        /// </summary>
        [FieldOffset(4)]
        public TouchEvent Touch;

        /// <summary>
        /// Arguments for sensor events (sensor changed)
        /// </summary>
        [FieldOffset(4)]
        public SensorEvent Sensor;
    }
}
