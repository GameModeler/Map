using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Map.WPF.Commands
{
    /// <summary>
    /// Generic relay command to call ViewModel actions from a view
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Attributes

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        #endregion

        #region Properties

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a relay command with an action to perform if it can be executed
        /// </summary>
        /// <param name="execute">Action to perform</param>
        /// <param name="canExecute">True if the action can be executed, false otherwise</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determinate if the action can be executed
        /// </summary>
        /// <param name="parameter">Parameter sent from the caller</param>
        /// <returns>True if the action can be executed, false otherwise</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// Execute the action to perform
        /// </summary>
        /// <param name="parameter">Parameter sent from the caller</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
