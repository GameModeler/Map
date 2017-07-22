using System;
using System.Runtime.InteropServices;
using System.Security;
using Map.Core;
using Map.Core.Structs;
using Map.Graphics.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Define a view (position, size, etc...)
    /// Can be considered as a 2D camera
    /// More information on coordinates ans undistorted rendering in Map.Graphics.Transformable
    /// </summary>
    public class View : ObjectBase
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfView_create();

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfView_createFromRect(FloatRect rectangle);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfView_copy(IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_destroy(IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_setCenter(IntPtr view, Vector2Df center);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_setSize(IntPtr view, Vector2Df size);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_setRotation(IntPtr view, float angle);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_setViewport(IntPtr view, FloatRect viewport);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_reset(IntPtr view, FloatRect rectangle);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Df sfView_getCenter(IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Df sfView_getSize(IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern float sfView_getRotation(IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern FloatRect sfView_getViewport(IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_move(IntPtr view, Vector2Df offset);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_rotate(IntPtr view, float angle);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfView_zoom(IntPtr view, float factor);

        #endregion

        #region Attributes

        private readonly bool _external;

        #endregion

        #region Properties

        /// <summary>
        /// Center of the view
        /// </summary>
        public Vector2Df Center
        {
            get => sfView_getCenter(CPtr);
            set => sfView_setCenter(CPtr, value);
        }

        /// <summary>
        /// Half-size of the view
        /// </summary>
        public Vector2Df Size
        {
            get => sfView_getSize(CPtr);
            set => sfView_setSize(CPtr, value);
        }

        /// <summary>
        /// Rotation of the view (in degrees)
        /// </summary>
        public float Rotation
        {
            get => sfView_getRotation(CPtr);
            set => sfView_setRotation(CPtr, value);
        }

        /// <summary>
        /// Target viewport of the view
        /// (defined as a factor of the size of the target to which the view is applied)
        /// </summary>
        public FloatRect Viewport
        {
            get => sfView_getViewport(CPtr);
            set => sfView_setViewport(CPtr, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default view (1000x1000)
        /// </summary>
        public View() :  base(sfView_create()) {}
        
        /// <summary>
        /// Construct the view from a rectangle
        /// </summary>
        /// <param name="viewRect">Rectangle defining the position and size of the view</param>
        public View(FloatRect viewRect) : base(sfView_createFromRect(viewRect)) {}

        /// <summary>
        /// Construct the view from its center and size
        /// </summary>
        /// <param name="center">Center of the view</param>
        /// <param name="size">Size of the view</param>
        public View(Vector2Df center, Vector2Df size) : base(sfView_create())
        {
            Center = center;
            Size = size;
        }
        
        /// <summary>
        /// Construct the view from another view
        /// </summary>
        /// <param name="copy">View to copy</param>
        public View(View copy) : base(sfView_copy(copy.CPtr)) {}
        
        /// <summary>
        /// Internal constructor for other classes which need to manipulate raw views
        /// </summary>
        /// <param name="cPointer">Direct pointer to the view object in the C library</param>
        internal View(IntPtr cPointer) : base(cPointer)
        {
            _external = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Rebuild the view from a rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle defining the position and size of the view</param>
        public void Reset(FloatRect rectangle)
        {
            sfView_reset(CPtr, rectangle);
        }
        
        /// <summary>
        /// Move the view
        /// </summary>
        /// <param name="offset">Offset to move the view</param>
        public void Move(Vector2Df offset)
        {
            sfView_move(CPtr, offset);
        }
        
        /// <summary>
        /// Rotate the view
        /// </summary>
        /// <param name="angle">Angle of rotation (in degrees)</param>
        public void Rotate(float angle)
        {
            sfView_rotate(CPtr, angle);
        }
        
        /// <summary>
        /// Resize the view rectangle to simulate a zoom / unzoom effect
        /// </summary>
        /// <param name="factor">Zoom factor to apply, relative to the current zoom</param>
        public void Zoom(float factor)
        {
            sfView_zoom(CPtr, factor);
        }

        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            if (!_external)
            {
                sfView_destroy(CPtr);
            }
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[View] " +
                                 $"Center({Center}) " +
                                 $"Size({Size}) " +
                                 $"Rotation({Rotation}) " +
                                 $"Viewport({Viewport})");
        }

        #endregion
    }
}
