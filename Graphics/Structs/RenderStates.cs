using System;
using System.Runtime.InteropServices;

namespace Map.Graphics.Structs
{
    /// <summary>
    /// Define the states used for drawing to a RenderTarget
    /// </summary>
    public struct RenderStates
    {
        #region Properties

        /// <summary>
        /// Blending mode
        /// </summary>
        public BlendMode BlendMode { get; set; }

        /// <summary>
        /// Transform
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// Texture
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// Shader
        /// </summary>
        public Shader Shader  { get; set; }

        /// <summary>
        /// Special instance holding the default render states
        /// </summary>
        public static RenderStates Default => new RenderStates(BlendMode.Alpha, Transform.Identity, null, null);

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        internal struct MarshalData
        {
            public BlendMode blendMode;
            public Transform transform;
            public IntPtr texture;
            public IntPtr shader;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a default set of render states with a custom blend mode
        /// </summary>
        /// <param name="blendMode">Blend mode to use</param>
        public RenderStates(BlendMode blendMode) : this(blendMode, Transform.Identity, null, null) {}
        
        /// <summary>
        /// Construct a default set of render states with a custom transform
        /// </summary>
        /// <param name="transform">Transform to use</param>
        public RenderStates(Transform transform) :  this(BlendMode.Alpha, transform, null, null) {}
        
        /// <summary>
        /// Construct a default set of render states with a custom texture
        /// </summary>
        /// <param name="texture">Texture to use</param>
        public RenderStates(Texture texture) : this(BlendMode.Alpha, Transform.Identity, texture, null) {}
        
        /// <summary>
        /// Construct a default set of render states with a custom shader
        /// </summary>
        /// <param name="shader">Shader to use</param>
        public RenderStates(Shader shader) : this(BlendMode.Alpha, Transform.Identity, null, shader) {}
        
        /// <summary>
        /// Construct a set of render states with all its attributes
        /// </summary>
        /// <param name="blendMode">Blend mode to use</param>
        /// <param name="transform">Transform to use</param>
        /// <param name="texture">Texture to use</param>
        /// <param name="shader">Shader to use</param>
        public RenderStates(BlendMode blendMode, Transform transform, Texture texture, Shader shader)
        {
            BlendMode = blendMode;
            Transform = transform;
            Texture = texture;
            Shader = shader;
        }
        
        /// <summary>
        /// Construct a set of render states from another
        /// </summary>
        /// <param name="copy">States to copy</param>
        public RenderStates(RenderStates copy)
        {
            BlendMode = copy.BlendMode;
            Transform = copy.Transform;
            Texture = copy.Texture;
            Shader = copy.Shader;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a marshalled version of the instance that can
        /// directly be passed to the C API
        /// </summary>
        /// <returns></returns>
        internal MarshalData Marshal()
        {
            var data = new MarshalData
            {
                blendMode = BlendMode,
                transform = Transform,
                texture = Texture?.CPtr ?? IntPtr.Zero,
                shader = Shader?.CPtr ?? IntPtr.Zero
            };

            return data;
        }

        #endregion
    }
}
