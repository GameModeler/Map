using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Map.Models
{
    class Editor
    {
        #region Attributes

        /// <summary>
        /// Window containing the map.
        /// </summary>
        private MainWindow mainWindow;

        /// <summary>
        /// Array representing the map.
        /// </summary>
        private int[,] map;

        /// <summary>
        /// Width of the map (number of squares).
        /// </summary>
        private int width;

        /// <summary>
        /// Height of the map (number of squares).
        /// </summary>
        private int height;

        /// <summary>
        /// Width of a tile.
        /// </summary>
        private int tileWidth;

        /// <summary>
        /// Height of a tile.
        /// </summary>
        private int tileHeight;

        /// <summary>
        /// Images available to paint the map.
        /// </summary>
        private List<CroppedBitmap> images = new List<CroppedBitmap>();

        #endregion

        #region Properties

        /// <summary>
        /// Boolean value representing the state of the Map (not saved if false).
        /// </summary>
        public bool Dirty { get; set; }

        #endregion

        #region Constructors

        public Editor(MainWindow mainWindow, int width, int height, int tileWidth, int tileHeight)
            : this(mainWindow)
        {
            SetProperties(width, height, tileWidth, tileHeight);
            Dirty = true;

            Initialize();
        }

        public Editor(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the map by clearing the images and creating an empty map.
        /// </summary>
        private void Initialize()
        {
            images.Clear();

            mainWindow.SetCanvas(width * tileWidth, height * tileHeight, tileWidth, tileHeight);
            mainWindow.SetCanvasContent(map, width, height, tileWidth, tileHeight);
        }

        /// <summary>
        /// Sets the map properties. 
        /// </summary>
        /// <param name="width">Number of horizontal squares.</param>
        /// <param name="height">Number of vertical squares.</param>
        /// <param name="tileWidth">Width of a tile.</param>
        /// <param name="tileHeight">Height of a tile.</param>
        internal void SetProperties(int width, int height, int tileWidth, int tileHeight)
        {
            if (map == null)
            {
                map = new int[width, height];
                this.width = width;
                this.height = height;
                this.tileWidth = tileWidth;
                this.tileHeight = tileHeight;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        map[j, i] = -1;
                    }
                }
            }
            else
            {
                var newMap = new int[width, height];

                for (var i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        newMap[j, i] = -1;
                    }
                }

                for (int i = 0; i < this.height; i++)
                {
                    for (int j = 0; j < this.width; j++)
                    {
                        if (i < height && j < width)
                        {
                            newMap[j, i] = map[j, i];
                        }
                    }
                }

                this.width = width;
                this.height = height;
                this.tileWidth = tileWidth;
                this.tileHeight = tileHeight;

                map = newMap;
            }
        }

        /// <summary>
        /// Crops the images and adds them to the UI.
        /// </summary>
        /// <param name="tiles">Images to add to the UI.</param>
        internal void DiplayImages(List<BitmapImage> tiles)
        {
            foreach (BitmapImage tile in tiles)
            {
                CroppedBitmap croppedBitmap = new CroppedBitmap(tile, new Int32Rect(0, 0, tileWidth, tileHeight));
                images.Add(croppedBitmap);
            }

            mainWindow.SetImages(images);
        }

        /// <summary>
        /// Saves the map.
        /// </summary>
        /// <param name="s">Name of the save file.</param>
        internal void Save(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    stringBuilder.Append(map[j, i] + ",");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1).AppendLine("");
            }
            File.WriteAllText(s, stringBuilder.ToString());
        }

        /// <summary>
        /// Loads a map from a save file.
        /// </summary>
        /// <param name="file">Save file.</param>
        internal void LoadMap(string file)
        {
            string[] lines = File.ReadAllLines(file);

            width = lines[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length;
            height = lines.Length;

            map = new int[width, height];

            int x = 0;
            int y = 0;

            foreach (var line in lines)
            {
                x = 0;
                foreach (var num in line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    map[x, y] = int.Parse(num);
                    x++;
                }
                y++;
            }

            Initialize();
            Repaint();
        }

        /// <summary>
        /// Draws a loaded image in a square of the map.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="p"></param>
        internal void Draw(Point point, int p)
        {
            int x = (int) point.X / tileWidth;
            int y = (int) point.Y / tileHeight;

            if (x >= 0 && x < width && y >= 0 && y < height && map[x, y] != p)
            {
                map[x, y] = p;
                Dirty = true;
                mainWindow.SetCanvasContentDrawOver(x, y, tileWidth, tileHeight, p);
            }
        }

        /// <summary>
        /// Paints the map after loading a save.
        /// </summary>
        internal void Repaint()
        {
            mainWindow.SetCanvasContent(map, width, height, tileWidth, tileHeight);
        }

        #endregion
    }
}
