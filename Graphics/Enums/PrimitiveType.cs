namespace Map.Graphics.Enums
{
    /// <summary>
    /// Primitive types that a VertexArray can render
    /// Points and lines have no area (thickness will always be 1 pixel)
    /// </summary>
    public enum PrimitiveType
    {
        /// <summary>
        /// List of individual points
        /// </summary>
        Points,

        /// <summary>
        /// List of individual lines
        /// </summary>
        Lines,

        /// <summary>
        /// List of connected lines
        /// A point uses the previous point to form a line
        /// </summary>
        LinesStrip,

        /// <summary>
        /// List of individual triangles
        /// </summary>
        Triangles,

        /// <summary>
        /// List of connected triangles
        /// A point uses the two previous points to form a triangle
        /// </summary>
        TrianglesStrip,

        /// <summary>
        /// List of connected triangles
        /// A point uses the common center and the previous point to form a triangle
        /// </summary>
        TrianglesFan,

        /// <summary>
        /// List of individual quads
        /// </summary>
        Quads
    }
}
