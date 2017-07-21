using System;
using System.Runtime.InteropServices;
using System.Security;
using Map.Graphics.Interfaces;
using Map.Graphics.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Define a sprite (texture, transformations, colors)
    /// </summary>
    public class Sprite : Transformable, IDrawable
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfSprite_create();

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfSprite_copy(IntPtr sprite);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfSprite_destroy(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfSprite_setColor(IntPtr cPointer, Color color);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Color sfSprite_getColor(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_drawSprite(IntPtr cPointer, IntPtr pprite, ref RenderStates.MarshalData states);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_drawSprite(IntPtr cPointer, IntPtr sprite, ref RenderStates.MarshalData states);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfSprite_setTexture(IntPtr cPointer, IntPtr texture, bool adjustToNewSize);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfSprite_setTextureRect(IntPtr cPointer, IntRect rectangle);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntRect sfSprite_getTextureRect(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern FloatRect sfSprite_getLocalBounds(IntPtr cPointer);

        #endregion

        #region Attributes

        private Texture _texture;

        #endregion

        #region Properties

        /// <summary>
        /// Global color of the object
        /// </summary>
        public Color Color
        {
            get => sfSprite_getColor(CPtr);
            set => sfSprite_setColor(CPtr, value);
        }
        
        /// <summary>
        /// Source texture displayed by the sprite
        /// </summary>
        public Texture Texture
        {
            get => _texture;
            set
            {
                _texture = value;
                sfSprite_setTexture(CPtr, value?.CPtr ?? IntPtr.Zero, false);
            }
        }
        
        /// <summary>
        /// Sub-rectangle of the source image displayed by the sprite
        /// </summary>
        public IntRect TextureRect
        {
            get => sfSprite_getTextureRect(CPtr);
            set => sfSprite_setTextureRect(CPtr, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Sprite() : base(sfSprite_create()) {}
        
        /// <summary>
        /// Construct the sprite from a source texture
        /// </summary>
        /// <param name="texture">Source texture to assign to the sprite</param>
        public Sprite(Texture texture) : base(sfSprite_create())
        {
            Texture = texture;
        }
        
        /// <summary>
        /// Construct the sprite from a source texture
        /// </summary>
        /// <param name="texture">Source texture to assign to the sprite</param>
        /// <param name="rectangle">Sub-rectangle of the texture to assign to the sprite</param>
        public Sprite(Texture texture, IntRect rectangle) : base(sfSprite_create())
        {
            Texture = texture;
            TextureRect = rectangle;
        }
        
        /// <summary>
        /// Construct the sprite from another sprite
        /// </summary>
        /// <param name="copy">Sprite to copy</param>
        public Sprite(Sprite copy) : base(sfSprite_copy(copy.CPtr))
        {
            Origin = copy.Origin;
            Position = copy.Position;
            Rotation = copy.Rotation;
            Scale = copy.Scale;

            Texture = copy.Texture;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the local bounding rectangle of the entity.
        ///
        /// The returned rectangle is in local coordinates, which means
        /// that it ignores the transformations (translation, rotation,
        /// scale, ...) that are applied to the entity.
        /// In other words, this function returns the bounds of the
        /// entity in the entity's coordinate system.
        /// </summary>
        /// <returns>Local bounding rectangle of the entity</returns>
        public FloatRect GetLocalBounds()
        {
            return sfSprite_getLocalBounds(CPtr);
        }
        
        /// <summary>
        /// Get the global bounding rectangle of the entity.
        ///
        /// The returned rectangle is in global coordinates, which means
        /// that it takes in account the transformations (translation,
        /// rotation, scale, ...) that are applied to the entity.
        /// In other words, this function returns the bounds of the
        /// sprite in the global 2D world's coordinate system.
        /// </summary>
        /// <returns>Global bounding rectangle of the entity</returns>
        public FloatRect GetGlobalBounds()
        {
            // we don't use the native getGlobalBounds function,
            // because we override the object's transform
            return Transform.TransformRect(GetLocalBounds());
        }
        
        /// <summmary>
        /// Draw the sprite to a render target
        /// </summmary>
        /// <param name="target">Render target to draw to</param>
        /// <param name="states">Current render states</param>
        public void Draw(IRenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            var marshaledStates = states.Marshal();
            var window = target as RenderWindow;

            if (window != null)
            {
                sfRenderWindow_drawSprite(window.CPtr, CPtr, ref marshaledStates);
            }
            else if (target is RenderTexture)
            {
                sfRenderTexture_drawSprite(((RenderTexture) target).CPtr, CPtr, ref marshaledStates);
            }
        }
        
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        protected override void Destroy(bool disposing)
        {
            sfSprite_destroy(CPtr);
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[Sprite] " +
                                 $"Color({Color}) " +
                                 $"Texture({Texture}) " +
                                 $"TextureRect({TextureRect})");
        }

        #endregion
    }
}
