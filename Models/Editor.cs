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

        private MainWindow mainWindow;
        private int[,] map;
        private int width;
        private int height;
        private int tileWidth;
        private int tileHeight;
        private List<CroppedBitmap> images = new List<CroppedBitmap>();

        #endregion

        #region Properties

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

        private void Initialize()
        {
            images.Clear();

            mainWindow.SetCanvas(width * tileWidth, height * tileHeight, tileWidth, tileHeight);
            mainWindow.SetCanvasContent(map, width, height, tileWidth, tileHeight);
        }

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

        internal void DiplayImages(List<BitmapImage> tiles)
        {
            foreach (BitmapImage tile in tiles)
            {
                CroppedBitmap croppedBitmap = new CroppedBitmap(tile, new Int32Rect(0, 0, tileWidth, tileHeight));
                images.Add(croppedBitmap);
            }

            mainWindow.SetImages(images);
        }

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

        internal void Repaint()
        {
            mainWindow.SetCanvasContent(map, width, height, tileWidth, tileHeight);
        }

        #endregion
    }
}
