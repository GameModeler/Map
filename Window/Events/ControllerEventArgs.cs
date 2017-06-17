using System;
using Map.Window.Enums;

namespace Map.Window.Events
{
    /// <summary>
    /// Controller connection / disconnection event parameters
    /// </summary>
    public class ControllerConnectEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Index of the controller which triggered the event
        /// </summary>
        public int ControllerId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the controller connect arguments from a controller connect event
        /// </summary>
        /// <param name="e">Controller connect event</param>
        public ControllerConnectEventArgs(ControllerConnectEvent e)
        {
            ControllerId = e.ControllerId;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String descritption of the object</returns>
        public override string ToString()
        {
            return string.Format("[ControllerConnectEventArgs] " +
                                 $"ControllerId({ControllerId})");
        }

        #endregion
    }

    /// <summary>
    /// Controller axis move event parameters
    /// </summary>
    public class ControllerMoveEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Index of the controller which triggered the event
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// Controller axis
        /// </summary>
        public ControllerAxis Axis { get; set; }

        /// <summary>
        /// Current position of the axis
        /// </summary>
        public float Position { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the controller move arguments from a controller move event
        /// </summary>
        /// <param name="e">Controller move event</param>
        public ControllerMoveEventArgs(ControllerMoveEvent e)
        {
            ControllerId = e.ControllerId;
            Axis = e.Axis;
            Position = e.Position;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String descritption of the object</returns>
        public override string ToString()
        {
            return string.Format("[ControllerMoveEventArgs] " +
                                 $"ControllerId({ControllerId}) " +
                                 $"Axis({Axis}) " +
                                 $"Position({Position})");
        }

        #endregion
    }

    /// <summary>
    /// Controller buttons event parameters
    /// </summary>
    public class ControllerButtonEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Index of the controller which triggered the event
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// Index of the button
        /// </summary>
        public int Button { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the controller button arguments from a controller button event
        /// </summary>
        /// <param name="e">Controller button event</param>
        public ControllerButtonEventArgs(ControllerButtonEvent e)
        {
            ControllerId = e.ControllerId;
            Button = e.Button;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String descritption of the object</returns>
        public override string ToString()
        {
            return string.Format("[ControllerMoveEventArgs] " +
                                 $"ControllerId({ControllerId}) " +
                                 $"Button({Button})");
        }

        #endregion
    }
}
