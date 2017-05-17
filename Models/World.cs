using System.Collections.Generic;

namespace Map.Models
{
    /// <summary>
    /// Represents a map.
    /// </summary>
    public class World
    {
        #region Properties

        /// <summary>
        /// Map's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Map's width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Map's height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Map's background.
        /// </summary>
        public Asset Background { get; set; }

        /// <summary>
        /// Layers of the map.
        /// </summary>
        public IList<Layer> Layers { get; set; }

        /// <summary>
        /// Map's save state.
        /// </summary>
        public bool IsSaved { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// World default initialization.
        /// </summary>
        public World()
        {
            Layers = new List<Layer>();
            IsSaved = false;
        }

        /// <summary>
        /// World initialization with a name, a width and a height. 
        /// </summary>
        /// <param name="name">The map's name.</param>
        /// <param name="width">The map's width.</param>
        /// <param name="height">The map's height.</param>
        public World(string name, int width, int height) : this()
        {
            Name = name;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// World initialization with a name, a width, a height and a starting saved state.
        /// </summary>
        /// <param name="name">The map's name.</param>
        /// <param name="width">The map's width.</param>
        /// <param name="height">The map's height.</param>
        /// <param name="isSaved">The map's starting saved state.</param>
        public World(string name, int width, int height, bool isSaved) : this(name, width, height)
        {
            IsSaved = isSaved;
        }

        /// <summary>
        /// World initialization with a name, a width, a height and a background.
        /// </summary>
        /// <param name="name">The map's name.</param>
        /// <param name="width">The map's width.</param>
        /// <param name="height">The map's height.</param>
        /// <param name="background">The map's background.</param>
        public World(string name, int width, int height, Asset background) : this(name, width, height)
        {
            Background = background;
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
