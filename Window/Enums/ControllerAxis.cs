namespace Map.Window.Enums
{
    /// <summary>
    /// Supported controller axes
    /// </summary>
    public enum ControllerAxis
    {
        /// <summary>
        /// The X axis
        /// Sony PlayStation => L3 [-100 .. 100]
        /// </summary>
        X,

        /// <summary>
        /// The Y axis
        /// Sony PlayStation => L3 [-100 .. 100]
        /// </summary>
        Y,

        /// <summary>
        /// The Z axis
        /// Sony PlayStation => R3 [-100 .. 100]
        /// </summary>
        Z,

        /// <summary>
        /// The R axis
        /// Sony PlayStation => R3 [-100 .. 100]
        /// </summary>
        R,

        /// <summary>
        /// The U axis
        /// Sony PlayStation => L2 [-100 .. 100]
        /// </summary>
        U,

        /// <summary>
        /// The V axis
        /// Sony PlayStation => R2 [-100 .. 100]
        /// </summary>
        V,

        /// <summary>
        /// The X axis of the point-of-view hat
        /// Sony PlayStation => D-PAD (left = -100, right = 100)
        /// </summary>
        PovX,

        /// <summary>
        /// The Y axis of the point-of-view hat
        /// Sony PlayStation => D-PAD (top = 100, bottom = -100)
        /// </summary>
        PovY
    }
}
