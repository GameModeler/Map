﻿using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Map.Core.Structs;
using Map.Graphics.Enums;
using Map.Graphics.Interfaces;
using Map.Graphics.Structs;
using Map.Window.Enums;
using Map.Window.Events;
using Map.Window.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Window wrapper that allows easy 2D rendering
    /// </summary>
    public class RenderWindow : Window.Window, IRenderTarget
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderWindow_create(VideoMode mode, string title, WindowStyle style, ref ContextSettings Params);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderWindow_createUnicode(VideoMode mode, IntPtr title, WindowStyle style, ref ContextSettings Params);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderWindow_createFromHandle(IntPtr handle, ref ContextSettings Params);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_destroy(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderWindow_isOpen(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_close(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderWindow_pollEvent(IntPtr cPointer, out Event evt);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderWindow_waitEvent(IntPtr cPointer, out Event evt);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_clear(IntPtr cPointer, Color clearColor);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_display(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern ContextSettings sfRenderWindow_getSettings(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfRenderWindow_getPosition(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setPosition(IntPtr cPointer, Vector2Di position);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfRenderWindow_getSize(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setSize(IntPtr cPointer, Vector2Di size);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setTitle(IntPtr cPointer, string title);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setUnicodeTitle(IntPtr cPointer, IntPtr title);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfRenderWindow_setIcon(IntPtr cPointer, int width, int height, byte* pixels);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setVisible(IntPtr cPointer, bool visible);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setMouseCursorVisible(IntPtr cPointer, bool visible);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setVerticalSyncEnabled(IntPtr cPointer, bool enable);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setKeyRepeatEnabled(IntPtr cPointer, bool enable);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderWindow_setActive(IntPtr cPointer, bool active);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderWindow_saveGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderWindow_restoreGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setFramerateLimit(IntPtr cPointer, int limit);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern uint sfRenderWindow_getFrameTime(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setJoystickThreshold(IntPtr cPointer, float threshold);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_setView(IntPtr cPointer, IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderWindow_getView(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderWindow_getDefaultView(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntRect sfRenderWindow_getViewport(IntPtr cPointer, IntPtr targetView);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfRenderWindow_mapCoordsToPixel(IntPtr cPointer, Vector2Df point, IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Df sfRenderWindow_mapPixelToCoords(IntPtr cPointer, Vector2Di point, IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfRenderWindow_drawPrimitives(IntPtr cPointer, Vertex* vertexPtr, int vertexCount, 
            PrimitiveType type, ref RenderStates.MarshalData renderStates);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_pushGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_popGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_resetGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderWindow_getSystemHandle(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderWindow_capture(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_requestFocus(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderWindow_hasFocus(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfMouse_getPositionRenderWindow(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfMouse_setPositionRenderWindow(Vector2Di position, IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfTouch_getPositionRenderWindow(int finger, IntPtr relativeTo);

        #endregion

        #region Attributes

        private View _defaultView;

        #endregion;

        #region Properties

        /// <summary>
        /// Tell whether or not the window is opened.
        /// </summary>
        /// <returns>True if the window is opened</returns>
        public override bool IsOpen => sfRenderWindow_isOpen(CPtr);
        
        /// <summary>
        /// Creation settings of the window
        /// </summary>
        public override ContextSettings Settings => sfRenderWindow_getSettings(CPtr);
        
        /// <summary>
        /// Position of the window
        /// </summary>
        public override Vector2Di Position
        {
            get => sfRenderWindow_getPosition(CPtr);
            set => sfRenderWindow_setPosition(CPtr, value);
        }
        
        /// <summary>
        /// Size of the rendering region of the window
        /// </summary>
        public override Vector2Di Size
        {
            get => sfRenderWindow_getSize(CPtr);
            set => sfRenderWindow_setSize(CPtr, value);
        }
        
        /// <summary>
        /// OS-specific handle of the window
        /// </summary>
        public override IntPtr SystemHandle => sfRenderWindow_getSystemHandle(CPtr);
        
        /// <summary>
        /// Default view of the window
        /// </summary>
        public View DefaultView => new View(_defaultView);

        #endregion

        #region Constructors

        /// <summary>
        /// Create the window with default style and creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        public RenderWindow(VideoMode mode, string title) :
            this(mode, title, WindowStyle.Default, new ContextSettings(0, 0)) {}
        
        /// <summary>
        /// Create the window with default creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        /// <param name="style">Window style (Resize | Close by default)</param>
        public RenderWindow(VideoMode mode, string title, WindowStyle style) :
            this(mode, title, style, new ContextSettings(0, 0)) {}
        
        /// <summary>
        /// Create the window
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        /// <param name="style">Window style (Resize | Close by default)</param>
        /// <param name="settings">Creation parameters</param>
        public RenderWindow(VideoMode mode, string title, WindowStyle style, ContextSettings settings) :
            base(IntPtr.Zero, 0)
        {
            // Copy the string to a null-terminated UTF-32 byte array
            var titleAsUtf32 = Encoding.UTF32.GetBytes(string.Format($"{title}\0"));

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    CPtr = sfRenderWindow_createUnicode(mode, (IntPtr) titlePtr, style, ref settings);
                }
            }

            Initialize();
        }
        
        /// <summary>
        /// Create the window from an existing control with default creation settings
        /// </summary>
        /// <param name="handle">Platform-specific handle of the control</param>
        public RenderWindow(IntPtr handle) : this(handle, new ContextSettings(0, 0)) {}
        
        /// <summary>
        /// Create the window from an existing control
        /// </summary>
        /// <param name="handle">Platform-specific handle of the control</param>
        /// <param name="settings">Creation parameters</param>
        public RenderWindow(IntPtr handle, ContextSettings settings) :
            base(sfRenderWindow_createFromHandle(handle, ref settings), 0)
        {
            Initialize();
        }

        #endregion

        /// <summary>
        /// Close (destroy) the window.
        /// The Window instance remains valid and you can call
        /// Create to recreate the window
        /// </summary>
        public override void Close()
        {
            sfRenderWindow_close(CPtr);
        }
        
        /// <summary>
        /// Display the window on screen
        /// </summary>
        public override void Display()
        {
            sfRenderWindow_display(CPtr);
        }

        /// <summary>
        /// Change the title of the window
        /// </summary>
        /// <param name="title">New title</param>
        public override void SetTitle(string title)
        {
            // Copy the title to a null-terminated UTF-32 byte array
            var titleAsUtf32 = Encoding.UTF32.GetBytes(title + '\0');

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    sfRenderWindow_setUnicodeTitle(CPtr, (IntPtr) titlePtr);
                }
            }
        }
        
        /// <summary>
        /// Change the window's icon
        /// </summary>
        /// <param name="width">Icon's width, in pixels</param>
        /// <param name="height">Icon's height, in pixels</param>
        /// <param name="pixels">Array of pixels, format must be RGBA 32 bits</param>
        public override void SetIcon(int width, int height, byte[] pixels)
        {
            unsafe
            {
                fixed (byte* pixelsPtr = pixels)
                {
                    sfRenderWindow_setIcon(CPtr, width, height, pixelsPtr);
                }
            }
        }
        
        /// <summary>
        /// Show or hide the window
        /// </summary>
        /// <param name="visible">True to show the window, false to hide it</param>
        public override void SetVisible(bool visible)
        {
            sfRenderWindow_setVisible(CPtr, visible);
        }
        
        /// <summary>
        /// Show or hide the mouse cursor
        /// </summary>
        /// <param name="visible">True to show, false to hide</param>
        public override void SetMouseCursorVisible(bool visible)
        {
            sfRenderWindow_setMouseCursorVisible(CPtr, visible);
        }
        
        /// <summary>
        /// Enable / disable vertical synchronization
        /// </summary>
        /// <param name="enable">True to enable v-sync, false to deactivate</param>
        public override void SetVerticalSyncEnabled(bool enable)
        {
            sfRenderWindow_setVerticalSyncEnabled(CPtr, enable);
        }
        
        /// <summary>
        /// Enable or disable automatic key-repeat.
        /// Automatic key-repeat is enabled by default
        /// </summary>
        /// <param name="enable">True to enable, false to disable</param>
        public override void SetKeyRepeatEnabled(bool enable)
        {
            sfRenderWindow_setKeyRepeatEnabled(CPtr, enable);
        }
        
        /// <summary>
        /// Activate of deactivate the window as the current target
        /// for rendering
        /// </summary>
        /// <param name="active">True to activate, false to deactivate (true by default)</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        public override bool SetActive(bool active)
        {
            return sfRenderWindow_setActive(CPtr, active);
        }
        
        /// <summary>
        /// Limit the framerate to a maximum fixed frequency
        /// </summary>
        /// <param name="limit">Framerate limit, in frames per seconds (use 0 to disable limit)</param>
        public override void SetFramerateLimit(int limit)
        {
            sfRenderWindow_setFramerateLimit(CPtr, limit);
        }

        /// <summary>
        /// Change the controller threshold (value below which no move event will be generated)
        /// </summary>
        /// <param name="threshold">New threshold (in range [0, 100])</param>
        public override void SetControllerThreshold(float threshold)
        {
            sfRenderWindow_setJoystickThreshold(CPtr, threshold);
        }

        /// <summary>
        /// Return the current active view
        /// </summary>
        /// <returns>The current view</returns>
        public View GetView()
        {
            return new View(sfRenderWindow_getView(CPtr));
        }
        
        /// <summary>
        /// Change the current active view
        /// </summary>
        /// <param name="view">New view</param>
        public void SetView(View view)
        {
            sfRenderWindow_setView(CPtr, view.CPtr);
        }
        
        /// <summary>
        /// Get the viewport of a view applied to this target
        /// </summary>
        /// <param name="view">Target view</param>
        /// <returns>Viewport rectangle, expressed in pixels in the current target</returns>
        public IntRect GetViewport(View view)
        {
            return sfRenderWindow_getViewport(CPtr, view.CPtr);
        }
        
        /// <summary>
        /// Convert a point from target coordinates to world
        /// coordinates, using the current view
        ///
        /// This function is an overload of the MapPixelToCoords
        /// function that implicitely uses the current view.
        /// It is equivalent to:
        /// target.MapPixelToCoords(point, target.GetView());
        /// </summary>
        /// <param name="point">Pixel to convert</param>
        /// <returns>The converted point, in "world" coordinates</returns>
        public Vector2Df MapPixelToCoords(Vector2Di point)
        {
            return MapPixelToCoords(point, GetView());
        }
        
        /// <summary>
        /// Convert a point from target coordinates to world coordinates
        ///
        /// This function finds the 2D position that matches the
        /// given pixel of the render-target. In other words, it does
        /// the inverse of what the graphics card does, to find the
        /// initial position of a rendered pixel.
        ///
        /// Initially, both coordinate systems (world units and target pixels)
        /// match perfectly. But if you define a custom view or resize your
        /// render-target, this assertion is not true anymore, ie. a point
        /// located at (10, 50) in your render-target may map to the point
        /// (150, 75) in your 2D world -- if the view is translated by (140, 25).
        ///
        /// For render-windows, this function is typically used to find
        /// which point (or object) is located below the mouse cursor.
        ///
        /// This version uses a custom view for calculations, see the other
        /// overload of the function if you want to use the current view of the
        /// render-target.
        /// </summary>
        /// <param name="point">Pixel to convert</param>
        /// <param name="view">The view to use for converting the point</param>
        /// <returns>The converted point, in "world" coordinates</returns>
        public Vector2Df MapPixelToCoords(Vector2Di point, View view)
        {
            return sfRenderWindow_mapPixelToCoords(CPtr, point, view?.CPtr ?? IntPtr.Zero);
        }
        
        /// <summary>
        /// Convert a point from world coordinates to target
        /// coordinates, using the current view
        ///
        /// This function is an overload of the mapCoordsToPixel
        /// function that implicitely uses the current view.
        /// It is equivalent to:
        /// target.MapCoordsToPixel(point, target.GetView());
        /// </summary>
        /// <param name="point">Point to convert</param>
        /// <returns>The converted point, in target coordinates (pixels)</returns>
        public Vector2Di MapCoordsToPixel(Vector2Df point)
        {
            return MapCoordsToPixel(point, GetView());
        }
        
        /// <summary>
        /// Convert a point from world coordinates to target coordinates
        ///
        /// This function finds the pixel of the render-target that matches
        /// the given 2D point. In other words, it goes through the same process
        /// as the graphics card, to compute the final position of a rendered point.
        ///
        /// Initially, both coordinate systems (world units and target pixels)
        /// match perfectly. But if you define a custom view or resize your
        /// render-target, this assertion is not true anymore, ie. a point
        /// located at (150, 75) in your 2D world may map to the pixel
        /// (10, 50) of your render-target -- if the view is translated by (140, 25).
        ///
        /// This version uses a custom view for calculations, see the other
        /// overload of the function if you want to use the current view of the
        /// render-target.
        /// </summary>
        /// <param name="point">Point to convert</param>
        /// <param name="view">The view to use for converting the point</param>
        /// <returns>The converted point, in target coordinates (pixels)</returns>
        public Vector2Di MapCoordsToPixel(Vector2Df point, View view)
        {
            return sfRenderWindow_mapCoordsToPixel(CPtr, point, view?.CPtr ?? IntPtr.Zero);
        }
        
        /// <summary>
        /// Clear the entire window with black color
        /// </summary>
        public void Clear()
        {
            sfRenderWindow_clear(CPtr, Color.Black);
        }
        
        /// <summary>
        /// Clear the entire window with a single color
        /// </summary>
        /// <param name="color">Color to use to clear the window</param>
        public void Clear(Color color)
        {
            sfRenderWindow_clear(CPtr, color);
        }
        
        /// <summary>
        /// Draw a drawable object to the render-target, with default render states
        /// </summary>
        /// <param name="drawable">Object to draw</param>
        public void Draw(IDrawable drawable)
        {
            Draw(drawable, RenderStates.Default);
        }
        
        /// <summary>
        /// Draw a drawable object to the render-target
        /// </summary>
        /// <param name="drawable">Object to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        public void Draw(IDrawable drawable, RenderStates states)
        {
            drawable.Draw(this, states);
        }
        
        /// <summary>
        /// Draw primitives defined by an array of vertices, with default render states
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="type">Type of primitives to draw</param>
        public void Draw(Vertex[] vertices, PrimitiveType type)
        {
            Draw(vertices, type, RenderStates.Default);
        }

        /// <summary>
        /// Draw primitives defined by an array of vertices
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="type">Type of primitives to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        public void Draw(Vertex[] vertices, PrimitiveType type, RenderStates states)
        {
            Draw(vertices, 0, vertices.Length, type, states);
        }
        
        /// <summary>
        /// Draw primitives defined by a sub-array of vertices, with default render states
        /// </summary>
        /// <param name="vertices">Array of vertices to draw</param>
        /// <param name="start">Index of the first vertex to draw in the array</param>
        /// <param name="count">Number of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        public void Draw(Vertex[] vertices, int start, int count, PrimitiveType type)
        {
            Draw(vertices, start, count, type, RenderStates.Default);
        }
        
        /// <summary>
        /// Draw primitives defined by a sub-array of vertices
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="start">Index of the first vertex to use in the array</param>
        /// <param name="count">Number of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        public void Draw(Vertex[] vertices, int start, int count, PrimitiveType type, RenderStates states)
        {
            var marshaledStates = states.Marshal();

            unsafe
            {
                fixed (Vertex* vertexPtr = vertices)
                {
                    sfRenderWindow_drawPrimitives(CPtr, vertexPtr + start, count, type, ref marshaledStates);
                }
            }
        }
        
        /// <summary>
        /// Save the current OpenGL render states and matrices.
        ///
        /// This function can be used when you mix SFML drawing
        /// and direct OpenGL rendering. Combined with PopGLStates,
        /// it ensures that:
        /// \li SFML's internal states are not messed up by your OpenGL code
        /// \li your OpenGL states are not modified by a call to a SFML function
        ///
        /// More specifically, it must be used around code that
        /// calls Draw functions. Example:
        ///
        /// // OpenGL code here...
        /// window.PushGLStates();
        /// window.Draw(...);
        /// window.Draw(...);
        /// window.PopGLStates();
        /// // OpenGL code here...
        ///
        /// Note that this function is quite expensive: it saves all the
        /// possible OpenGL states and matrices, even the ones you
        /// don't care about. Therefore it should be used wisely.
        /// It is provided for convenience, but the best results will
        /// be achieved if you handle OpenGL states yourself (because
        /// you know which states have really changed, and need to be
        /// saved and restored). Take a look at the ResetGLStates
        /// function if you do so.
        /// </summary>
        public void PushGlStates()
        {
            sfRenderWindow_pushGLStates(CPtr);
        }
        
        /// <summary>
        /// Restore the previously saved OpenGL render states and matrices.
        ///
        /// See the description of PushGLStates to get a detailed
        /// description of these functions.
        /// </summary>
        public void PopGlStates()
        {
            sfRenderWindow_popGLStates(CPtr);
        }
        
        /// <summary>
        /// Reset the internal OpenGL states so that the target is ready for drawing.
        ///
        /// This function can be used when you mix SFML drawing
        /// and direct OpenGL rendering, if you choose not to use
        /// PushGLStates/PopGLStates. It makes sure that all OpenGL
        /// states needed by SFML are set, so that subsequent Draw()
        /// calls will work as expected.
        ///
        /// Example:
        ///
        /// // OpenGL code here...
        /// glPushAttrib(...);
        /// window.ResetGLStates();
        /// window.Draw(...);
        /// window.Draw(...);
        /// glPopAttrib(...);
        /// // OpenGL code here...
        /// </summary>
        public void ResetGlStates()
        {
            sfRenderWindow_resetGLStates(CPtr);
        }
        
        /// <summary>
        /// Capture the current contents of the window into an image
        /// </summary>
        public Image Capture()
        {
            return new Image(sfRenderWindow_capture(CPtr));
        }
        
        /// <summary>
        /// Request the current window to be made the active
        /// foreground window
        /// </summary>
        public override void RequestFocus()
        {
            sfRenderWindow_requestFocus(CPtr);
        }
        
        /// <summary>
        /// Check whether the window has the input focus
        /// </summary>
        /// <returns>True if the window has focus, false otherwise</returns>
        public override bool HasFocus()
        {
            return sfRenderWindow_hasFocus(CPtr);
        }
        
        ///// <summary>
        ///// Internal function to get the next event
        ///// </summary>
        ///// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
        ///// <returns>True if there was an event, false otherwise</returns>
        //protected override bool PollEvent(out Event eventToFill)
        //{
        //    return sfRenderWindow_pollEvent(CPtr, out eventToFill);
        //}
        
        ///// <summary>
        ///// Internal function to get the next event (blocking)
        ///// </summary>
        ///// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
        ///// <returns>False if any error occured</returns>
        //protected override bool WaitEvent(out Event eventToFill)
        //{
        //    return sfRenderWindow_waitEvent(CPtr, out eventToFill);
        //}
        
        /// <summary>
        /// Internal function to get the mouse position relative to the window.
        /// This function is public because it is called by another class,
        /// it is not meant to be called by users.
        /// </summary>
        /// <returns>Relative mouse position</returns>
        protected internal override Vector2Di InternalGetMousePosition()
        {
            return sfMouse_getPositionRenderWindow(CPtr);
        }
        
        /// <summary>
        /// Internal function to set the mouse position relative to the window.
        /// This function is public because it is called by another class,
        /// it is not meant to be called by users.
        /// </summary>
        /// <param name="position">Relative mouse position</param>
        protected internal override void InternalSetMousePosition(Vector2Di position)
        {
            sfMouse_setPositionRenderWindow(position, CPtr);
        }
        
        /// <summary>
        /// Internal function to get the touch position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <param name="finger">Finger index</param>
        /// <returns>Relative touch position</returns>
        protected internal override Vector2Di InternalGetTouchPosition(int finger)
        {
            return sfTouch_getPositionRenderWindow(finger, CPtr);
        }
        
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            sfRenderWindow_destroy(CPtr);

            if (disposing)
            {
                _defaultView.Dispose();
            }
        }
        
        /// <summary>
        /// Do common initializations
        /// </summary>
        private void Initialize()
        {
            _defaultView = new View(sfRenderWindow_getDefaultView(CPtr));
            GC.SuppressFinalize(_defaultView);
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[RenderWindow] " +
                                 $"Position({Position}) " +
                                 $"Settings({Settings}) " +
                                 $"DefaultView({DefaultView}) " +
                                 $"View({GetView()})");
        }
    }
}
