using System;
using System.Runtime.InteropServices;

namespace Map.Core.Structs
{
    /// <summary>
    /// Utility struct to manipulate 3D vectors with float components
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3Df : IEquatable<Vector3Df>
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

        /// <summary>
        /// Depth component of the vector
        /// </summary>
        public float Z { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        public Vector3Df(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Operator overload - to get the opposite of a vector
        /// </summary>
        /// <param name="v">Vector to negate</param>
        /// <returns>Opposite of the vector</returns>
        public static Vector3Df operator -(Vector3Df v)
        {
            return new Vector3Df(-v.X, -v.Y, -v.Z);
        }

        /// <summary>
        /// Operator overload - to substract two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Vector representing the difference between the two vectors</returns>
        public static Vector3Df operator -(Vector3Df v1, Vector3Df v2)
        {
            return new Vector3Df(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// Operator overload + to add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Vector representing the sum of the two vectors</returns>
        public static Vector3Df operator +(Vector3Df v1, Vector3Df v2)
        {
            return new Vector3Df(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// Operator overload * to multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>Vector representing the multiplication of a vector by a scalar value</returns>
        public static Vector3Df operator *(Vector3Df v, float x)
        {
            return new Vector3Df(v.X * x, v.Y * x, v.Z * x);
        }

        /// <summary>
        /// Operator overload * to multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>Vector representing the multiplication of a scalar value by a vector</returns>
        public static Vector3Df operator *(float x, Vector3Df v)
        {
            return new Vector3Df(x * v.X, x * v.Y, x * v.Z);
        }

        /// <summary>
        /// Operator overload / to divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>Vector representing the division of a vector by a scalar value</returns>
        public static Vector3Df operator /(Vector3Df v, float x)
        {
            return new Vector3Df(v.X / x, v.Y / x, v.Z / x);
        }

        /// <summary>
        /// Operator overload == to check for vector equality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>True if the vectors are equal, false otherwise</returns>
        public static bool operator ==(Vector3Df v1, Vector3Df v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// Operator overload == to check for vector inequality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>True if the vectors are not equal, false otherwise</returns>
        public static bool operator !=(Vector3Df v1, Vector3Df v2)
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
            return obj is Vector3Df && obj.Equals(this);
        }

        /// <summary>
        /// Compare two vectors and check if they are equal
        /// </summary>
        /// <param name="other">Vector to check</param>
        /// <returns>True if the two vectors are equal, false otherwise</returns>
        public bool Equals(Vector3Df other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        /// <summary>
        /// Provide an integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return string.Format($"[Vector3Df] X({X}) Y({Y}) Z({Z})");
        }

        #endregion
    }
}
