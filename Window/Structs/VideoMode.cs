using System.Runtime.InteropServices;
using System.Security;

namespace Map.Window.Structs
{
    /// <summary>
    /// Define a video mode (width, height, bpp, frequency)
    /// and provide static methods to get modes supported by the display device
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VideoMode
    {
        #region Imports

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern VideoMode sfVideoMode_getDesktopMode();

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe VideoMode* sfVideoMode_getFullscreenModes(out int count);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfVideoMode_isValid(VideoMode mode);

        #endregion

        #region Properties

        /// <summary>
        /// Video mode width (pixels)
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Video mode height (pixels)
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Video mode depth (bits per pixel)
        /// </summary>
        public int BitsPerPixel { get; set; }

        /// <summary>
        /// Current desktop video mode
        /// </summary>
        public static VideoMode DesktopMode => sfVideoMode_getDesktopMode();

        /// <summary>
        /// List of all the supported fullscreen video modes
        /// </summary>
        public static VideoMode[] FullscreenModes
        {
            get
            {
                unsafe
                {
                    var modesPtr = sfVideoMode_getFullscreenModes(out int count);
                    var modes = new VideoMode[count];

                    for (var i = 0; i < count; ++i)
                    {
                        modes[i] = modesPtr[i];
                    }

                    return modes;
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize the video mode with its width and height
        /// and a default value of 32 bits per pixel 
        /// </summary>
        /// <param name="width">Video mode width</param>
        /// <param name="height">Video mode height</param>
        public VideoMode(int width, int height) : this(width, height, 32) {}

        /// <summary>
        /// Initialize the video mode with its width, height and depth 
        /// </summary>
        /// <param name="width">Video mode width</param>
        /// <param name="height">Video mode height</param>
        /// <param name="bpp">Video mode depth (bits per pixel)</param>
        public VideoMode(int width, int height, int bpp)
        {
            Width = width;
            Height = height;
            BitsPerPixel = bpp;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tell whether or not the video mode is supported
        /// </summary>
        /// <returns>True if the video mode is valid, false otherwise</returns>
        public bool IsValid()
        {
            return sfVideoMode_isValid(this);
        }

        /// <summary>
        /// Provide a string description of the video mode
        /// </summary>
        /// <returns>String description of the video mode</returns>
        public override string ToString()
        {
            return string.Format("[VideoMode] " +
                                 $"Width({Width}) " +
                                 $"Heigth({Height}) " +
                                 $"BitsPerPixel({BitsPerPixel})");
        }

        #endregion
    }
}
