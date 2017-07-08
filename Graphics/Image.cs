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
    /// Low-level class to load and manipulate images
    /// </summary>
    public class Image : ObjectBase
    {
        #region Imports

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfImage_createFromColor(int width, int height, Color color);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe IntPtr sfImage_createFromPixels(int width, int height, byte* pixels);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfImage_createFromFile(string filename);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfImage_createFromStream(IntPtr stream);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfImage_createFromMemory(IntPtr data, long size);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfImage_copy(IntPtr image);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfImage_destroy(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfImage_saveToFile(IntPtr cPointer, string filename);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfImage_createMaskFromColor(IntPtr cPointer, Color color, byte alpha);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfImage_copyImage(IntPtr cPointer, IntPtr source, int destX, int destY, IntRect sourceRect, bool applyAlpha);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfImage_setPixel(IntPtr cPointer, int x, int y, Color color);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Color sfImage_getPixel(IntPtr cPointer, int x, int y);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfImage_getPixelsPtr(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2Di sfImage_getSize(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern uint sfImage_flipHorizontally(IntPtr cPointer);

        [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern uint sfImage_flipVertically(IntPtr cPointer);

        #endregion

        #region Properties

        /// <summary>
        /// Size of the image (in pixels)
        /// </summary>
        public Vector2Di Size => sfImage_getSize(CPtr);

        /// <summary>
        /// Copy of the array of pixels (RGBA 8 bits integers components)
        /// </summary>
        public byte[] Pixels
        {
            get
            {
                var size = Size;
                var pixelsPtr = new byte[size.X * size.Y * 4];
                Marshal.Copy(sfImage_getPixelsPtr(CPtr), pixelsPtr, 0, pixelsPtr.Length);

                return pixelsPtr;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the image with black color
        /// </summary>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <exception cref="LoadingFailedException" />
        public Image(int width, int height) : this(width, height, Color.Black) {}
        
        /// <summary>
        /// Construct the image from a single color
        /// </summary>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="color">Color to fill the image with</param>
        /// <exception cref="LoadingFailedException" />
        public Image(int width, int height, Color color) : base(sfImage_createFromColor(width, height, color))
        {
            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("image");
            }
        }
        
        /// <summary>
        /// Construct the image from a file
        /// </summary>
        /// <param name="filename">Path of the image file to load</param>
        /// <exception cref="LoadingFailedException" />
        public Image(string filename) : base(sfImage_createFromFile(filename))
        {
            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("image", filename);
            }
        }
        
        /// <summary>
        /// Construct the image from a file in a stream
        /// </summary>
        /// <param name="stream">Stream containing the file contents</param>
        /// <exception cref="LoadingFailedException" />
        public Image(Stream stream) :  base(IntPtr.Zero)
        {
            using (var adaptor = new StreamAdaptor(stream))
            {
                CPtr = sfImage_createFromStream(adaptor.InputStreamPtr);
            }

            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("image");
            }
        }
        
        /// <summary>
        /// Construct the image from a file in memory
        /// </summary>
        /// <param name="bytes">Byte array containing the file contents</param>
        /// <exception cref="LoadingFailedException" />
        public Image(IReadOnlyCollection<byte> bytes) :
            base(IntPtr.Zero)
        {
            var pin = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                CPtr = sfImage_createFromMemory(pin.AddrOfPinnedObject(), Convert.ToInt64(bytes.Count));
            }
            finally
            {
                pin.Free();
            }

            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("image");
            }
        }
        
        /// <summary>
        /// Construct the image directly from an array of pixels
        /// </summary>
        /// <param name="pixels">2 dimensions array containing the pixels</param>
        /// <exception cref="LoadingFailedException" />
        public Image(Color[,] pixels) :  base(IntPtr.Zero)
        {
            var width = pixels.GetLength(0);
            var height = pixels.GetLength(1);

            // Transpose the array (.Net gives dimensions in reverse order of what the SFML library expects)
            var transposed = new Color[height, width];
            for (var x = 0; x < width; ++x)
            for (var y = 0; y < height; ++y)
                transposed[y, x] = pixels[x, y];

            unsafe
            {
                fixed (Color* pixelsPtr = transposed)
                {
                    CPtr = sfImage_createFromPixels(width, height, (byte*) pixelsPtr);
                }
            }

            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("image");
            }
        }
        
        /// <summary>
        /// Construct the image directly from an array of pixels
        /// </summary>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="pixels">Array containing the pixels</param>
        /// <exception cref="LoadingFailedException" />
        public Image(int width, int height, byte[] pixels) : base(IntPtr.Zero)
        {
            unsafe
            {
                fixed (byte* pixelsPtr = pixels)
                {
                    CPtr = sfImage_createFromPixels(width, height, pixelsPtr);
                }
            }

            if (CPtr == IntPtr.Zero)
            {
                throw new LoadingFailedException("image");
            }
        }
        
        /// <summary>
        /// Construct the image from another image
        /// </summary>
        /// <param name="copy">Image to copy</param>
        public Image(Image copy) : base(sfImage_copy(copy.CPtr)) {}

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="cPointer">Pointer to the object ic the C library</param>
        internal Image(IntPtr cPointer) : base(cPointer) {}

        #endregion

        #region Methods

        /// <summary>
        /// Save the contents of the image to a file
        /// </summary>
        /// <param name="filename">Path of the file to save (overwritten if already exist)</param>
        /// <returns>True if saving was successful, false otherwise</returns>
        public bool SaveToFile(string filename)
        {
            return sfImage_saveToFile(CPtr, filename);
        }
        
        /// <summary>
        /// Create a transparency mask from a specified color key
        /// </summary>
        /// <param name="color">Color to become transparent</param>
        public void CreateMaskFromColor(Color color)
        {
            CreateMaskFromColor(color, 0);
        }
        
        /// <summary>
        /// Create a transparency mask from a specified color key
        /// </summary>
        /// <param name="color">Color to become transparent</param>
        /// <param name="alpha">Alpha value to use for transparent pixels</param>
        public void CreateMaskFromColor(Color color, byte alpha)
        {
            sfImage_createMaskFromColor(CPtr, color, alpha);
        }
        
        /// <summary>
        /// Copy pixels from another image onto this one.
        /// This nethod does a slow pixel copy and should only be used at initialization time
        /// </summary>
        /// <param name="source">Source image to copy</param>
        /// <param name="destX">X coordinate of the destination position</param>
        /// <param name="destY">Y coordinate of the destination position</param>
        public void Copy(Image source, int destX, int destY)
        {
            Copy(source, destX, destY, new IntRect(0, 0, 0, 0));
        }
        
        /// <summary>
        /// Copy pixels from another image onto this one.
        /// This function does a slow pixel copy and should only be used at initialization time
        /// </summary>
        /// <param name="source">Source image to copy</param>
        /// <param name="destX">X coordinate of the destination position</param>
        /// <param name="destY">Y coordinate of the destination position</param>
        /// <param name="sourceRect">Sub-rectangle of the source image to copy</param>
        public void Copy(Image source, int destX, int destY, IntRect sourceRect)
        {
            Copy(source, destX, destY, sourceRect, false);
        }
        
        /// <summary>
        /// Copy pixels from another image onto this one.
        /// This function does a slow pixel copy and should only be used at initialization time
        /// </summary>
        /// <param name="source">Source image to copy</param>
        /// <param name="destX">X coordinate of the destination position</param>
        /// <param name="destY">Y coordinate of the destination position</param>
        /// <param name="sourceRect">Sub-rectangle of the source image to copy</param>
        /// <param name="applyAlpha">Should the copy take in account the source transparency?</param>
        public void Copy(Image source, int destX, int destY, IntRect sourceRect, bool applyAlpha)
        {
            sfImage_copyImage(CPtr, source.CPtr, destX, destY, sourceRect, applyAlpha);
        }
        
        /// <summary>
        /// Get a pixel from the image
        /// </summary>
        /// <param name="x">X coordinate of pixel in the image</param>
        /// <param name="y">Y coordinate of pixel in the image</param>
        /// <returns>Color of pixel (x, y)</returns>
        public Color GetPixel(int x, int y)
        {
            return sfImage_getPixel(CPtr, x, y);
        }
        
        /// <summary>
        /// Change the color of a pixel
        /// </summary>
        /// <param name="x">X coordinate of pixel in the image</param>
        /// <param name="y">Y coordinate of pixel in the image</param>
        /// <param name="color">New color for pixel (x, y)</param>
        public void SetPixel(int x, int y, Color color)
        {
            sfImage_setPixel(CPtr, x, y, color);
        }
        
        /// <summary>
        /// Flip the image horizontally
        /// </summary>
        public void FlipHorizontally()
        {
            sfImage_flipHorizontally(CPtr);
        }
        
        /// <summary>
        /// Flip the image vertically
        /// </summary>
        public void FlipVertically()
        {
            sfImage_flipVertically(CPtr);
        }

        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            sfImage_destroy(CPtr);
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format($"[Image] Size({Size})");
        }

        #endregion
    }
}
