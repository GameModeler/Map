using System;
using System.Runtime.InteropServices;

namespace Map.Graphics.Structs
{
    /// <summary>
    /// Utility struct to manipulate 32 bits RGBA colors
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Color : IEquatable<Color>
    {
        #region Properties

        /// <summary>
        /// Red component of the color
        /// </summary>
        public byte R { get; }

        /// <summary>
        /// Green component of the color
        /// </summary>
        public byte G { get; }

        /// <summary>
        /// Blue component of the color
        /// </summary>
        public byte B { get; }

        /// <summary>
        /// Alpha (transparency) component of the color
        /// </summary>
        public byte A { get; }

        /// <summary>
        /// Predefined black color
        /// </summary>
        public static readonly Color Black = new Color(0, 0, 0);

        /// <summary>
        /// Predefined white color
        /// </summary>
        public static readonly Color White = new Color(255, 255, 255);

        /// <summary>
        /// Predefined red color
        /// </summary>
        public static readonly Color Red = new Color(255, 0, 0);

        /// <summary>
        /// Predefined green color
        /// </summary>
        public static readonly Color Green = new Color(0, 255, 0);

        /// <summary>
        /// Predefined blue color
        /// </summary>
        public static readonly Color Blue = new Color(0, 0, 255);

        /// <summary>
        /// Predefined yellow color
        /// </summary>
        public static readonly Color Yellow = new Color(255, 255, 0);

        /// <summary>
        /// Predefined magenta color
        /// </summary>
        public static readonly Color Magenta = new Color(255, 0, 255);

        /// <summary>
        /// Predefined cyan color
        /// </summary>
        public static readonly Color Cyan = new Color(0, 255, 255);

        /// <summary>
        /// Predefined white color
        /// </summary>
        public static readonly Color Transparent = new Color(0, 0, 0, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the color from another color
        /// </summary>
        /// <param name="color">Color to copy></param>
        public Color(Color color) : this(color.R, color.G, color.B, color.A) {}

        /// <summary>
        /// Construct the color from its red, green, blue components
        /// </summary>
        /// <param name="red">Red component</param>
        /// <param name="green">Green component</param>
        /// <param name="blue">Blue component</param>
        public Color(byte red, byte green, byte blue) : this(red, green, blue, 255) {}

        /// <summary>
        /// Construct the color from its red, green, blue and alpha components
        /// </summary>
        /// <param name="red">Red component</param>
        /// <param name="green">Green component</param>
        /// <param name="blue">Blue component</param>
        /// <param name="alpha">Alpha conponent</param>
        public Color(byte red, byte green, byte blue, byte alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Conpare two colors and check if they are equal
        /// </summary>
        /// <param name="first">First color</param>
        /// <param name="second">Second color</param>
        /// <returns>True if colors are equal, false otherwise</returns>
        public static bool operator ==(Color first, Color second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Conpare two colors and check if they are not equal
        /// </summary>
        /// <param name="first">First color</param>
        /// <param name="second">Second color</param>
        /// <returns>True if colors are not equal, false otherwise</returns>
        public static bool operator !=(Color first, Color second)
        {
            return !first.Equals(second);
        }

        /// <summary>
        /// Component-wise sum of two colors
        /// Components that exceed 255 are clamped to 255
        /// </summary>
        /// <param name="first">First color</param>
        /// <param name="second">Second color</param>
        /// <returns>Color representing the sum of the two colors</returns>
        public static Color operator +(Color first, Color second)
        {
            return new Color(
                (byte) Math.Min(first.R + second.R, 255),
                (byte) Math.Min(first.G + second.G, 255),
                (byte) Math.Min(first.B + second.B, 255),
                (byte) Math.Min(first.A + second.A, 255));
        }

        /// <summary>
        /// Component-wise substraction of two colors
        /// Components below 0 are clamped to 0
        /// </summary>
        /// <param name="first">First color</param>
        /// <param name="second">Second color</param>
        /// <returns>Color representing the substraction of the two colors</returns>
        public static Color operator -(Color first, Color second)
        {
            return new Color(
                (byte)Math.Min(first.R - second.R, 255),
                (byte)Math.Min(first.G - second.G, 255),
                (byte)Math.Min(first.B - second.B, 255),
                (byte)Math.Min(first.A - second.A, 255));
        }

        /// <summary>
        /// Component-wise multiplication of two colors
        /// Components above 255 are clamped to 255
        /// </summary>
        /// <param name="first">First color</param>
        /// <param name="second">Second color</param>
        /// <returns>Color representing the multiplication of the two colors</returns>
        public static Color operator *(Color first, Color second)
        {
            return new Color(
                (byte)Math.Min(first.R * second.R, 255),
                (byte)Math.Min(first.G * second.G, 255),
                (byte)Math.Min(first.B * second.B, 255),
                (byte)Math.Min(first.A * second.A, 255));
        }

        /// <summary>
        /// Compare color and object and check if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if the color and the object are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            return obj is Color && obj.Equals(this);
        }

        /// <summary>
        /// Compare two colors and check if they are equal
        /// </summary>
        /// <param name="other">Color to check</param>
        /// <returns>True if the colors are equal, false otherwise</returns>
        public bool Equals(Color other)
        {
            return R == other.R &&
                   G == other.G &&
                   B == other.B &&
                   A == other.A;
        }

        /// <summary>
        /// Provide an integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode()
        {
            return (R << 24) |
                   (G << 16) |
                   (B << 8) |
                   A;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[Color] " +
                                 $"R({R}) " +
                                 $"G({G}) " +
                                 $"B({B}) " +
                                 $"A({A})");
        }

        #endregion
    }
}
