using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Map.Core.Structs
{
    /// <summary>
    /// Represent a time value
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Time : IEquatable<Time>
    {
        #region Imports

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfSeconds(float amount);

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfMilliseconds(int amount);

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfMicroseconds(long amount);

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfTime_asSeconds(Time time);

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int sfTime_asMilliseconds(Time time);

        [DllImport("csfml-system-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern long sfTime_asMicroseconds(Time time);

        #endregion

        #region Attributes

        /// <summary>
        /// Time's microseconds
        /// </summary>
        private readonly long microseconds;

        #endregion

        #region Properties

        /// <summary>
        /// Predefined "zero" time value
        /// </summary>
        public static readonly Time Zero = FromMicroseconds(0);

        #endregion

        #region Methods

        /// <summary>
        /// Construct a time value from a number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds</param>
        /// <returns>Time value from the amount of seconds</returns>
        public static Time FromSeconds(float seconds)
        {
            return sfSeconds(seconds);
        }

        /// <summary>
        /// Construct a time value from a number of millisecondss
        /// </summary>
        /// <param name="milliseconds">Number of millisecondss</param>
        /// <returns>Time value from the amount of millisecondss</returns>
        public static Time FromMilliseconds(int milliseconds)
        {
            return sfMilliseconds(milliseconds);
        }

        /// <summary>
        /// Construct a time value from a number of microseconds
        /// </summary>
        /// <param name="microseconds">Number of microseconds</param>
        /// <returns>Time value from the amount of microseconds</returns>
        public static Time FromMicroseconds(long microseconds)
        {
            return sfMicroseconds(microseconds);
        }

        /// <summary>
        /// Return the time value as a number of seconds
        /// </summary>
        /// <returns></returns>
        public float AsSeconds()
        {
            return sfTime_asSeconds(this);
        }

        /// <summary>
        /// Return the time value as a number of milliseconds
        /// </summary>
        /// <returns></returns>
        public int AsMilliseconds()
        {
            return sfTime_asMilliseconds(this);
        }

        /// <summary>
        /// Return the time value as a number of microseconds
        /// </summary>
        /// <returns></returns>
        public long AsMicroseconds()
        {
            return sfTime_asMicroseconds(this);
        }

        /// <summary>
        /// Compare two times and check if they are equal
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>Times are equal</returns>
        public static bool operator ==(Time first, Time second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Compare two times and check if they are not equal
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>Times are not equal</returns>
        public static bool operator !=(Time first, Time second)
        {
            return !first.Equals(second);
        }

        /// <summary>
        /// Overload of &lt; operator to compare two time values
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>True if first is lesser than second</returns>
        public static bool operator <(Time first, Time second)
        {
            return first.AsMicroseconds() < second.AsMicroseconds();
        }

        /// <summary>
        /// Overload of &gt; operator to compare two time values
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>True if first is greater than second</returns>
        public static bool operator >(Time first, Time second)
        {
            return first.AsMicroseconds() > second.AsMicroseconds();
        }

        /// <summary>
        /// Overload of &lt;= operator to compare two time values
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>True if first is lesser than or equal to second</returns>
        public static bool operator <=(Time first, Time second)
        {
            return first.AsMicroseconds() <= second.AsMicroseconds();
        }

        /// <summary>
        /// Overload of &gt;= operator to compare two time values
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>True if first is greater than or equal to second</returns>
        public static bool operator >=(Time first, Time second)
        {
            return first.AsMicroseconds() >= second.AsMicroseconds();
        }

        /// <summary>
        /// Overload of binary operator - to substract two time values
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>Difference of the two time values</returns>
        public static Time operator -(Time first, Time second)
        {
            return FromMicroseconds(first.AsMicroseconds() - second.AsMicroseconds());
        }

        /// <summary>
        /// Overload of binary operator + to add two time values
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>Sum of the two time values</returns>
        public static Time operator +(Time first, Time second)
        {
            return FromMicroseconds(first.AsMicroseconds() + second.AsMicroseconds());
        }

        /// <summary>
        /// Overload of binary operator * to scale a time value
        /// </summary>
        /// <param name="time">Time value</param>
        /// <param name="seconds">Seconds</param>
        /// <returns>Time value multiplied by amount of seconds</returns>
        public static Time operator *(Time time, float seconds)
        {
            return FromSeconds(time.AsSeconds() * seconds);
        }

        /// <summary>
        /// Overload of binary operator * to scale a time value
        /// </summary>
        /// <param name="time">Time value</param>
        /// <param name="microseconds">Microseconds</param>
        /// <returns>Time value multiplied by amount of microseconds</returns>
        public static Time operator *(Time time, long microseconds)
        {
            return FromMicroseconds(time.AsMicroseconds() * microseconds);
        }

        /// <summary>
        /// Overload of binary operator * to scale a time value
        /// </summary>
        /// <param name="seconds">Seconds</param>
        /// <param name="time">Time value</param>
        /// <returns>Amount of seconds multiplied by a time value</returns>
        public static Time operator *(float seconds, Time time)
        {
            return FromSeconds(seconds * time.AsSeconds());
        }

        /// <summary>
        /// Overload of binary operator * to scale a time value
        /// </summary>
        /// <param name="microseconds">Microseconds</param>
        /// <param name="time">Time value</param>
        /// <returns>Amount of microseconds multiplied by a time value</returns>
        public static Time operator *(long microseconds, Time time)
        {
            return FromMicroseconds(microseconds * time.AsMicroseconds());
        }

        /// <summary>
        /// Overload of binary operator / to scale a time value
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>First time value divided by second time value</returns>
        public static Time operator /(Time first, Time second)
        {
            return FromMicroseconds(first.AsMicroseconds() / second.AsMicroseconds());
        }

        /// <summary>
        /// Overload of binary operator / to scale a time value
        /// </summary>
        /// <param name="time">Time value</param>
        /// <param name="seconds">Seconds</param>
        /// <returns>Time value divided by amount of seconds</returns>
        public static Time operator /(Time time, float seconds)
        {
            return FromSeconds(time.AsSeconds() / seconds);
        }

        /// <summary>
        /// Overload of binary operator / to scale a time value
        /// </summary>
        /// <param name="time">Time value</param>
        /// <param name="microseconds">Microseconds</param>
        /// <returns>Time value divided by amount of microseconds</returns>
        public static Time operator /(Time time, long microseconds)
        {
            return FromMicroseconds(time.AsMicroseconds() / microseconds);
        }

        /// <summary>
        /// Overload of binary operator % to compute remainder of a time value
        /// </summary>
        /// <param name="first">First time value</param>
        /// <param name="second">Second time value</param>
        /// <returns>First time value modulo of second time value</returns>
        public static Time operator %(Time first, Time second)
        {
            return FromMicroseconds(first.AsMicroseconds() % second.AsMicroseconds());
        }

        /// <summary>
        /// Compare time and object and check if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and time are equal</returns>
        public override bool Equals(object obj)
        {
            return (obj is Time) && obj.Equals(this);
        }

        /// <summary>
        /// Compare two times and check if they are equal
        /// </summary>
        /// <param name="other">Time to check</param>
        /// <returns>Times are equal</returns>
        public bool Equals(Time other)
        {
            return microseconds == other.microseconds;
        }

        /// <summary>
        /// Provide an integer describing the object
        /// </summary>
        /// <returns>Integer value describing the object</returns>
        public override int GetHashCode()
        {
            return microseconds.GetHashCode();
        }

        #endregion
    }
}
