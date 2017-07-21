using System;
using System.Drawing;
using System.IO;
using Map.Models.Base;

namespace Map.Models
{
    /// <summary>
    /// Represents an asset of the map.
    /// </summary>
    [Serializable]
    public class Asset : BaseModel
    {
        #region Attributes

        private string _location;
        private string _name;
        private Image _picture;
        private bool _isSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Asset's location on the storage drive.
        /// </summary>
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Asset's name.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Asset's pixel data with it's attributes.
        /// </summary>
        public Image Picture
        {
            get => _picture;
            set
            {
                _picture = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Asset's selection state.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Asset initialization with a location.
        /// </summary>
        /// <param name="location">The asset's location on the storage drive.</param>
        public Asset(string location)
        {
            Location = location;
            Name = GetName();
            Picture = new Bitmap(Location);
            IsSelected = false;
        }

        /// <summary>
        /// Asset initialization with a location and a custom name.
        /// </summary>
        /// <param name="location">The asset's location on the storage drive.</param>
        /// <param name="name">Custom name of the asset</param>
        public Asset(string location, string name) : this(location)
        {
            Name = name;
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
        /// Returns the full location path or the name of the asset
        /// depending on the path boolean value
        /// </summary>
        /// <returns>The full path to the asset.</returns>
        public string ToString(bool path = false)
        {
            return path ? Location : Name;
        }

        #endregion
    }
}
