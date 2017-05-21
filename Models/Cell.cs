using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Map.Models.Base;

namespace Map.Models
{
    /// <summary>
    /// Represents a cell of the map.
    /// </summary>
    [Serializable]
    public class Cell : BaseModel
    {
        #region Attributes

        private Point _position;

        #endregion

        #region Properties

        /// <summary>
        /// Cell's position on the map.
        /// </summary>
        public Point Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Collection of assets inside the cell.
        /// </summary>
        public IList<Asset> Assets { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Cell initialization.
        /// </summary>
        public Cell()
        {
            Assets = new ObservableCollection<Asset>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a string identifying the cell in the map.
        /// </summary>
        /// <returns>A string with the cell's coordinates.</returns>
        public override string ToString()
        {
            return string.Format($"Cell [{Position.X}, {Position.Y}]");
        }

        #endregion
    }
}
