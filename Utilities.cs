using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using Map.Annotations;

namespace Map
{
    /// <summary>
    /// Utility class to bring convenience
    /// </summary>
    public static class Utilities
    {
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
        /// Get all children inside a dependency object
        /// </summary>
        /// <param name="root">The dependency object to search in</param>
        /// <returns>The children found</returns>
        public static IEnumerable<DependencyObject> FindChildren(this DependencyObject root)
        {
            var count = VisualTreeHelper.GetChildrenCount(root);

            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(root, i);
                yield return child;

                foreach (var descendants in child.FindChildren())
                {
                    yield return descendants;
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

                    foundChild = (T)child;
                    break;
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// Browse to open one or more files using the standard Windows open dialog
        /// </summary>
        /// <param name="dialogTitle">The title of the dialog</param>
        /// <param name="fileTypesLabel">The global label of the allowed file types</param>
        /// <param name="fileTypes">An array of allowed file types (jpg, png, etc...)</param>
        /// <param name="multiSelect">A boolean value to enable multi select (default is false)</param>
        /// <returns>The list of files to open</returns>
        public static OpenFileDialog BrowseFiles(string dialogTitle, string fileTypesLabel, string[] fileTypes, bool multiSelect = false)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Title = dialogTitle,
                Filter = string.Format(
                    $"{fileTypesLabel} ({ConcatenateArrayWithTransformation(fileTypes, "; ", "*.", "")})" +
                    $"|{ConcatenateArrayWithTransformation(fileTypes, "; ", "*.", "")}"),
                Multiselect = multiSelect
            };

            var result = openFileDialog.ShowDialog();

            return result == true ? openFileDialog : null;
        }

        /// <summary>
        /// Clones a serializable object to create a copy
        /// Value and reference types are all copied meaning changes made to the copy won't impact the source and vice-versa
        /// </summary>
        /// <typeparam name="T">The type of object to clone</typeparam>
        /// <param name="source">The source object to clone from</param>
        /// <returns>The copied object</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("Not serializable", nameof(source));
            }

            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Concatenate strings in a list with a separator and a transformation applied to each item
        /// </summary>
        /// <param name="stringArray">The string array to concatenate</param>
        /// <param name="separator">The separator</param>
        /// <param name="prefix">The prefix string to add to each item</param>
        /// <param name="suffix">The suffix string to add to each item</param>
        /// <returns></returns>
        public static string ConcatenateArrayWithTransformation(IEnumerable<string> stringArray, string separator, string prefix, string suffix)
        {
            return string.Format($"{string.Join(separator, stringArray.Select(s => string.Format($"{prefix}{s.ToString()}{suffix}")).ToArray())}");
        }
    }
}
