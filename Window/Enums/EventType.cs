namespace Map.Window.Enums
{
    /// <summary>
    /// Different event types
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Triggered when a window is manually closed
        /// </summary>
        Closed,

        /// <summary>
        /// Triggered when a window is resized
        /// </summary>
        Resized,

        /// <summary>
        /// Triggered when a window loses the focus
        /// </summary>
        LostFocus,

        /// <summary>
        /// Triggered when a window gains the focus
        /// </summary>
        GainedFocus,

        /// <summary>
        /// Triggered when a valid character is entered
        /// </summary>
        TextEntered,

        /// <summary>
        /// Triggered when a keyboard key is pressed
        /// </summary>
        KeyPressed,

        /// <summary>
        /// Triggered when a keyboard key is released
        /// </summary>
        KeyReleased,

        /// <summary>
        /// Triggered when the mouse wheel is scrolled (deprecated)
        /// </summary>
        MouseWheelMoved,

        /// <summary>
        /// Triggered when the mouse wheel is scrolled
        /// </summary>
        MouseWheelScrolled,

        /// <summary>
        /// Triggered when a mouse button is pressed
        /// </summary>
        MouseButtonPressed,

        /// <summary>
        /// Triggered when a mouse button is released
        /// </summary>
        MouseButtonReleased,

        /// <summary>
        /// Triggered when the mouse moves within the area of a window
        /// </summary>
        MouseMoved,

        /// <summary>
        /// Triggered when the mouse enters the area of a window
        /// </summary>
        MouseEntered,

        /// <summary>
        /// Triggered when the mouse leaves the area of a window
        /// </summary>
        MouseLeft,

        /// <summary>
        /// Triggered when a controller button is pressed
        /// </summary>
        ControllerButtonPressed,

        /// <summary>
        /// Triggered when a controller button is released
        /// </summary>
        ControllerButtonReleased,

        /// <summary>
        /// Triggered when a controller axis moves
        /// </summary>
        ControllerMoved,

        /// <summary>
        /// Triggered when a controller is connected
        /// </summary>
        ControllerConnected,

        /// <summary>
        /// Triggered when a controller is disconnected
        /// </summary>
        ControllerDisconnected,

        /// <summary>
        /// Triggered when a touch begins
        /// </summary>
        TouchBegan,

        /// <summary>
        /// Triggered when a touch is moved
        /// </summary>
        TouchMoved,

        /// <summary>
        /// Triggered when a touch is ended
        /// </summary>
        TouchEnded,

        /// <summary>
        /// Triggered when a sensor is changed
        /// </summary>
        SensorChanged,

        /// <summary>
        /// Total number of event types (keep in last position)
        /// </summary>
        Count
    }
}
