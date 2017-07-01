using System;
using System.Runtime.InteropServices;
using Map.Core.Structs;

namespace Map.Graphics.Structs
{
    /// <summary>
    /// Utility struct to manipulate 2D rectangles with integer coordinates
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IntRect : IEquatable<IntRect>
    {
        #region Properties

        /// <summary>
        /// Left coordinate of the rectangle
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Top coordinate of the rectangle
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Width of the rectangle
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the rectangle
        /// </summary>
        public int Height { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the rectangle from its coordinates
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public IntRect(int left, int top, int width, int height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
        
        /// <summary>
        /// Construct the rectangle from position and size
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle</param>
        /// <param name="size">Size of the rectangle</param>
        public IntRect(Vector2Di position, Vector2Di size) : this(position.X, position.Y, size.X, size.Y) {}

        #endregion

        #region Methods
        
        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="x">X coordinate of the point to test</param>
        /// <param name="y">Y coordinate of the point to test</param>
        /// <returns>True if the point is inside, false otherwise</returns>
        public bool Contains(int x, int y)
        {
            var minX = Math.Min(Left, Left + Width);
            var maxX = Math.Max(Left, Left + Width);
            var minY = Math.Min(Top, Top + Height);
            var maxY = Math.Max(Top, Top + Height);

            return (x >= minX) && (x < maxX) && (y >= minY) && (y < maxY);
        }
        
        /// <summary>
        /// Check intersection between two rectangles
        /// </summary>
        /// <param name="rect">Rectangle to test</param>
        /// <returns>True if rectangles overlap, false otherwise</returns>
        public bool Intersects(IntRect rect)
        {
            return Intersects(rect, out IntRect overlap);
        }
        
        /// <summary>
        /// Check intersection between two rectangles
        /// </summary>
        /// <param name="rect">Rectangle to test</param>
        /// <param name="overlap">Rectangle to be filled with overlapping rect</param>
        /// <returns>True if rectangles overlap, false otherwise</returns>
        public bool Intersects(IntRect rect, out IntRect overlap)
        {
            // Rectangles with negative dimensions are allowed, so we must handle them correctly

            // Compute the min and max of the first rectangle on both axes
            var r1MinX = Math.Min(Left, Left + Width);
            var r1MaxX = Math.Max(Left, Left + Width);
            var r1MinY = Math.Min(Top, Top + Height);
            var r1MaxY = Math.Max(Top, Top + Height);

            // Compute the min and max of the second rectangle on both axes
            var r2MinX = Math.Min(rect.Left, rect.Left + rect.Width);
            var r2MaxX = Math.Max(rect.Left, rect.Left + rect.Width);
            var r2MinY = Math.Min(rect.Top, rect.Top + rect.Height);
            var r2MaxY = Math.Max(rect.Top, rect.Top + rect.Height);

            // Compute the intersection boundaries
            var interLeft = Math.Max(r1MinX, r2MinX);
            var interTop = Math.Max(r1MinY, r2MinY);
            var interRight = Math.Min(r1MaxX, r2MaxX);
            var interBottom = Math.Min(r1MaxY, r2MaxY);

            overlap = new IntRect();

            // If the intersection is valid (positive non zero area), then there is an intersection
            if (interLeft < interRight && interTop < interBottom)
            {
                overlap.Left = interLeft;
                overlap.Top = interTop;
                overlap.Width = interRight - interLeft;
                overlap.Height = interBottom - interTop;

                return true;
            }

            overlap.Left = 0;
            overlap.Top = 0;
            overlap.Width = 0;
            overlap.Height = 0;

            return false;
        }

        /// <summary>
        /// Operator overload == to check rectangles equality
        /// </summary>
        /// <param name="first">First rectangle</param>
        /// <param name="second">Second rectangle</param>
        /// <returns>True if the rectangle are equal, false otherwise</returns>
        public static bool operator ==(IntRect first, IntRect second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Operator overload != to check rectangles inequality
        /// </summary>
        /// <param name="first">First rectangle</param>
        /// <param name="second">Second rectangle</param>
        /// <returns>True if the rectangle are not equal, false otherwise</returns>
        public static bool operator !=(IntRect first, IntRect second)
        {
            return !first.Equals(second);
        }
        
        /// <summary>
        /// Compare rectangle and object and check if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if the object and the rectangle are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            return (obj is IntRect) && obj.Equals(this);
        }
        
        /// <summary>
        /// Compare two rectangles and check if they are equal
        /// </summary>
        /// <param name="other">Rectangle to check</param>
        /// <returns>True if the rectangles are equal, false otherwise</returns>
        public bool Equals(IntRect other)
        {
            return (Left == other.Left) &&
                   (Top == other.Top) &&
                   (Width == other.Width) &&
                   (Height == other.Height);
        }
        
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode()
        {
            return Left ^
                   ((Top << 13) | (Top >> 19)) ^
                   ((Width << 26) | (Width >> 6)) ^
                   ((Height << 7) | (Height >> 25));
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[IntRect] " +
                                 $"Left({Left}) " +
                                 $"Top({Top}) " +
                                 $"Width({Width}) " +
                                 $"Height({Height})");
        }

        /// <summary>
        /// Explicit casting to a float rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Float rectangle</returns>
        public static explicit operator FloatRect(IntRect r)
        {
            return new FloatRect(r.Left, r.Top, r.Width, r.Height);
        }

        #endregion
    }

    /// <summary>
    /// Utility struct to manipulate 2D rectangles with float coordinates
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FloatRect : IEquatable<FloatRect>
    {
        #region Properties

        /// <summary>
        /// Left coordinate of the rectangle
        /// </summary>
        public float Left { get; set; }

        /// <summary>
        /// Top coordinate of the rectangle
        /// </summary>
        public float Top { get; set; }

        /// <summary>
        /// Width of the rectangle
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Height of the rectangle
        /// </summary>
        public float Height { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the rectangle from its coordinates
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public FloatRect(float left, float top, float width, float height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
        
        /// <summary>
        /// Construct the rectangle from position and size
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle</param>
        /// <param name="size">Size of the rectangle</param>
        public FloatRect(Vector2Df position, Vector2Df size) : this(position.X, position.Y, size.X, size.Y) {}

        #endregion

        #region Methods

        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="x">X coordinate of the point to test</param>
        /// <param name="y">Y coordinate of the point to test</param>
        /// <returns>True if the point is inside, false otherwise</returns>
        public bool Contains(float x, float y)
        {
            var minX = Math.Min(Left, Left + Width);
            var maxX = Math.Max(Left, Left + Width);
            var minY = Math.Min(Top, Top + Height);
            var maxY = Math.Max(Top, Top + Height);

            return x >= minX && x < maxX && y >= minY && y < maxY;
        }
        
        /// <summary>
        /// Check intersection between two rectangles
        /// </summary>
        /// <param name="rect">Rectangle to test</param>
        /// <returns>True if rectangles overlap, false otherwise</returns>
        public bool Intersects(FloatRect rect)
        {
            return Intersects(rect, out FloatRect overlap);
        }
        
        /// <summary>
        /// Check intersection between two rectangles
        /// </summary>
        /// <param name="rect">Rectangle to test</param>
        /// <param name="overlap">Rectangle to be filled with overlapping rect</param>
        /// <returns>True if rectangles overlap, false otherwise</returns>
        public bool Intersects(FloatRect rect, out FloatRect overlap)
        {
            // Rectangles with negative dimensions are allowed, so we must handle them correctly

            // Compute the min and max of the first rectangle on both axes
            var r1MinX = Math.Min(Left, Left + Width);
            var r1MaxX = Math.Max(Left, Left + Width);
            var r1MinY = Math.Min(Top, Top + Height);
            var r1MaxY = Math.Max(Top, Top + Height);

            // Compute the min and max of the second rectangle on both axes
            var r2MinX = Math.Min(rect.Left, rect.Left + rect.Width);
            var r2MaxX = Math.Max(rect.Left, rect.Left + rect.Width);
            var r2MinY = Math.Min(rect.Top, rect.Top + rect.Height);
            var r2MaxY = Math.Max(rect.Top, rect.Top + rect.Height);

            // Compute the intersection boundaries
            var interLeft = Math.Max(r1MinX, r2MinX);
            var interTop = Math.Max(r1MinY, r2MinY);
            var interRight = Math.Min(r1MaxX, r2MaxX);
            var interBottom = Math.Min(r1MaxY, r2MaxY);

            overlap = new FloatRect();

            // If the intersection is valid (positive non zero area), then there is an intersection
            if (interLeft < interRight && interTop < interBottom)
            {
                overlap.Left = interLeft;
                overlap.Top = interTop;
                overlap.Width = interRight - interLeft;
                overlap.Height = interBottom - interTop;

                return true;
            }

            overlap.Left = 0;
            overlap.Top = 0;
            overlap.Width = 0;
            overlap.Height = 0;

            return false;
        }
        
        /// <summary>
        /// Operator overload == to check rectangles equality
        /// </summary>
        /// <param name="first">First rectangle</param>
        /// <param name="second">Second rectangle</param>
        /// <returns>True if the rectangles are equal, false otherwise</returns>
        public static bool operator ==(FloatRect first, FloatRect second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Operator overload != to check rectangles inequality
        /// </summary>
        /// <param name="first">First rectangle</param>
        /// <param name="second">Second rectangle</param>
        /// <returns>True if the rectangles are not equal, false otherwise</returns>
        public static bool operator !=(FloatRect first, FloatRect second)
        {
            return !first.Equals(second);
        }

        /// <summary>
        /// Compare rectangle and object and check if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if the object and the rectangle are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            return obj is FloatRect && obj.Equals(this);
        }

        /// <summary>
        /// Compare two rectangles and check if they are equal
        /// </summary>
        /// <param name="other">Rectangle to check</param>
        /// <returns>True if the rectangles are equal, false otherwise</returns>
        public bool Equals(FloatRect other)
        {
            return Left.Equals(other.Left) &&
                   Top.Equals(other.Top) &&
                   Width.Equals(other.Width) &&
                   Height.Equals(other.Height);
        }
        
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode()
        {
            return unchecked((int) Left ^
                             (((int) Top << 13) | ((int) Top >> 19)) ^
                             (((int) Width << 26) | ((int) Width >> 6)) ^
                             (((int) Height << 7) | ((int) Height >> 25)));
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format("[FloatRect] " +
                                 $"Left({Left}) " +
                                 $"Top({Top}) " +
                                 $"Width({Width}) " +
                                 $"Height({Height})");
        }
        
        /// <summary>
        /// Explicit casting to an integer rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Integer rectangle</returns>
        public static explicit operator IntRect(FloatRect r)
        {
            return new IntRect((int) r.Left,
                (int) r.Top,
                (int) r.Width,
                (int) r.Height);
        }

        #endregion
    }
}
