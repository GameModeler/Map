using Map.Core.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Specialized shape representing a rectangle
    /// </summary>
    public class RectangleShape : Shape
    {
        #region Attributes

        private Vector2Df _size;

        #endregion

        #region Properties

        public Vector2Df Size
        {
            get => _size;
            set
            {
                _size = value;
                Update();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public RectangleShape() : this(new Vector2Df(0, 0)) {}
        
        /// <summary>
        /// Construct the shape with an initial size
        /// </summary>
        /// <param name="size">Size of the shape</param>
        public RectangleShape(Vector2Df size)
        {
            Size = size;
        }
        
        /// <summary>
        /// Construct the shape from another shape
        /// </summary>
        /// <param name="copy">Shape to copy</param>
        public RectangleShape(RectangleShape copy) : base(copy)
        {
            Size = copy.Size;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the total number of points of the rectangle.
        /// </summary>
        /// <returns>The total point count. For rectangle shapes, this number is always 4.</returns>
        public override int GetPointCount()
        {
            return 4;
        }
        
        /// <summary>
        /// Get the position of a point
        ///
        /// The returned point is in local coordinates, that is,
        /// the shape's transforms (position, rotation, scale) are
        /// not taken into account.
        /// The result is undefined if index is out of the valid range.
        /// </summary>
        /// <param name="index">Index of the point to get, in range [0 .. 3]</param>
        /// <returns>index-th point of the shape</returns>
        public override Vector2Df GetPoint(int index)
        {
            switch (index)
            {
                case 1:
                    return new Vector2Df(Size.X, 0);
                case 2:
                    return new Vector2Df(Size.X, Size.Y);
                case 3:
                    return new Vector2Df(0, Size.Y);
                default:
                    return new Vector2Df(0, 0);
            }
        }

        #endregion
    }
}
