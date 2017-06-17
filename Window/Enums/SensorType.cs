namespace Map.Window.Enums
{
    /// <summary>
    /// Sensor types
    /// </summary>
    public enum SensorType
    {
        /// <summary>
        /// Measure the raw acceleration (m/s^2)
        /// </summary>
        Accelerometer,

        /// <summary>
        /// Measure the raw rotation rates (degrees/s)
        /// </summary>
        Gyroscope,

        /// <summary>
        /// Measure the ambient magnetic field (micro-teslas)
        /// </summary>
        Magnetometer,

        /// <summary>
        /// Measure the direction and intensity of gravity
        /// Independant of device acceleration (m/s^2)
        /// </summary>
        Gravity,

        /// <summary>
        /// Measure the direction and intensity of device acceleration
        /// Independant of the gravity (m/s^2)
        /// </summary>
        UserAcceleration,

        /// <summary>
        /// Measure the absolute 3D orientation (degrees)
        /// </summary>
        Orientation,

        /// <summary>
        /// The total number of sensor types (keep in last position)
        /// </summary>
        TypeCount
    }
}
