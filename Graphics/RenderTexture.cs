﻿using System;
using System.Runtime.InteropServices;
using System.Security;
using Map.Core;
using Map.Core.Structs;
using Map.Graphics.Enums;
using Map.Graphics.Interfaces;
using Map.Graphics.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Target for off-screen 2D rendering into an texture
    /// </summary>
    public class RenderTexture : ObjectBase, IRenderTarget
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderTexture_create(int width, int height, bool depthBuffer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_destroy(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_clear(IntPtr cPointer, Color clearColor);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfRenderTexture_getSize(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderTexture_setActive(IntPtr cPointer, bool active);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderTexture_saveGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderTexture_restoreGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderTexture_display(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_setView(IntPtr cPointer, IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderTexture_getView(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderTexture_getDefaultView(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntRect sfRenderTexture_getViewport(IntPtr cPointer, IntPtr targetView);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfRenderTexture_mapCoordsToPixel(IntPtr cPointer, Vector2Df point, IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Df sfRenderTexture_mapPixelToCoords(IntPtr cPointer, Vector2Di point, IntPtr view);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfRenderTexture_getTexture(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_setSmooth(IntPtr texture, bool smooth);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfRenderTexture_isSmooth(IntPtr texture);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfRenderTexture_drawPrimitives(IntPtr cPointer, Vertex* vertexPtr, int vertexCount, 
            PrimitiveType type, ref RenderStates.MarshalData renderStates);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_pushGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_popGLStates(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_resetGLStates(IntPtr cPointer);

        #endregion

        #region Attributes

        private readonly View _defaultView;

        #endregion

        #region Properties

        /// <summary>
        /// Size of the rendering region of the render texture
        /// </summary>
        public Vector2Di Size => sfRenderTexture_getSize(CPtr);
        
        /// <summary>
        /// Default view of the render texture
        /// </summary>
        public View DefaultView => new View(_defaultView);
        
        /// <summary>
        /// Target texture of the render texture
        /// </summary>
        public Texture Texture { get; }

        /// <summary>
        /// Control the smooth filter
        /// </summary>
        public bool Smooth
        {
            get => sfRenderTexture_isSmooth(CPtr);
            set => sfRenderTexture_setSmooth(CPtr, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create the render-texture with the given dimensions
        /// </summary>
        /// <param name="width">Width of the render-texture</param>
        /// <param name="height">Height of the render-texture</param>
        public RenderTexture(int width, int height) : this(width, height, false) {}
        
        /// <summary>
        /// Create the render-texture with the given dimensions and
        /// an optional depth-buffer attached
        /// </summary>
        /// <param name="width">Width of the render-texture</param>
        /// <param name="height">Height of the render-texture</param>
        /// <param name="depthBuffer">Do you want a depth-buffer attached?</param>
        public RenderTexture(int width, int height, bool depthBuffer) 
            : base(sfRenderTexture_create(width, height, depthBuffer))
        {
            _defaultView = new View(sfRenderTexture_getDefaultView(CPtr));
            Texture = new Texture(sfRenderTexture_getTexture(CPtr));
            GC.SuppressFinalize(DefaultView);
            GC.SuppressFinalize(Texture);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Activate of deactivate the render texture as the current target
        /// for rendering
        /// </summary>
        /// <param name="active">True to activate, false to deactivate (true by default)</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        public bool SetActive(bool active)
        {
            return sfRenderTexture_setActive(CPtr, active);
        }
        
        /// <summary>
        /// Return the current active view
        /// </summary>
        /// <returns>The current view</returns>
        public View GetView()
        {
            return new View(sfRenderTexture_getView(CPtr));
        }
        
        /// <summary>
        /// Change the current active view
        /// </summary>
        /// <param name="view">New view</param>
        public void SetView(View view)
        {
            sfRenderTexture_setView(CPtr, view.CPtr);
        }
        
        /// <summary>
        /// Get the viewport of a view applied to this target
        /// </summary>
        /// <param name="view">Target view</param>
        /// <returns>Viewport rectangle, expressed in pixels in the current target</returns>
        public IntRect GetViewport(View view)
        {
            return sfRenderTexture_getViewport(CPtr, view.CPtr);
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
            return sfRenderTexture_mapPixelToCoords(CPtr, point, view?.CPtr ?? IntPtr.Zero);
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

        ////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////
        public Vector2Di MapCoordsToPixel(Vector2Df point, View view)
        {
            return sfRenderTexture_mapCoordsToPixel(CPtr, point, view?.CPtr ?? IntPtr.Zero);
        }
        
        /// <summary>
        /// Clear the entire render texture with black color
        /// </summary>
        public void Clear()
        {
            sfRenderTexture_clear(CPtr, Color.Black);
        }
        
        /// <summary>
        /// Clear the entire render texture with a single color
        /// </summary>
        /// <param name="color">Color to use to clear the texture</param>
        public void Clear(Color color)
        {
            sfRenderTexture_clear(CPtr, color);
        }
        
        /// <summary>
        /// Update the contents of the target texture
        /// </summary>
        public void Display()
        {
            sfRenderTexture_display(CPtr);
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
                    sfRenderTexture_drawPrimitives(CPtr, vertexPtr + start, count, type, ref marshaledStates);
                }
            }
        }
        
        /// <summary>
        /// Save the current OpenGL render states and matrices.
        ///
        /// This function can be used when you mix SFML drawing
        /// and direct OpenGL rendering. Combined with PopGLStates,
        /// it ensures that:
        ///  - SFML's internal states are not messed up by your OpenGL code
        ///  - Your OpenGL states are not modified by a call to a SFML function
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
            sfRenderTexture_pushGLStates(CPtr);
        }
        
        /// <summary>
        /// Restore the previously saved OpenGL render states and matrices.
        ///
        /// See the description of PushGLStates to get a detailed
        /// description of these functions.
        /// </summary>
        public void PopGlStates()
        {
            sfRenderTexture_popGLStates(CPtr);
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
            sfRenderTexture_resetGLStates(CPtr);
        }
        
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        protected override void Destroy(bool disposing)
        {
            if (!disposing)
            {
                Context.GlobalContext.SetActive(true);
            }

            sfRenderTexture_destroy(CPtr);

            if (disposing)
            {
                DefaultView.Dispose();
                Texture.Dispose();
            }

            if (!disposing)
            {
                Context.GlobalContext.SetActive(false);
            }
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[RenderTexture] " +
                                 $"Size({Size}) " +
                                 $"Texture({Texture}) " +
                                 $"DefaultView({DefaultView}) " +
                                 $"View({GetView()})");
        }

        #endregion
    }
}
