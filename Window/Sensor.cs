using System.Runtime.InteropServices;
using System.Security;
using Map.Core.Structs;
using Map.Window.Enums;

namespace Map.Window
{
    public static class Sensor
    {
        #region Imports

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfSensor_isAvailable(SensorType type);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSensor_setEnabled(SensorType type, bool enabled);

        [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3Df sfSensor_getValue(SensorType type);

        #endregion

        #region Methods

        /// <summary>
        /// Check if a sensor is available on the underlying platform
        /// </summary>
        /// <param name="type">Sensor to check</param>
        /// <returns>True if the sensor is available, false otherwise</returns>
        public static bool IsAvailable(SensorType type)
        {
            return sfSensor_isAvailable(type);
        }

        /// <summary>
        /// Enable or disable a sensor
        /// </summary>
        /// <param name="type">Sensor to enable or disable</param>
        /// <param name="enabled">True to enable, false to disable</param>
        public static void SetEnabled(SensorType type, bool enabled)
        {
            sfSensor_setEnabled(type, enabled);
        }

        /// <summary>
        /// Get the current sensor value
        /// </summary>
        /// <param name="type">Sensor to read the value from</param>
        /// <returns>The current sensor value</returns>
        public static Vector3Df GetValue(SensorType type)
        {
            return sfSensor_getValue(type);
        }

        #endregion
    }
}
