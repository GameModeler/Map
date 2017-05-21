using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Map.Properties;

namespace Map.Utilities
{
    public class MapUtilities
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
    }
}
