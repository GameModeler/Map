using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Map.Core;
using Map.Core.Structs;
using Map.Graphics.Structs;
using Map.Window;
using Map.Window.Exceptions;

namespace Map.Graphics
{
    /// <summary>
    /// Image stored and rendered by the GPU can be used for drawing
    /// </summary>
    public class Texture : ObjectBase
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_create(int width, int height);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_createFromFile(string filename, ref IntRect area);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_createFromStream(IntPtr stream, ref IntRect area);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_createFromImage(IntPtr image, ref IntRect area);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_createFromMemory(IntPtr data, long size, ref IntRect area);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_copy(IntPtr texture);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_destroy(IntPtr texture);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfTexture_getSize(IntPtr texture);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_copyToImage(IntPtr texture);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfTexture_updateFromPixels(IntPtr texture, byte* pixels, int width, int height, int x, int y);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_updateFromImage(IntPtr texture, IntPtr image, int x, int y);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_updateFromWindow(IntPtr texture, IntPtr window, int x, int y);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_updateFromRenderWindow(IntPtr texture, IntPtr renderWindow, int x, int y);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_bind(IntPtr texture);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_setSmooth(IntPtr texture, bool smooth);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfTexture_isSmooth(IntPtr texture);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_setRepeated(IntPtr texture, bool repeated);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfTexture_isRepeated(IntPtr texture);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern FloatRect sfTexture_getTexCoords(IntPtr texture, IntRect rectangle);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern int sfTexture_getMaximumSize();

        #endregion

        #region Attributes

        private readonly bool _external;

        #endregion

        #region Properties

        /// <summary>
        /// Control the smooth filter
        /// </summary>
        public bool Smooth
        {
            get => sfTexture_isSmooth(CPtr);
            set => sfTexture_setSmooth(CPtr, value);
        }
        
        /// <summary>
        /// Control the repeat mode
        /// </summary>
        public bool Repeated
        {
            get => sfTexture_isRepeated(CPtr);
            set => sfTexture_setRepeated(CPtr, value);
        }
        
        /// <summary>
        /// Size of the texture, in pixels
        /// </summary>
        public Vector2Di Size => sfTexture_getSize(CPtr);
        
        /// <summary>
        /// Maximum texture size allowed
        /// </summary>
        public static int MaximumSize => sfTexture_getMaximumSize();

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the texture
        /// </summary>
        /// <param name="width">Texture width</param>
        /// <param name="height">Texture height</param>
        /// <exception cref="LoadingFailedException" />
        public Texture(int width, int height) : base(sfTexture_create(width, height))
        {
            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture");
            }
        }
        
        /// <summary>
        /// Construct the texture from a file
        /// </summary>
        /// <param name="filename">Path of the image file to load</param>
        /// <exception cref="LoadingFailedException" />
        public Texture(string filename) : this(filename, new IntRect(0, 0, 0, 0)) {}
        
        /// <summary>
        /// Construct the texture from a file
        /// </summary>
        /// <param name="filename">Path of the image file to load</param>
        /// <param name="area">Area of the image to load</param>
        /// <exception cref="LoadingFailedException" />
        public Texture(string filename, IntRect area) : base(sfTexture_createFromFile(filename, ref area))
        {
            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture", filename);
            }
        }
        
        /// <summary>
        /// Construct the texture from a file in a stream
        /// </summary>
        /// <param name="stream">Stream containing the file contents</param>
        /// <exception cref="LoadingFailedException" />
        public Texture(Stream stream) : this(stream, new IntRect(0, 0, 0, 0)) {}
        
        /// <summary>
        /// Construct the texture from a file in a stream
        /// </summary>
        /// <param name="stream">Stream containing the file contents</param>
        /// <param name="area">Area of the image to load</param>
        /// <exception cref="LoadingFailedException" />
        public Texture(Stream stream, IntRect area) : base(IntPtr.Zero)
        {
            using (var adaptor = new StreamAdaptor(stream))
            {
                CPtr = sfTexture_createFromStream(adaptor.InputStreamPtr, ref area);
            }

            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture");
            }
        }
        
        /// <summary>
        /// Construct the texture from an image
        /// </summary>
        /// <param name="image">Image to load to the texture</param>
        /// <exception cref="LoadingFailedException" />
        public Texture(Image image) : this(image, new IntRect(0, 0, 0, 0)) {}
        
        /// <summary>
        /// Construct the texture from an image
        /// </summary>
        /// <param name="image">Image to load to the texture</param>
        /// <param name="area">Area of the image to load</param>
        /// <exception cref="LoadingFailedException" />
        public Texture(Image image, IntRect area) : base(sfTexture_createFromImage(image.CPtr, ref area))
        {
            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture");
            }
        }
        
        /// <summary>
        /// Construct the texture from a file in memory
        /// </summary>
        /// <param name="bytes">Byte array containing the file contents</param>
        /// <exception cref="LoadingFailedException" />
        public Texture(IReadOnlyCollection<byte> bytes) : base(IntPtr.Zero)
        {
            var pin = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                var rect = new IntRect(0, 0, 0, 0);
                CPtr = sfTexture_createFromMemory(pin.AddrOfPinnedObject(), Convert.ToInt64(bytes.Count), ref rect);
            }
            finally
            {
                pin.Free();
            }

            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture");
            }
        }
        
        /// <summary>
        /// Construct the texture from another texture
        /// </summary>
        /// <param name="copy">Texture to copy</param>
        public Texture(Texture copy) : base(sfTexture_copy(copy.CPtr)) {}

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="cPointer">Pointer to the object in C library</param>
        internal Texture(IntPtr cPointer) : base(cPointer)
        {
            _external = true;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Copy a texture's pixels to an image
        /// </summary>
        /// <returns>Image containing the texture's pixels</returns>
        public Image CopyToImage()
        {
            return new Image(sfTexture_copyToImage(CPtr));
        }
        
        /// <summary>
        /// Update a texture from an array of pixels
        /// </summary>
        /// <param name="pixels">Array of pixels to copy to the texture</param>
        public void Update(byte[] pixels)
        {
            var size = Size;
            Update(pixels, size.X, size.Y, 0, 0);
        }
        
        /// <summary>
        /// Update a texture from an array of pixels
        /// </summary>
        /// <param name="pixels">Array of pixels to copy to the texture</param>
        /// <param name="width">Width of the pixel region contained in pixels</param>
        /// <param name="height">Height of the pixel region contained in pixels</param>
        /// <param name="x">X offset in the texture where to copy the source pixels</param>
        /// <param name="y">Y offset in the texture where to copy the source pixels</param>
        public void Update(byte[] pixels, int width, int height, int x, int y)
        {
            unsafe
            {
                fixed (byte* ptr = pixels)
                {
                    sfTexture_updateFromPixels(CPtr, ptr, width, height, x, y);
                }
            }
        }
        
        /// <summary>
        /// Update a texture from an image
        /// </summary>
        /// <param name="image">Image to copy to the texture</param>
        public void Update(Image image)
        {
            Update(image, 0, 0);
        }
        
        /// <summary>
        /// Update a texture from an image
        /// </summary>
        /// <param name="image">Image to copy to the texture</param>
        /// <param name="x">X offset in the texture where to copy the source pixels</param>
        /// <param name="y">Y offset in the texture where to copy the source pixels</param>
        public void Update(Image image, int x, int y)
        {
            sfTexture_updateFromImage(CPtr, image.CPtr, x, y);
        }
        
        /// <summary>
        /// Update a texture from the contents of a window
        /// </summary>
        /// <param name="window">Window to copy to the texture</param>
        public void Update(Window.Window window)
        {
            Update(window, 0, 0);
        }
        
        /// <summary>
        /// Update a texture from the contents of a window
        /// </summary>
        /// <param name="window">Window to copy to the texture</param>
        /// <param name="x">X offset in the texture where to copy the source pixels</param>
        /// <param name="y">Y offset in the texture where to copy the source pixels</param>
        public void Update(Window.Window window, int x, int y)
        {
            sfTexture_updateFromWindow(CPtr, window.CPtr, x, y);
        }
        
        /// <summary>
        /// Update a texture from the contents of a render-window
        /// </summary>
        /// <param name="window">Render-window to copy to the texture</param>
        public void Update(RenderWindow window)
        {
            Update(window, 0, 0);
        }
        
        /// <summary>
        /// Update a texture from the contents of a render-window
        /// </summary>
        /// <param name="window">Render-window to copy to the texture</param>
        /// <param name="x">X offset in the texture where to copy the source pixels</param>
        /// <param name="y">Y offset in the texture where to copy the source pixels</param>
        public void Update(RenderWindow window, int x, int y)
        {
            sfTexture_updateFromRenderWindow(CPtr, window.CPtr, x, y);
        }
        
        /// <summary>
        /// Bind a texture for rendering
        /// </summary>
        /// <param name="texture">Shader to bind (can be null to use no texture)</param>
        public static void Bind(Texture texture)
        {
            sfTexture_bind(texture?.CPtr ?? IntPtr.Zero);
        }

        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            if (_external)
            {
                return;
            }

            if (!disposing)
            {
                Context.GlobalContext.SetActive(true);
            }

            sfTexture_destroy(CPtr);

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
            return string.Format("[Texture] " +
                                 $"Size({Size}) " +
                                 $"Smooth({Smooth}) " +
                                 $"Repeated({Repeated})");
        }

        #endregion
    }
}
