using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Map.Core.Structs;
using Map.Graphics.Enums;
using Map.Graphics.Interfaces;
using Map.Graphics.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Define a 2D text that can be drawn on screen
    /// </summary>
    public class Text : Transformable, IDrawable
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfText_create();

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfText_copy(IntPtr text);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfText_destroy(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfText_setColor(IntPtr cPointer, Color color);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Color sfText_getColor(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderWindow_drawText(IntPtr cPointer, IntPtr text, ref RenderStates.MarshalData states);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfRenderTexture_drawText(IntPtr cPointer, IntPtr text, ref RenderStates.MarshalData states);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfText_setUnicodeString(IntPtr cPointer, IntPtr text);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfText_setFont(IntPtr cPointer, IntPtr font);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfText_setCharacterSize(IntPtr cPointer, int size);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfText_setStyle(IntPtr cPointer, TextStyle style);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfText_getString(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfText_getUnicodeString(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern int sfText_getCharacterSize(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern TextStyle sfText_getStyle(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern FloatRect sfText_getRect(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Df sfText_findCharacterPos(IntPtr cPointer, int index);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern FloatRect sfText_getLocalBounds(IntPtr cPointer);

        #endregion

        #region Attributes

        private Font _font;

        #endregion

        #region Properties

        /// <summary>
        /// Global color of the object
        /// </summary>
        public Color Color
        {
            get => sfText_getColor(CPtr);
            set => sfText_setColor(CPtr, value);
        }
        
        /// <summary>
        /// String which is displayed
        /// </summary>
        public string DisplayedString
        {
            get
            {
                // Get a pointer to the source string (UTF-32)
                var source = sfText_getUnicodeString(CPtr);

                // Find its length (find the terminating 0)
                var length = 0;
                unsafe
                {
                    for (var ptr = (int*) source.ToPointer(); *ptr != 0; ++ptr)
                    {
                        length++;
                    }
                }

                // Copy it to a byte array
                var sourceBytes = new byte[length * 4];
                Marshal.Copy(source, sourceBytes, 0, sourceBytes.Length);

                // Convert it to a C# string
                return Encoding.UTF32.GetString(sourceBytes);
            }

            set
            {
                // Copy the string to a null-terminated UTF-32 byte array
                var utf32 = Encoding.UTF32.GetBytes(value + '\0');

                // Pass it to the C API
                unsafe
                {
                    fixed (byte* ptr = utf32)
                    {
                        sfText_setUnicodeString(CPtr, (IntPtr) ptr);
                    }
                }
            }
        }
        
        /// <summary>
        /// Font used to display the text
        /// </summary>
        public Font Font
        {
            get => _font;
            set
            {
                _font = value;
                sfText_setFont(CPtr, value?.CPtr ?? IntPtr.Zero);
            }
        }
        
        /// <summary>
        /// Base size of characters
        /// </summary>
        public int CharacterSize
        {
            get => sfText_getCharacterSize(CPtr);
            set => sfText_setCharacterSize(CPtr, value);
        }
        
        /// <summary>
        /// Style of the text (see Styles enum)
        /// </summary>
        public TextStyle Style
        {
            get => sfText_getStyle(CPtr);
            set => sfText_setStyle(CPtr, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Text() : this("", null) {}
        
        /// <summary>
        /// Construct the text from a string and a font
        /// </summary>
        /// <param name="str">String to display</param>
        /// <param name="font">Font to use</param>
        public Text(string str, Font font) : this(str, font, 30) {}
        
        /// <summary>
        /// Construct the text from a string, font and size
        /// </summary>
        /// <param name="str">String to display</param>
        /// <param name="font">Font to use</param>
        /// <param name="characterSize">Base characters size</param>
        public Text(string str, Font font, int characterSize) : base(sfText_create())
        {
            DisplayedString = str;
            Font = font;
            CharacterSize = characterSize;
        }
        
        /// <summary>
        /// Construct the text from another text
        /// </summary>
        /// <param name="copy">Text to copy</param>
        public Text(Text copy) : base(sfText_copy(copy.CPtr))
        {
            Origin = copy.Origin;
            Position = copy.Position;
            Rotation = copy.Rotation;
            Scale = copy.Scale;

            Font = copy.Font;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Return the visual position of the Index-th character of the text,
        /// in coordinates relative to the text
        /// (note : translation, origin, rotation and scale are not applied)
        /// </summary>
        /// <param name="index">Index of the character</param>
        /// <returns>Position of the Index-th character (end of text if Index is out of range)</returns>
        public Vector2Df FindCharacterPos(int index)
        {
            return sfText_findCharacterPos(CPtr, index);
        }
        
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
            return sfText_getLocalBounds(CPtr);
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
        /// Draw the text to a render target
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
                sfRenderWindow_drawText(window.CPtr, CPtr, ref marshaledStates);
            }
            else if (target is RenderTexture)
            {
                sfRenderTexture_drawText(((RenderTexture) target).CPtr, CPtr, ref marshaledStates);
            }
        }
        
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            sfText_destroy(CPtr);
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[Text] " +
                                 $"Color({Color}) " +
                                 $"String({DisplayedString}) " +
                                 $"Font({Font}) " +
                                 $"CharacterSize({CharacterSize}) " +
                                 $"Style({Style})");
        }

        #endregion
    }
}
