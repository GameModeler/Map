using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace Map.Utilities
{
    /// <summary>
    /// Utility class for interactions with the Windows OS.
    /// </summary>
    public static class WindowsInteractions
    {
        /// <summary>
        /// Generic message box to notify the user and get back a response depending on the type being used.
        /// </summary>
        /// <param name="title">The title of the message box.</param>
        /// <param name="message">The notification message.</param>
        /// <param name="type">The type of response wanted from the user.</param>
        /// <param name="icon">The icon linked to the message.</param>
        /// <returns>The user's response.</returns>
        public static MessageBoxResult ShowMessage(string title, string message, MessageBoxButton type, MessageBoxImage icon)
        {
            return MessageBox.Show(message, title, type, icon);
        }

        /// <summary>
        /// Browse to open one or more files using the standard Windows open dialog.
        /// </summary>
        /// <param name="dialogTitle">The title of the dialog.</param>
        /// <param name="fileTypesLabel">The global label of the allowed file types.</param>
        /// <param name="fileTypes">An array of allowed file types (jpg, png, etc...).</param>
        /// <param name="multiSelect">A boolean value to enable multi select (default is false).</param>
        /// <returns>The list of files to open.</returns>
        public static OpenFileDialog BrowseFiles(string dialogTitle, string fileTypesLabel, string[] fileTypes, bool multiSelect = false)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = dialogTitle,
                Filter = string.Format($"{fileTypesLabel} ({ConcatenateArrayWithTransformation(fileTypes, "; ", "*.", "")})|{ConcatenateArrayWithTransformation(fileTypes, "; ", "*.", "")}"),
                Multiselect = multiSelect
            };

            bool? result = openFileDialog.ShowDialog();

            return result == true ? openFileDialog : null;
        }

        /// <summary>
        /// Concatenate strings in a list with a separator and a transformation applied to each item.
        /// </summary>
        /// <param name="stringArray">The string array to concatenate.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="prefix">The prefix string to add to each item.</param>
        /// <param name="suffix">The suffix string to add to each item.</param>
        /// <returns></returns>
        private static string ConcatenateArrayWithTransformation(IEnumerable<string> stringArray, string separator, string prefix, string suffix)
        {
            return string.Format($"{string.Join(separator, stringArray.Select(s => string.Format($"{prefix}{s.ToString()}{suffix}")).ToArray())}");
        }
    }
}
