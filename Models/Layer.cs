using System.Collections.Generic;

namespace Map.Models
{
    /// <summary>
    /// Represents a map layer.
    /// </summary>
    public class Layer
    {
        #region Properties

        /// <summary>
        /// Layer's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Layer's background.
        /// </summary>
        public Asset Background { get; set; }

        /// <summary>
        /// Cells width.
        /// </summary>
        public int CellWidth { get; set; }

        /// <summary>
        /// Cells height.
        /// </summary>
        public int CellHeight { get; set; }

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
            Cells = new List<Cell>();
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
