using System;
using System.Runtime.InteropServices;
using System.Security;
using Map.Core;
using Map.Graphics.Enums;
using Map.Graphics.Interfaces;
using Map.Graphics.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Define a set of one or more 2D primitives
    /// </summary>
    public class VertexArray : ObjectBase, IDrawable
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfVertexArray_create();

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfVertexArray_copy(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfVertexArray_destroy(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern int sfVertexArray_getVertexCount(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe Vertex* sfVertexArray_getVertex(IntPtr cPointer, int index);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfVertexArray_clear(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfVertexArray_resize(IntPtr cPointer, int vertexCount);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfVertexArray_append(IntPtr cPointer, Vertex vertex);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfVertexArray_setPrimitiveType(IntPtr cPointer, PrimitiveType type);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern PrimitiveType sfVertexArray_getPrimitiveType(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern FloatRect sfVertexArray_getBounds(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_drawVertexArray(IntPtr cPointer, IntPtr vertexArray, ref RenderStates.MarshalData states);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_drawVertexArray(IntPtr cPointer, IntPtr vertexArray, ref RenderStates.MarshalData states);

        #endregion

        #region Properties

        /// <summary>
        /// Total vertex count
        /// </summary>
        public int VertexCount => sfVertexArray_getVertexCount(CPtr);
        
        /// <summary>
        /// Read-write access to vertices by their index.
        ///
        /// This function doesn't check index, it must be in range
        /// [0, VertexCount - 1]. The behaviour is undefined
        /// otherwise.
        /// </summary>
        /// <param name="index">Index of the vertex to get</param>
        /// <returns>Reference to the index-th vertex</returns>
        public Vertex this[int index]
        {
            get
            {
                unsafe
                {
                    return *sfVertexArray_getVertex(CPtr, index);
                }
            }
            set
            {
                unsafe
                {
                    *sfVertexArray_getVertex(CPtr, index) = value;
                }
            }
        }
        
        /// <summary>
        /// Type of primitives to draw
        /// </summary>
        public PrimitiveType PrimitiveType
        {
            get => sfVertexArray_getPrimitiveType(CPtr);
            set => sfVertexArray_setPrimitiveType(CPtr, value);
        }
        
        /// <summmary>
        /// Compute the bounding rectangle of the vertex array.
        ///
        /// This function returns the axis-aligned rectangle that
        /// contains all the vertices of the array.
        /// </summmary>
        public FloatRect Bounds => sfVertexArray_getBounds(CPtr);

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public VertexArray() : base(sfVertexArray_create()) {}
        
        /// <summary>
        /// Construct the vertex array with a type
        /// </summary>
        /// <param name="type">Type of primitives</param>
        public VertexArray(PrimitiveType type) : base(sfVertexArray_create())
        {
            PrimitiveType = type;
        }
        
        /// <summary>
        /// Construct the vertex array with a type and an initial number of vertices
        /// </summary>
        /// <param name="type">Type of primitives</param>
        /// <param name="vertexCount">Initial number of vertices in the array</param>
        public VertexArray(PrimitiveType type, int vertexCount) : base(sfVertexArray_create())
        {
            PrimitiveType = type;
            Resize(vertexCount);
        }
        
        /// <summary>
        /// Construct the vertex array from another vertex array
        /// </summary>
        /// <param name="copy">Transformable to copy</param>
        public VertexArray(VertexArray copy) : base(sfVertexArray_copy(copy.CPtr)) {}

        #endregion

        #region Methods

        /// <summary>
        /// Clear the vertex array
        /// </summary>
        public void Clear()
        {
            sfVertexArray_clear(CPtr);
        }
        
        /// <summary>
        /// Resize the vertex array
        /// 
        /// If \a vertexCount is greater than the current size, the previous
        /// vertices are kept and new (default-constructed) vertices are
        /// added.
        /// If \a vertexCount is less than the current size, existing vertices
        /// are removed from the array.
        /// </summary>
        /// <param name="vertexCount">New size of the array (number of vertices)</param>
        public void Resize(int vertexCount)
        {
            sfVertexArray_resize(CPtr, vertexCount);
        }
        
        /// <summary>
        /// Add a vertex to the array
        /// </summary>
        /// <param name="vertex">Vertex to add</param>
        public void Append(Vertex vertex)
        {
            sfVertexArray_append(CPtr, vertex);
        }
        
        /// <summmary>
        /// Draw the vertex array to a render target
        /// </summmary>
        /// <param name="target">Render target to draw to</param>
        /// <param name="states">Current render states</param>
        public void Draw(IRenderTarget target, RenderStates states)
        {
            var marshaledStates = states.Marshal();
            var window = target as RenderWindow;

            if (window != null)
            {
                sfRenderWindow_drawVertexArray(window.CPtr, CPtr, ref marshaledStates);
            }
            else if (target is RenderTexture)
            {
                sfRenderTexture_drawVertexArray(((RenderTexture) target).CPtr, CPtr, ref marshaledStates);
            }
        }
        
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            sfVertexArray_destroy(CPtr);
        }

        #endregion
    }
}
