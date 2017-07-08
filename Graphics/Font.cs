using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Map.Core;
using Map.Graphics.Structs;
using Map.Window;
using Map.Window.Exceptions;

namespace Map.Graphics
{
    /// <summary>
    /// Low-level class to load and manipulate character fonts
    /// Meant to be used by String2D
    /// </summary>
    public class Font : ObjectBase
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfFont_createFromFile(string filename);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfFont_createFromStream(IntPtr stream);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfFont_createFromMemory(IntPtr data, long size);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfFont_copy(IntPtr font);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfFont_destroy(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Glyph sfFont_getGlyph(IntPtr cPointer, int codePoint, int characterSize, bool bold);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern float sfFont_getKerning(IntPtr cPointer, int first, int second, int characterSize);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern float sfFont_getLineSpacing(IntPtr cPointer, int characterSize);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern float sfFont_getUnderlinePosition(IntPtr cPointer, int characterSize);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern float sfFont_getUnderlineThickness(IntPtr cPointer, int characterSize);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfFont_getTexture(IntPtr cPointer, int characterSize);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern InfoMarshalData sfFont_getInfo(IntPtr cPointer);

        #endregion

        #region Structs

        /// <summary>
        /// Holds various information about a font
        /// </summary>
        public struct Info
        {
            /// <summary>
            /// The font family
            /// </summary>
            public string Family;
        }

        /// <summary>
        /// Internal struct user to marshal the font info struct from unmanaged code
        /// </summary>
        internal struct InfoMarshalData
        {
            public IntPtr Family;
        }

        #endregion

        #region Attributes

        private readonly StreamAdaptor _stream;
        private readonly Dictionary<int, Texture> _textures = new Dictionary<int, Texture>();

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the font from a file
        /// </summary>
        /// <param name="filename">Font file to load</param>
        /// <exception cref="LoadingFailedException" />
        public Font(string filename) : base(sfFont_createFromFile(filename))
        {
            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("font", filename);
            }
        }
        
        /// <summary>
        /// Construct the font from a custom stream
        /// </summary>
        /// <param name="stream">Source stream to read from</param>
        /// <exception cref="LoadingFailedException" />
        public Font(Stream stream) : base(IntPtr.Zero)
        {
            _stream = new StreamAdaptor(stream);
            CPtr = sfFont_createFromStream(_stream.InputStreamPtr);

            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("font");
            }
        }
        
        /// <summary>
        /// Construct the font from a file in memory
        /// </summary>
        /// <param name="bytes">Byte array containing the file contents</param>
        /// <exception cref="LoadingFailedException" />
        public Font(byte[] bytes) : base(IntPtr.Zero)
        {
            var pin = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                CPtr = sfFont_createFromMemory(pin.AddrOfPinnedObject(), Convert.ToInt64(bytes.Length));
            }
            finally
            {
                pin.Free();
            }

            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("font");
            }
        }
        
        /// <summary>
        /// Construct the font from another font
        /// </summary>
        /// <param name="copy">Font to copy</param>
        public Font(Font copy) : base(sfFont_copy(copy.CPtr)) {}

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="cPointer">Pointer to the object in C library</param>
        private Font(IntPtr cPointer) : base(cPointer) {}

        #endregion

        #region Methods

        /// <summary>
        /// Get a glyph in the font
        /// </summary>
        /// <param name="codePoint">Unicode code point of the character to get</param>
        /// <param name="characterSize">Character size</param>
        /// <param name="bold">Retrieve the bold version or the regular one?</param>
        /// <returns>The glyph corresponding to the character</returns>
        public Glyph GetGlyph(int codePoint, int characterSize, bool bold)
        {
            return sfFont_getGlyph(CPtr, codePoint, characterSize, bold);
        }
        
        /// <summary>
        /// Get the kerning offset between two glyphs
        /// </summary>
        /// <param name="first">Unicode code point of the first character</param>
        /// <param name="second">Unicode code point of the second character</param>
        /// <param name="characterSize">Character size</param>
        /// <returns>Kerning offset, in pixels</returns>
        public float GetKerning(int first, int second, int characterSize)
        {
            return sfFont_getKerning(CPtr, first, second, characterSize);
        }
        
        /// <summary>
        /// Get spacing between two consecutive lines
        /// </summary>
        /// <param name="characterSize">Character size</param>
        /// <returns>Line spacing, in pixels</returns>
        public float GetLineSpacing(int characterSize)
        {
            return sfFont_getLineSpacing(CPtr, characterSize);
        }
        
        /// <summary>
        /// Get the position of the underline
        /// </summary>
        /// <param name="characterSize">Character size</param>
        /// <returns>Underline position, in pixels</returns>
        public float GetUnderlinePosition(int characterSize)
        {
            return sfFont_getUnderlinePosition(CPtr, characterSize);
        }
        
        /// <summary>
        /// Get the thickness of the underline
        /// </summary>
        /// <param name="characterSize">Character size</param>
        /// <returns>Underline thickness, in pixels</returns>
        public float GetUnderlineThickness(int characterSize)
        {
            return sfFont_getUnderlineThickness(CPtr, characterSize);
        }
        
        /// <summary>
        /// Get the texture containing the glyphs of a given size
        /// </summary>
        /// <param name="characterSize">Character size</param>
        /// <returns>Texture storing the glyphs for the given size</returns>
        public Texture GetTexture(int characterSize)
        {
            _textures[characterSize] = new Texture(sfFont_getTexture(CPtr, characterSize));
            return _textures[characterSize];
        }
        
        /// <summary>
        /// Get the font information
        /// </summary>
        /// <returns>A structure that holds the font information</returns>
        public Info GetInfo()
        {
            var data = sfFont_getInfo(CPtr);
            var info = new Info
            {
                Family = Marshal.PtrToStringAnsi(data.Family)
            };


            return info;
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

            sfFont_destroy(CPtr);

            if (disposing)
            {
                foreach (var texture in _textures.Values)
                {
                    texture.Dispose();
                }

                _stream?.Dispose();
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
            return "[Font]";
        }

        #endregion
    }
}
