using System;
using System.Runtime.InteropServices;

namespace Map.Core.Structs
{
    /// <summary>
    /// Utility struct to manipulate 2D vectors with float components
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2Df : IEquatable<Vector2Df>
    {
        #region Properties

        /// <summary>
        /// Horizontal component of the vector
        /// </summary>
        public float X { get; }

        /// <summary>
        /// Vertical component of the vector
        /// </summary>
        public float Y { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2Df(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Operator overload - to get the opposite of a vector
        /// </summary>
        /// <param name="v">Vector to negate</param>
        /// <returns>Opposite vector</returns>
        public static Vector2Df operator -(Vector2Df v)
        {
            return new Vector2Df(-v.X, -v.Y);
        }

        /// <summary>
        /// Operator overload - to substract two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Vector representing the difference between the two vectors</returns>
        public static Vector2Df operator -(Vector2Df v1, Vector2Df v2)
        {
            return new Vector2Df(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Operator overload + to add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Vector representing the sum between the two vectors</returns>
        public static Vector2Df operator +(Vector2Df v1, Vector2Df v2)
        {
            return new Vector2Df(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// Operator overload * to multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>Vector representing the multiplication of a vector by a scalar value</returns>
        public static Vector2Df operator *(Vector2Df v, float x)
        {
            return new Vector2Df(v.X * x, v.Y * x);
        }

        /// <summary>
        /// Operator overload * to multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>Vector representing the multiplication of a scalar value by a vector</returns>
        public static Vector2Df operator *(float x, Vector2Df v)
        {
            return new Vector2Df(x * v.X, x * v.Y);
        }

        /// <summary>
        /// Operator overload / to divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>Vector representing the division of a vector by a scalar value</returns>
        public static Vector2Df operator /(Vector2Df v, float x)
        {
            return new Vector2Df(v.X / x, v.Y / x);
        }

        /// <summary>
        /// Operator overload == to check vector equality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>True if v1 and v2 are equal, false otherwise</returns>
        public static bool operator ==(Vector2Df v1, Vector2Df v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// Operator overload != to check vector inequality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>True if v1 and v2 are not equal, false otherwise</returns>
        public static bool operator !=(Vector2Df v1, Vector2Df v2)
        {
            return !v1.Equals(v2);
        }

        /// <summary>
        /// Compare vector and object and check if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if the vector and the object are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            return obj is Vector2Df && obj.Equals(this);
        }

        /// <summary>
        /// Compare two vectors and check if they are equal
        /// </summary>
        /// <param name="other">Vector to check</param>
        /// <returns>True if the vectors are equal, false otherwise</returns>
        public bool Equals(Vector2Df other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        /// <summary>
        /// Explicit casting to an integer vector type
        /// </summary>
        /// <param name="v">Vector to cast</param>
        /// <returns>Casted vector</returns>
        public static explicit operator Vector2Di(Vector2Df v)
        {
            return new Vector2Di((int)v.X, (int)v.Y);
        }

        /// <summary>
        /// Provide an integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format($"[Vector2Df] X({X}) Y({Y})");
        }

        #endregion
    }

    /// <summary>
    /// Utility struct to manipulate 2D vectors with integer components 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2Di : IEquatable<Vector2Di>
    {
        #region Properties

        /// <summary>
        /// Horizontal component of the vector
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Vertical component of the vector
        /// </summary>
        public int Y { get; }

        #endregion

        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2Di(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region Methods

        /// <summary>
        /// Operator overload - to get the opposite of a vector
        /// </summary>
        /// <param name="v">Vector to negate</param>
        /// <returns>Opposite vector</returns>
        public static Vector2Di operator -(Vector2Di v)
        {
            return new Vector2Di(-v.X, -v.Y);
        }

        /// <summary>
        /// Operator overload - to substract two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Vector representing the difference between the two vectors</returns>
        public static Vector2Di operator -(Vector2Di v1, Vector2Di v2)
        {
            return new Vector2Di(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Operator overload + to add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Vector representing the sum of the two vectors</returns>
        public static Vector2Di operator +(Vector2Di v1, Vector2Di v2)
        {
            return new Vector2Di(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// Operator overload * to multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>Vector representing the multiplication of a vector by a scalar value</returns>
        public static Vector2Di operator *(Vector2Di v, int x)
        {
            return new Vector2Di(v.X * x, v.Y * x);
        }

        /// <summary>
        /// Operator overload * to multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>Vector representing the multiplication of a scalar value by a vector</returns>
        public static Vector2Di operator *(int x, Vector2Di v)
        {
            return new Vector2Di(x * v.X, x * v.Y);
        }

        /// <summary>
        /// Operator overload / to divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>Vector representing the division of a vector by a scalar value</returns>
        public static Vector2Di operator /(Vector2Di v, int x)
        {
            return new Vector2Di(v.X / x, v.Y / x);
        }

        /// <summary>
        /// Operator overload == to check vector equality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>True if v1 and v2 are equal, false otherwise</returns>
        public static bool operator ==(Vector2Di v1, Vector2Di v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// Operator overload != to check vector inequality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>True if v1 and v2 are not equal, false otherwise</returns>
        public static bool operator !=(Vector2Di v1, Vector2Di v2)
        {
            return !v1.Equals(v2);
        }

        /// <summary>
        /// Compare a vector and an object and check if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if the vector and the object are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            return obj is Vector2Di && obj.Equals(this);
        }

        /// <summary>
        /// Compare two vectors and check if they are equal
        /// </summary>
        /// <param name="other">Vector to check</param>
        /// <returns>True if the vectors are equal, false otherwise</returns>
        public bool Equals(Vector2Di other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        /// <summary>
        /// Explicite casting to a float vector type
        /// </summary>
        /// <param name="v">Vector to cast</param>
        /// <returns>Casted vector</returns>
        public static explicit operator Vector2Df(Vector2Di v)
        {
            return new Vector2Df(v.X, v.Y);
        }

        /// <summary>
        /// Provide an integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format($"[Vector2Di] X({X}) Y({Y})");
        }

        #endregion
    }
}
