using System;
using Map.Window.Enums;

namespace Map.Window.Events
{
    /// <summary>
    /// Sensor event parameters
    /// </summary>
    public class SensorEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Type of the sensor
        /// </summary>
        public SensorType Type { get; set; }

        /// <summary>
        /// Current value of the sensor on X axis
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Current value of the sensor on Y axis
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Current value of the sensor on Z axis
        /// </summary>
        public float Z { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the sensor arguments from a sensor event
        /// </summary>
        /// <param name="e">Sensor event</param>
        public SensorEventArgs(SensorEvent e)
        {
            Type = e.Type;
            X = e.X;
            Y = e.Y;
            Z = e.Z;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String descritption of the object</returns>
        public override string ToString()
        {
            return string.Format("[SensorEventArgs] " +
                                 $"Type({Type}) " +
                                 $"X({X}) " +
                                 $"Y({Y}) " +
                                 $"Z({Z})");
        }

        #endregion
    }
}
