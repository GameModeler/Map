using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Map.Annotations;
using Map.Utilities;
using Microsoft.Win32;

namespace Map.WPF.Utilities
{
    /// <summary>
    /// Utility class for interactions with the Windows OS.
    /// </summary>
    public static class WpfUtilities
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
            var openFileDialog = new OpenFileDialog()
            {
                Title = dialogTitle,
                Filter = string.Format(
                    $"{fileTypesLabel} ({MapUtilities.ConcatenateArrayWithTransformation(fileTypes, "; ", "*.", "")})" +
                    $"|{MapUtilities.ConcatenateArrayWithTransformation(fileTypes, "; ", "*.", "")}"),
                Multiselect = multiSelect
            };

            var result = openFileDialog.ShowDialog();

            return result == true ? openFileDialog : null;
        }

        /// <summary>
        /// Find all controls within a Dependency object
        /// </summary>
        /// <typeparam name="T">Type of objects to find</typeparam>
        /// <param name="d">Highest level object to begin the search in</param>
        /// <returns></returns>
        public static IEnumerable<T> FindAllByType<T>(DependencyObject d) where T : DependencyObject
        {
            if (d == null)
            {
                yield break;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                var child = VisualTreeHelper.GetChild(d, i);
                var type = child as T;

                if (type != null)
                {
                    yield return type;
                }

                foreach (var subChild in FindAllByType<T>(child))
                {
                    yield return subChild;
                }
            }
        }

        /// <summary>
        /// Find a parent of the given object in the visual tree
        /// </summary>
        /// <typeparam name="T">Type of the parent to find</typeparam>
        /// <param name="d">Object to find the parent from</param>
        /// <returns>The parent object or null if no object found</returns>
        [CanBeNull]
        public static T FindParent<T>(DependencyObject d) where T : DependencyObject
        {
            while (true)
            {
                d = VisualTreeHelper.GetParent(d);

                if (d == null)
                {
                    return null;
                }

                var t = d as T;

                if (t != null)
                {
                    return t;
                }
            }
        }

        /// <summary>
        /// Find a child of the given object in the visual tree
        /// </summary>
        /// <typeparam name="T">Type of the child</typeparam>
        /// <param name="d">Parent of the child</param>
        /// <param name="name">Name of the child</param>
        /// <returns></returns>
        [CanBeNull]
        public static T FindChild<T>(DependencyObject d, string name) where T : DependencyObject
        {
            if (d == null)
            {
                return null;
            }

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(d);

            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(d, i);
                var childType = child as T;

                if (childType == null)
                {
                    foundChild = FindChild<T>(child, name);

                    if (foundChild != null)
                        break;
                }
                else if (!string.IsNullOrEmpty(name))
                {
                    var frameworkElement = child as FrameworkElement;

                    if (frameworkElement == null || frameworkElement.Name != name)
                    {
                        continue;
                    }

                    foundChild = (T) child;
                    break;
                }
                else
                {
                    foundChild = (T) child;
                    break;
                }
            }

            return foundChild;
        }
    }
}
