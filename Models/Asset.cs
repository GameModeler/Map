using System;
using System.Drawing;
using System.IO;

namespace Map.Models
{
    /// <summary>
    /// Represents an asset of the map.
    /// </summary>
    public class Asset
    {
        #region Properties

        /// <summary>
        /// Asset's location on the storage drive.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Asset's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Asset's pixel data with it's attributes.
        /// </summary>
        public Image Picture { get; set; }

        /// <summary>
        /// Asset's selection state.
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Asset initialization.
        /// </summary>
        /// <param name="location">The asset's location on the storage drive.</param>
        public Asset(string location)
        {
            Location = location;
            Name = GetName();
            Picture = new Bitmap(Location);
            IsSelected = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the asset's name based on it's location.
        /// Takes an optional boolean parameter to include the extension in the name (default is false).
        /// </summary>
        /// <param name="withExtension">A boolean value to get the extension in the name.</param>
        /// <returns>The name of the asset.</returns>
        public string GetName(bool withExtension = false)
        {
            return withExtension ? Path.GetFileName(Location) : Path.GetFileNameWithoutExtension(Location);
        }

        /// <summary>
        /// Returns the full location path of the asset on the storage drive.
        /// </summary>
        /// <returns>The full path to the asset.</returns>
        public override string ToString()
        {
            return Location;
        }

        #endregion
    }
}
