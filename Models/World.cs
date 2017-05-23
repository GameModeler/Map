using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Map.Models.Base;

namespace Map.Models
{
    /// <summary>
    /// Represents a map.
    /// </summary>
    [Serializable]
    public class World : BaseModel
    {
        #region Attributes

        private string _name;
        private int _width;
        private int _height;
        private int _cellWidth;
        private int _cellHeight;
        private Asset _background;
        private bool _isSaved;
        private bool _isModified;

        #endregion

        #region Properties

        /// <summary>
        /// Map's name.
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
        /// Map's width.
        /// </summary>
        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Map's height.
        /// </summary>
        public int Height {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Cells width.
        /// </summary>
        public int CellWidth
        {
            get => _cellWidth;
            set
            {
                _cellWidth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Cells height.
        /// </summary>
        public int CellHeight
        {
            get => _cellHeight;
            set
            {
                _cellHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Map's background.
        /// </summary>
        public Asset Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Layers of the map.
        /// </summary>
        public IList<Layer> Layers { get; set; }

        /// <summary>
        /// Map's save state.
        /// </summary>
        public bool IsSaved
        {
            get => _isSaved;
            set
            {
                _isSaved = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Map's modification state.
        /// </summary>
        public bool IsModified
        {
            get => _isModified;
            set
            {
                _isModified = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// World default initialization.
        /// </summary>
        public World()
        {
            Name = "My Map";
            Width = 15;
            Height = 10;
            CellWidth = 64;
            CellHeight = 64;
            Layers = new ObservableCollection<Layer>();
            IsSaved = false;
            IsModified = true;
        }

        /// <summary>
        /// World initialization with a name, a width and a height. 
        /// </summary>
        /// <param name="name">The map's name.</param>
        /// <param name="width">The map's width.</param>
        /// <param name="height">The map's height.</param>
        /// <param name="cellWidth">Width of a cell.</param>
        /// <param name="cellHeight">Height of a cell.</param>
        /// <param name="background">The map's background.</param>
        public World(string name, int width, int height, int cellWidth, int cellHeight, Asset background) : this()
        {
            Name = name;
            Width = width;
            Height = height;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
            Background = background;
        }

        /// <summary>
        /// World initialization with a name, a width, a height and a starting saved state.
        /// </summary>
        /// <param name="name">The map's name.</param>
        /// <param name="width">The map's width.</param>
        /// <param name="height">The map's height.</param>
        /// <param name="cellWidth">Width of a cell.</param>
        /// <param name="cellHeight">Height of a cell.</param>
        /// <param name="background">The map's background.</param>
        /// <param name="isSaved">The map's starting saved state.</param>
        public World(string name, int width, int height, int cellWidth, int cellHeight, Asset background, bool isSaved) : this(name, width, height, cellWidth, cellHeight, background)
        {
            IsSaved = isSaved;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the map's informations.
        /// </summary>
        /// <returns>The map's properties as a string.</returns>
        public override string ToString()
        {
            return string.Format($"Map - Name: {Name}, Size: [{Width}, {Height}], Background: {Background.GetName(true)}");
        }

        #endregion
    }
}
