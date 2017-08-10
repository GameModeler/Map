using System.Windows.Controls;
using System.Windows.Media;

namespace Map.WPF.Graphics
{
    /// <summary>
    /// Define the world (container of the whole map)
    /// </summary>
    public class World : Canvas
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public World() {}

        /// <summary>
        /// Construct the world map with a background brush
        /// </summary>
        /// <param name="background">Background brush of the world map</param>
        public World(Brush background) : this()
        {
            Background = background;
        }

        #endregion

        #region Methods

        #endregion
    }
}
