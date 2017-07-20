using System.Runtime.InteropServices;
using Map.Core.Structs;

namespace Map.Graphics.Structs
{
    /// <summary>
    /// Define a point with color and texture coordinates
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        #region Properties

        /// <summary>
        /// 2D position of the vertex
        /// </summary>
        public Vector2Df Position;

        /// <summary>
        /// Coordinates of the texture's pixel to map to the vertex
        /// </summary>
        public Vector2Df TextCoords;

        /// <summary>
        /// Color of the vertex
        /// </summary>
        public Color Color;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the vertex from its position
        /// The vertex color is white and texture coordinates are (0, 0).
        /// </summary>
        /// <param name="position">Vertex position</param>
        public Vertex(Vector2Df position) : this(position, Color.White, new Vector2Df(0, 0)) {}
        
        /// <summary>
        /// Construct the vertex from its position and color
        /// The texture coordinates are (0, 0).
        /// </summary>
        /// <param name="position">Vertex position</param>
        /// <param name="color">Vertex color</param>
        public Vertex(Vector2Df position, Color color) : this(position, color, new Vector2Df(0, 0)) {}
        
        /// <summary>
        /// Construct the vertex from its position and texture coordinates
        /// The vertex color is white.
        /// </summary>
        /// <param name="position">Vertex position</param>
        /// <param name="textCoords">Vertex texture coordinates</param>
        public Vertex(Vector2Df position, Vector2Df textCoords) : this(position, Color.White, textCoords) {}

        /// <summary>
        /// Construct the vertex from its position, color and texture coordinates
        /// </summary>
        /// <param name="position">Vertex position</param>
        /// <param name="color">Vertex color</param>
        /// <param name="textCoords">Vertex texture coordinates</param>
        public Vertex(Vector2Df position, Color color, Vector2Df textCoords)
        {
            Position = position;
            Color = color;
            TextCoords = textCoords;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[Vertex] " +
                                 $"Position({Position}) " +
                                 $"TexCoord({TextCoords}) " +
                                 $"Color({Color})");
        }

        #endregion
    }
}
