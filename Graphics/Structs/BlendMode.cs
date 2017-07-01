using System;
using System.Runtime.InteropServices;
using Map.Graphics.Enums;

namespace Map.Graphics.Structs
{
    /// <summary>
    /// Blending mode for drawing
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BlendMode : IEquatable<BlendMode>
    {
        #region Properties

        /// <summary>
        /// Blend source and destination according to alpha destination
        /// </summary>
        public static readonly BlendMode Alpha = new BlendMode(BlendFactor.SrcAlpha, BlendFactor.OneMinusSrcAlpha, 
            BlendEquation.Add, BlendFactor.One, BlendFactor.One, BlendEquation.Add);

        /// <summary>
        /// Add source to destination
        /// </summary>
        public static readonly BlendMode Add = new BlendMode(BlendFactor.SrcAlpha, BlendFactor.One, 
            BlendEquation.Add, BlendFactor.One, BlendFactor.One, BlendEquation.Add);

        /// <summary>
        /// Multiply source and destination
        /// </summary>
        public static readonly BlendMode Multiply = new BlendMode(BlendFactor.DstColor, BlendFactor.Zero);

        /// <summary>
        /// Overwrite destination with source
        /// </summary>
        public static readonly BlendMode None = new BlendMode(BlendFactor.One, BlendFactor.Zero);

        /// <summary>
        /// Source blending factor for the color channels
        /// </summary>
        public BlendFactor ColorSrcFactor { get; }

        /// <summary>
        /// Destination blending factor for the color channels
        /// </summary>
        public BlendFactor ColorDstFactor { get; }

        /// <summary>
        /// Blending equation for the color channels
        /// </summary>
        public BlendEquation ColorEquation { get; }

        /// <summary>
        /// Source blending factor for the alpha channel
        /// </summary>
        public BlendFactor AlphaSrcFactor { get; }

        /// <summary>
        /// Destination blending factor for the alpha channel
        /// </summary>
        public BlendFactor AlphaDstFactor { get; }

        /// <summary>
        /// Blending equation for the alpha channel
        /// </summary>
        public BlendEquation AlphaEquation { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the blend mode given the factors with a default "add" blending equation
        /// </summary>
        /// <param name="srcFactor">Specifies how to compute the source factor for the color and alpha channels</param>
        /// <param name="dstFactor">Specifies how to compute the destination factor for the color and alpha channels</param>
        public BlendMode(BlendFactor srcFactor, BlendFactor dstFactor) : this(srcFactor, dstFactor, BlendEquation.Add) {}

        /// <summary>
        /// Construct the blend mode given the factors and equation
        /// </summary>
        /// <param name="srcFactor">Specifies how to compute the source factor for the color and alpha channels</param>
        /// <param name="dstFactor">Specifies how to compute the destination factor for the color and alpha channels</param>
        /// <param name="blendEquation">Specifies how to combine the source and destination colors and alphas</param>
        public BlendMode(BlendFactor srcFactor, BlendFactor dstFactor, BlendEquation blendEquation)
            : this(srcFactor, dstFactor, blendEquation, srcFactor, dstFactor, blendEquation) {}

        /// <summary>
        /// Construct the blend mode given the factors and equations
        /// </summary>
        /// <param name="colorSrcFactor">Specifies how to compute the source factor for the color channels</param>
        /// <param name="colorDstFactor">Specifies how th compute the destination factor for the color channels</param>
        /// <param name="colorBlendEquation">Specifies how to combine the source and destination colors</param>
        /// <param name="alphaSrcFactor">Specifies how to compute the source factor for the alpha channel</param>
        /// <param name="alphaDstFactor">Specifies how to compute the destination factor for the alpha channel</param>
        /// <param name="alphaBlendEquation">Specifies how to combine the source and destination alphas</param>
        public BlendMode(BlendFactor colorSrcFactor, BlendFactor colorDstFactor, BlendEquation colorBlendEquation,
            BlendFactor alphaSrcFactor, BlendFactor alphaDstFactor, BlendEquation alphaBlendEquation)
        {
            ColorSrcFactor = colorSrcFactor;
            ColorDstFactor = colorDstFactor;
            ColorEquation = colorBlendEquation;
            AlphaSrcFactor = alphaSrcFactor;
            AlphaDstFactor = alphaDstFactor;
            AlphaEquation = alphaBlendEquation;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compare two blend modes and check if they are equal
        /// </summary>
        /// <param name="first">First blend mode</param>
        /// <param name="second">Second blend mode</param>
        /// <returns>True if blend modes are equal, false otherwise</returns>
        public static bool operator ==(BlendMode first, BlendMode second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Compare two blend modes and check if they are not equal
        /// </summary>
        /// <param name="first">First blend mode</param>
        /// <param name="second">Second blend mode</param>
        /// <returns>True if blend modes are not equal, false otherwise</returns>
        public static bool operator !=(BlendMode first, BlendMode second)
        {
            return !first.Equals(second);
        }

        /// <summary>
        /// Compare blend mode and object and check if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if the blend mode and the object are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            return obj is BlendMode && obj.Equals(this);
        }

        /// <summary>
        /// Compare two blend modes and check if they are equal
        /// </summary>
        /// <param name="other">Blend mode to check</param>
        /// <returns>True if the blend modes are equal, false otherwise</returns>
        public bool Equals(BlendMode other)
        {
            return ColorSrcFactor == other.ColorSrcFactor &&
                   ColorDstFactor == other.ColorDstFactor &&
                   ColorEquation == other.ColorEquation &&
                   AlphaSrcFactor == other.AlphaSrcFactor &&
                   AlphaDstFactor == other.AlphaDstFactor &&
                   AlphaEquation == other.AlphaEquation;
        }

        /// <summary>
        /// Provide an integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode()
        {
            return ColorSrcFactor.GetHashCode() ^
                   ColorDstFactor.GetHashCode() ^
                   ColorEquation.GetHashCode() ^
                   AlphaSrcFactor.GetHashCode() ^
                   AlphaDstFactor.GetHashCode() ^
                   AlphaEquation.GetHashCode();
        }

        #endregion
    }
}
