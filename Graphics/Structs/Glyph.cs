using System.Runtime.InteropServices;

namespace Map.Graphics.Structs
{
    /// <summary>
    /// Structure describing a glyph (visual character)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Glyph
    {
        /// <summary>
        /// Offset to move horizontally to the next character
        /// </summary>
        public int Advance { get; set; }

        /// <summary>
        /// Texture coordinates of the glyph inside the font's texture
        /// </summary>
        public IntRect TextureRect { get; set; }

        /// <summary>
        /// Bounding rectangle of the glyph (in coordinates relative to the baseline)
        /// </summary>
        public FloatRect Bounds { get; set; }
    }
}
