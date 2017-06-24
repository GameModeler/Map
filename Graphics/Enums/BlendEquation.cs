namespace Map.Graphics.Enums
{
    /// <summary>
    /// Blending equations
    /// </summary>
    public enum BlendEquation
    {
        /// <summary>
        /// Pixel = Src * SrcFactor + Dst * DstFactor
        /// </summary>
        Add,

        /// <summary>
        /// Pixel = Src * SrcFactor - Dst * DstFactor
        /// </summary>
        Substract
    }
}
