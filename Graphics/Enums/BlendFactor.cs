namespace Map.Graphics.Enums
{
    /// <summary>
    /// Blending factors
    /// </summary>
    public enum BlendFactor
    {
        /// <summary>
        /// (0, 0, 0, 0)
        /// </summary>
        Zero,

        /// <summary>
        /// (1, 1, 1, 1)
        /// </summary>
        One,

        /// <summary>
        /// (src.r, src.g, src.b, src.a)
        /// </summary>
        SrcColor,

        /// <summary>
        /// (1, 1, 1, 1) - (src.r, src.g, src.b, src.a)
        /// </summary>
        OneMinusSrcColor,

        /// <summary>
        /// (dst.r, dst.g, dst.b, dst.a)
        /// </summary>
        DstColor,

        /// <summary>
        /// (1, 1, 1, 1) - (dst.r, dst.g, dst.b, dst.a)
        /// </summary>
        OneMinusDstColor,

        /// <summary>
        /// (src.a, src.a, src.a, src.a)
        /// </summary>
        SrcAlpha,

        /// <summary>
        /// (1, 1, 1, 1) - (src.a, src.a, src.a, src.a)
        /// </summary>
        OneMinusSrcAlpha,

        /// <summary>
        /// (dst.a, dst.a, dst.a, dst.a)
        /// </summary>
        DstAlpha,

        /// <summary>
        /// (1, 1, 1, 1) - (dst.a, dst.a, dst.a, dst.a)
        /// </summary>
        OneMinusDstAlpha
    }
}
