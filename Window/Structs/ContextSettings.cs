using System.Runtime.InteropServices;

namespace Map.Window.Structs
{
    /// <summary>
    /// OpenGL contexts settings
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ContextSettings
    {
        #region Properties

        /// <summary>
        /// Depth buffer bits (0 is disabled)
        /// </summary>
        public int DepthBits { get; set; }

        /// <summary>
        /// Stencil buffer bits (0 is disabled)
        /// </summary>
        public int StencilBits { get; set; }

        /// <summary>
        /// Antialiasing level (0 is disabled)
        /// </summary>
        public int AntialiasingLevel { get; set; }

        /// <summary>
        /// Major number of the context version
        /// </summary>
        public int MajorVersion { get; set; }

        /// <summary>
        /// Minor number of the context version
        /// </summary>
        public int MinorVersion { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the settings from depth and stencil bits
        /// </summary>
        /// <param name="depthBits">Depth buffer bits</param>
        /// <param name="stencilBits">Stencil buffer bits</param>
        public ContextSettings(int depthBits, int stencilBits) 
            : this(depthBits, stencilBits, 0) {}

        /// <summary>
        /// Construct the settings from depth and stencil bits, antialiasing level
        /// </summary>
        /// <param name="depthBits">Depth buffer bits</param>
        /// <param name="stencilBits">Stencil buffer bits</param>
        /// <param name="antialiasingLevel">Antialiasing level</param>
        public ContextSettings(int depthBits, int stencilBits, int antialiasingLevel) 
            : this(depthBits, stencilBits, antialiasingLevel, 2, 0) {}

        /// <summary>
        /// Construct the settings from depth and stencil bits, antialiasing level and version (major and minor)
        /// </summary>
        /// <param name="depthBits">Depth buffer bits</param>
        /// <param name="stencilBits">Stencil buffer bits</param>
        /// <param name="antialiasingLevel">Antialiasing level</param>
        /// <param name="majorVersion">Major number of the context version</param>
        /// <param name="minorVersion">Minor number of the context version</param>
        public ContextSettings(int depthBits, int stencilBits, int antialiasingLevel, int majorVersion, int minorVersion)
        {
            DepthBits = depthBits;
            StencilBits = stencilBits;
            AntialiasingLevel = antialiasingLevel;
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[ContextSettings] " +
                                 $"DepthBits({DepthBits}) " +
                                 $"StencilBits({StencilBits}) " +
                                 $"AntialiasingLevel({AntialiasingLevel}) " +
                                 $"MajorVersion({MajorVersion}) " +
                                 $"MinorVersion({MinorVersion})");
        }

        #endregion
    }
}
