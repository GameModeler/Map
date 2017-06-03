using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Map.Core;
using Map.Core.Structs;
using Map.Window.Enums;
using Map.Window.Events;
using Map.Window.Structs;

namespace Map.Window
{
    /// <summary>
    /// Rendering window
    /// Can create a new window or connect to an existing one
    /// </summary>
    public class Window : ObjectBase
    {
        #region Imports

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfWindow_create(VideoMode mode, string title, WindowStyle style, ref ContextSettings settings);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfWindow_createUnicode(VideoMode mode, IntPtr title, WindowStyle style, ref ContextSettings settings);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfWindow_createFromHandle(IntPtr handle, ref ContextSettings settings);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_destroy(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfWindow_isOpen(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_close(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfWindow_pollEvent(IntPtr cPtr, out Event evt);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfWindow_waitEvent(IntPtr cPtr, out Event evt);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_display(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern ContextSettings sfWindow_getSettings(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfWindow_getPosition(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setPosition(IntPtr cPtr, Vector2Di position);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfWindow_getSize(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setSize(IntPtr cPtr, Vector2Di size);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setTitle(IntPtr cPtr, string title);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setUnicodeTitle(IntPtr cPtr, IntPtr title);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfWindow_setIcon(IntPtr cPtr, int width, int height, byte* pixels);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setVisible(IntPtr cPtr, bool visible);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setMouseCursorVisible(IntPtr cPtr, bool show);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setVerticalSyncEnabled(IntPtr cPtr, bool enable);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setKeyRepeatEnabled(IntPtr cPtr, bool enable);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfWindow_setActive(IntPtr cPtr, bool active);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setFramerateLimit(IntPtr cPtr, int limit);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern uint sfWindow_getFrameTime(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_setJoystickThreshold(IntPtr cPtr, float threshold);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfWindow_getSystemHandle(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfWindow_requestFocus(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfWindow_hasFocus(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfMouse_getPosition(IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfMouse_setPosition(Vector2Di position, IntPtr cPtr);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfTouch_getPosition(int finger, IntPtr relativeTo);

        #endregion

        #region Properties

        /// <summary>
        /// Window state (opened or closed)
        /// </summary>
        public virtual bool IsOpen => sfWindow_isOpen(CPtr);

        /// <summary>
        /// Creation settings of the window
        /// </summary>
        public virtual ContextSettings Settings => sfWindow_getSettings(CPtr);

        /// <summary>
        /// Position of the window
        /// </summary>
        public virtual Vector2Di Position
        {
            get => sfWindow_getPosition(CPtr);
            set => sfWindow_setPosition(CPtr, value);
        }

        /// <summary>
        /// Size of the rendering region of the window
        /// </summary>
        public virtual Vector2Di Size
        {
            get => sfWindow_getSize(CPtr);
            set => sfWindow_setSize(CPtr, value);
        }

        /// <summary>
        /// OS-specific handle of the window
        /// </summary>
        public virtual IntPtr SystemHandle => sfWindow_getSystemHandle(CPtr);

        #endregion

        #region Events

        /// <summary>
        /// Event handler for the Closed event
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Event handler for the Resized event
        /// </summary>
        public event EventHandler<SizeEventArgs> Resized;

        /// <summary>
        /// Event handler for the LostFocus event
        /// </summary>
        public event EventHandler LostFocus;

        /// <summary>
        /// Event handler for the GainedFocus event
        /// </summary>
        public event EventHandler GainedFocus;

        /// <summary>
        /// Event handler for the TextEntered event
        /// </summary>
        public event EventHandler<TextEventArgs> TextEntered;

        /// <summary>
        /// Event handler for the KeyPressed event
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyPressed;

        /// <summary>
        /// Event handler for the KeyReleased event
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyReleased;

        /// <summary>
        /// Event handler for the MouseMoved event
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> MouseMoved;

        /// <summary>
        /// Event handler for the MouseButtonPressed event
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonPressed;

        /// <summary>
        /// Event handler for the MouseButtonReleased event
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonReleased;

        /// <summary>
        /// Event handler for the MouseWheelMoved event
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> MouseWheelScrolled;

        /// <summary>
        /// Event handler for the MouseEntered event
        /// </summary>
        public event EventHandler MouseEntered;

        /// <summary>
        /// Event handler for the MouseLeft event
        /// </summary>
        public event EventHandler MouseLeft;

        /// <summary>
        /// Event handler for the ControllerConnected event
        /// </summary>
        public event EventHandler<ControllerConnectEventArgs> ControllerConnected;

        /// <summary>
        /// Event handler for the ControllerDisconnected event
        /// </summary>
        public event EventHandler<ControllerConnectEventArgs> ControllerDisconnected;

        /// <summary>
        /// Event handler for the ControllerMoved event
        /// </summary>
        public event EventHandler<ControllerMoveEventArgs> ControllerMoved;

        /// <summary>
        /// Event handler for the ControllerButtonPressed event
        /// </summary>
        public event EventHandler<ControllerButtonEventArgs> ControllerButtonPressed;

        /// <summary>
        /// Event handler for the ControllerButtonReleased event
        /// </summary>
        public event EventHandler<ControllerButtonEventArgs> ControllerButtonReleased;

        /// <summary>
        /// Event handler for the TouchBegan event
        /// </summary>
        public event EventHandler<TouchEventArgs> TouchBegan;

        /// <summary>
        /// Event handler for the TouchMoved event
        /// </summary>
        public event EventHandler<TouchEventArgs> TouchMoved;

        /// <summary>
        /// Event handler for the TouchEnded event
        /// </summary>
        public event EventHandler<TouchEventArgs> TouchEnded;

        /// <summary>
        /// Event handler for the SensorChanged event
        /// </summary>
        public event EventHandler<SensorEventArgs> SensorChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Create the window with default style and creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        public Window(VideoMode mode, string title)
            : this(mode, title, WindowStyle.Default, new ContextSettings(0, 0)) {}

        /// <summary>
        /// Create the window with default creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        /// <param name="style">Window style</param>
        public Window(VideoMode mode, string title, WindowStyle style)
            : this(mode, title, style, new ContextSettings(0, 0)) {}

        /// <summary>
        /// Create the window
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        /// <param name="style">Window style</param>
        /// <param name="settings">Creation parameters</param>
        public Window(VideoMode mode, string title, WindowStyle style, ContextSettings settings)
            : base(IntPtr.Zero)
        {
            var titleAsUtf32 = Encoding.UTF32.GetBytes(string.Format($"{title}\0"));

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    CPtr = sfWindow_createUnicode(mode, (IntPtr) titlePtr, style, ref settings);
                }
            }
        }

        /// <summary>
        /// Create the window from an existing control with default creation settings
        /// </summary>
        /// <param name="handle">Plateform-specific handle of the control</param>
        public Window(IntPtr handle) : this(handle, new ContextSettings(0, 0)) {}

        /// <summary>
        /// Create the window from an existing control
        /// </summary>
        /// <param name="handle">Plateform-specific handle of the control</param>
        /// <param name="settings">Creation parameters</param>
        public Window(IntPtr handle, ContextSettings settings) 
            : base(sfWindow_createFromHandle(handle, ref settings)) {}

        /// <summary>
        /// Constructor for derived classes
        /// </summary>
        /// <param name="cPtr">Pointer to the internal object in the C API</param>
        /// <param name="dummy">Internal hack :)</param>
        protected Window(IntPtr cPtr, int dummy) : base(cPtr)
        {
            // Todo: Find a cleaner way of separating this constructor from Window(IntPtr handle)
        }

        #endregion

        #region Methods

        /// <summary>
        /// Close (destroy) the window
        /// The window instance remains valid to call create
        /// </summary>
        public virtual void Close()
        {
            sfWindow_close(CPtr);
        }

        /// <summary>
        /// Display the window on screen
        /// </summary>
        public virtual void Display()
        {
            sfWindow_display(CPtr);
        }

        /// <summary>
        /// Change the title of the window
        /// </summary>
        /// <param name="title">New title</param>
        public virtual void SetTitle(string title)
        {
            var titleAsUtf32 = Encoding.UTF32.GetBytes(string.Format($"{title}\0"));

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    sfWindow_setUnicodeTitle(CPtr, (IntPtr) titlePtr);
                }
            }
        }

        /// <summary>
        /// Change the icon of the window
        /// </summary>
        /// <param name="width">Icon width (in pixels)</param>
        /// <param name="height">Icon height (in pixels)</param>
        /// <param name="pixels">Array of pixels (format must be RGBA 32 bits)</param>
        public virtual void SetIcon(int width, int height, byte[] pixels)
        {
            unsafe
            {
                fixed (byte* pixelsPtr = pixels)
                {
                    sfWindow_setIcon(CPtr, width, height, pixelsPtr);
                }
            }
        }

        /// <summary>
        /// Show or hide the window
        /// </summary>
        /// <param name="visible">True to show the window, false to hide it</param>
        public virtual void SetVisible(bool visible)
        {
            sfWindow_setVisible(CPtr, visible);
        }

        /// <summary>
        /// Show or hide the mouse cursor
        /// </summary>
        /// <param name="visible">True to show the mouse cursor, false to hide it</param>
        public virtual void SetMouseCursorVisible(bool visible)
        {
            sfWindow_setMouseCursorVisible(CPtr, visible);
        }

        /// <summary>
        /// Enable / disable vertical synchronisation
        /// </summary>
        /// <param name="enable">True to enable v-sync, false to disable it</param>
        public virtual void SetVerticalSyncEnabled(bool enable)
        {
            sfWindow_setVerticalSyncEnabled(CPtr, enable);
        }

        /// <summary>
        /// Enable or disable automatic key-repeat
        /// Automatic key-repeat is enabled by default
        /// </summary>
        /// <param name="enable"></param>
        public virtual void SetKeyRepeatEnabled(bool enable)
        {
            sfWindow_setKeyRepeatEnabled(CPtr, enable);
        }

        /// <summary>
        /// Activate or deactivate the window as the current target for rendering
        /// </summary>
        /// <returns>True of operation was successful, false otherwise</returns>
        public virtual bool SetActive()
        {
            return SetActive(true);
        }

        /// <summary>
        /// Activate or deactivate the window as the current target for rendering
        /// </summary>
        /// <param name="active">True to activate, false to deactivate (true by default)</param>
        /// <returns>True of operation was successful, false otherwise</returns>
        public virtual bool SetActive(bool active)
        {
            return sfWindow_setActive(CPtr, active);
        }

        /// <summary>
        /// Limit the framerate to a max fixed frequency
        /// </summary>
        /// <param name="limit">Framerate limit (in frames per seconds, use 0 to disable limit)</param>
        public virtual void SetFramerateLimit(int limit)
        {
            sfWindow_setFramerateLimit(CPtr, limit);
        }

        /// <summary>
        /// Change the controller threshold (value below which no move event will be generated)
        /// </summary>
        /// <param name="threshold">New threshold (in range [0 .. 100])</param>
        public virtual void SetControllerThreshold(float threshold)
        {
            sfWindow_setJoystickThreshold(CPtr, threshold);
        }

        /// <summary>
        /// Wait for a new event and dispatch it to the corresponding event handler
        /// </summary>
        public void WaitAndDispatchEvents()
        {
            if (WaitEvent(out Event e))
            {
                CallEventHandler(e);
            }
        }

        /// <summary>
        /// Call the event handler for each pending event
        /// </summary>
        public void DispatchEvents()
        {
            while (PollEvent(out Event e))
            {
                CallEventHandler(e);
            }
        }

        /// <summary>
        /// Request the current window to be made the active foreground window
        /// </summary>
        public virtual void RequestFocus()
        {
            sfWindow_requestFocus(CPtr);
        }

        /// <summary>
        /// Check whether the window has the input focus
        /// </summary>
        /// <returns>True if the window has focus, false otherwise</returns>
        public virtual bool HasFocus()
        {
            return sfWindow_hasFocus(CPtr);
        }

        /// <summary>
        /// Get the next event (non-blocking)
        /// </summary>
        /// <param name="evenToFill">Variable to fill with the raw pointer to the event struct</param>
        /// <returns>True if there was an event, false otherwise</returns>
        public virtual bool PollEvent(out Event evenToFill)
        {
            return sfWindow_pollEvent(CPtr, out evenToFill);
        }

        /// <summary>
        /// Get the next event (blocking)
        /// </summary>
        /// <param name="eventToFill">Variable to fill with the raw pointer to the event struct</param>
        /// <returns>False if any error occured, true otherwise</returns>
        public virtual bool WaitEvent(out Event eventToFill)
        {
            return sfWindow_waitEvent(CPtr, out eventToFill);
        }

        /// <summary>
        /// Internal method to get the mouse position relative to the window
        /// This method is called by another class and is not meant to be called by users
        /// </summary>
        /// <returns>Relative mouse position</returns>
        protected internal virtual Vector2Di InternalGetMousePosition()
        {
            return sfMouse_getPosition(CPtr);
        }

        /// <summary>
        /// Internal method to set the mouse position relative to the window
        /// This method is called by another class and is not meant to be called by users
        /// </summary>
        /// <param name="position">Relative mouse position</param>
        protected internal virtual void InternalSetMousePosition(Vector2Di position)
        {
            sfMouse_setPosition(position, CPtr);
        }

        /// <summary>
        /// Internal method to get the touch position relative to the window.
        /// This method is called by another class and is not meant to be called by users
        /// </summary>
        /// <param name="finger">Finger index</param>
        /// <returns>Relative touch position</returns>
        protected internal virtual Vector2Di InternalGetTouchPosition(int finger)
        {
            return sfTouch_getPosition(finger, CPtr);
        }

        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            sfWindow_destroy(CPtr);
        }

        /// <summary>
        /// Call the event handler for the given event
        /// </summary>
        /// <param name="e">Event to dispatch</param>
        private void CallEventHandler(Event e)
        {
            switch (e.Type)
            {
                case EventType.Closed:
                    Closed?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.GainedFocus:
                    GainedFocus?.Invoke(this, EventArgs.Empty);
                    break;
                
                case EventType.LostFocus:
                    LostFocus?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.Resized:
                    Resized?.Invoke(this, new SizeEventArgs(e.Size));
                    break;

                case EventType.TextEntered:
                    TextEntered?.Invoke(this, new TextEventArgs(e.Text));
                    break;

                case EventType.KeyPressed:
                    KeyPressed?.Invoke(this, new KeyEventArgs(e.Key));
                    break;

                case EventType.KeyReleased:
                    KeyReleased?.Invoke(this, new KeyEventArgs(e.Key));
                    break;

                case EventType.MouseEntered:
                    MouseEntered?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.MouseLeft:
                    MouseLeft?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.MouseMoved:
                    MouseMoved?.Invoke(this, new MouseMoveEventArgs(e.MouseMove));
                    break;

                case EventType.MouseButtonPressed:
                    MouseButtonPressed?.Invoke(this, new MouseButtonEventArgs(e.MouseButton));
                    break;

                case EventType.MouseButtonReleased:
                    MouseButtonReleased?.Invoke(this, new MouseButtonEventArgs(e.MouseButton));
                    break;

                case EventType.MouseWheelMoved:
                    MouseWheelScrolled?.Invoke(this, new MouseWheelEventArgs(e.MouseWheel));
                    break;

                case EventType.MouseWheelScrolled:
                    MouseWheelScrolled?.Invoke(this, new MouseWheelEventArgs(e.MouseWheel));
                    break;

                case EventType.ControllerConnected:
                    ControllerConnected?.Invoke(this, new ControllerConnectEventArgs(e.ControllerConnect));
                    break;
                
                case EventType.ControllerDisconnected:
                    ControllerDisconnected?.Invoke(this, new ControllerConnectEventArgs(e.ControllerConnect));
                    break;
                
                case EventType.ControllerMoved:
                    ControllerMoved?.Invoke(this, new ControllerMoveEventArgs(e.ControllerMove));
                    break;
                
                case EventType.ControllerButtonPressed:
                    ControllerButtonPressed?.Invoke(this, new ControllerButtonEventArgs(e.ControllerButton));
                    break;
                
                case EventType.ControllerButtonReleased:
                    ControllerButtonReleased?.Invoke(this, new ControllerButtonEventArgs(e.ControllerButton));
                    break;

                case EventType.TouchBegan:
                    TouchBegan?.Invoke(this, new TouchEventArgs(e.Touch));
                    break;

                case EventType.TouchEnded:
                    TouchEnded?.Invoke(this, new TouchEventArgs(e.Touch));
                    break;
                
                case EventType.TouchMoved:
                    TouchMoved?.Invoke(this, new TouchEventArgs(e.Touch));
                    break;

                case EventType.SensorChanged:
                    SensorChanged?.Invoke(this, new SensorEventArgs(e.Sensor));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[Window] " +
                                 $"Size({Size}) " +
                                 $"Position({Position}) " +
                                 $"Settings({Settings})");
        }

        #endregion
    }
}
