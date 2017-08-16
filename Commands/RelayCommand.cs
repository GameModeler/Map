using System;
using System.Windows.Input;

namespace Map.Commands
{
    /// <summary>
    /// Generic relay command to call an action from a view
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Attributes

        /// <summary>
        /// Condition for the action to execute
        /// </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Action to execute
        /// </summary>
        private readonly Action<object> _execute;

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a relay command
        /// </summary>
        /// <param name="execute">Action method to execute</param>
        /// <param name="canExecute">Condition method to decide if the action can be executed</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Condition logic for the action to execute
        /// Return true if canExecute is null
        /// </summary>
        /// <param name="parameter">Parameter sent by the caller</param>
        /// <returns>True if the action can be exxecuted, false otherwise</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// Action logic to execute
        /// </summary>
        /// <param name="parameter">Parameter sent by the caller</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
