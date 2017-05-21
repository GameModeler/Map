using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Map.Models.Base;

namespace Map.Models
{
    /// <summary>
    /// Represents a map layer.
    /// </summary>
    [Serializable]
    public class Layer : BaseModel
    {
        #region Attributes

        private string _name;
        private Asset _background;
        private bool _isSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Layer's name.
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
        /// Layer's background.
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
        /// Layer's selection state.
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

        /// <summary>
        /// Collection of cells in the layer.
        /// </summary>
        public IList<Cell> Cells { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Layer initialization.
        /// </summary>
        public Layer()
        {
            Cells = new ObservableCollection<Cell>();
            IsSelected = false;
        }

        /// <summary>
        /// Layer initialization with a name.
        /// </summary>
        /// <param name="name">The name of the layer.</param>
        public Layer(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Layer initialization with a name and a background.
        /// </summary>
        /// <param name="name">The name of the layer.</param>
        /// <param name="background">The background of the layer.</param>
        public Layer(string name, Asset background) : this(name)
        {
            Background = background;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the name of the layer.
        /// </summary>
        /// <returns>The name of the layer.</returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
