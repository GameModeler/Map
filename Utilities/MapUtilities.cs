using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Map.Properties;

namespace Map.Utilities
{
    /// <summary>
    /// Cross platform utility class
    /// </summary>
    public static class MapUtilities
    {
        /// <summary>
        /// Clones a serializable object to create a copy.
        /// Value and reference types are all copied meaning changes made to the copy won't impact the source and vice-versa.
        /// </summary>
        /// <typeparam name="T">The type of object to clone.</typeparam>
        /// <param name="source">The source object to clone from.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException(Resources.serializable_type, nameof(source));
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
                return (T) formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Concatenate strings in a list with a separator and a transformation applied to each item.
        /// </summary>
        /// <param name="stringArray">The string array to concatenate.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="prefix">The prefix string to add to each item.</param>
        /// <param name="suffix">The suffix string to add to each item.</param>
        /// <returns></returns>
        public static string ConcatenateArrayWithTransformation(IEnumerable<string> stringArray, string separator, string prefix, string suffix)
        {
            return string.Format($"{string.Join(separator, stringArray.Select(s => string.Format($"{prefix}{s.ToString()}{suffix}")).ToArray())}");
        }
    }
}
