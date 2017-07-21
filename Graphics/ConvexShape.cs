﻿using System;
using Map.Core.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Specialized shape representing a convex polygon
    /// </summary>
    public class ConvexShape : Shape
    {
        #region Attributes

        private Vector2Df[] _points;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConvexShape() : this(0) {}
        
        /// <summary>
        /// Construct the shape with an initial point count
        /// </summary>
        /// <param name="pointCount">Number of points of the shape</param>
        public ConvexShape(int pointCount)
        {
            SetPointCount(pointCount);
        }
        
        /// <summary>
        /// Construct the shape from another shape
        /// </summary>
        /// <param name="copy">Shape to copy</param>
        public ConvexShape(ConvexShape copy) : base(copy)
        {
            SetPointCount(copy.GetPointCount());
            for (var i = 0; i < copy.GetPointCount(); ++i)
            {
                SetPoint(i, copy.GetPoint(i));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the total number of points of the polygon
        /// </summary>
        /// <returns>The total point count</returns>
        public override int GetPointCount()
        {
            return _points.Length;
        }
        
        /// <summary>
        /// Set the number of points of the polygon.
        /// The count must be greater than 2 to define a valid shape.
        /// </summary>
        /// <param name="count">New number of points of the polygon</param>
        public void SetPointCount(int count)
        {
            Array.Resize(ref _points, count);
            Update();
        }
        
        /// <summary>
        /// Get the position of a point
        ///
        /// The returned point is in local coordinates, that is,
        /// the shape's transforms (position, rotation, scale) are
        /// not taken into account.
        /// The result is undefined if index is out of the valid range.
        /// </summary>
        /// <param name="index">Index of the point to get, in range [0 .. PointCount - 1]</param>
        /// <returns>index-th point of the shape</returns>
        public override Vector2Df GetPoint(int index)
        {
            return _points[index];
        }
        
        /// <summary>
        /// Set the position of a point.
        ///
        /// Don't forget that the polygon must remain convex, and
        /// the points need to stay ordered!
        /// PointCount must be set first in order to set the total
        /// number of points. The result is undefined if index is out
        /// of the valid range.
        /// </summary>
        /// <param name="index">Index of the point to change, in range [0 .. PointCount - 1]</param>
        /// <param name="point">New position of the point</param>
        public void SetPoint(int index, Vector2Df point)
        {
            _points[index] = point;
            Update();
        }

        #endregion
    }
}
