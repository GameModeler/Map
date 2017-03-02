using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Map.Models;
using Microsoft.Win32;

namespace Map
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributes

        /// <summary>
        /// Instance of the Editor class.
        /// </summary>
        private Editor editor;
        private double offsetX;
        private double offsetY;

        /// <summary>
        /// Zoom applied to the map.
        /// </summary>
        private double zoom = 1;

        /// <summary>
        /// 
        /// </summary>
        private Point prevPosition;

        #endregion

        #region Constructors

        public MainWindow()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, ExitCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Properties, PropertiesCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Replace, TileSetCommand));

            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Command executed to create a new map.
        /// </summary>
        /// <param name="sender">Object triggering the command.</param>
        /// <param name="args">Facultative arguments.</param>
        public void NewCommand(object sender, RoutedEventArgs args)
        {
            if (editor != null && editor.Dirty)
            {
                if (!Save())
                {
                    return;
                }
            }

            editor = NewMap();
        }

        /// <summary>
        /// Command executed to save a map.
        /// </summary>
        /// <param name="sender">Object triggering the command.</param>
        /// <param name="args">Facultative arguments.</param>
        public void SaveCommand(object sender, RoutedEventArgs args)
        {
            if (editor != null)
            {
                Save();
            }
        }

        /// <summary>
        /// Command executed to load a map from a save.
        /// </summary>
        /// <param name="sender">Object triggering the command.</param>
        /// <param name="args">Facultative arguments.</param>
        public void OpenCommand(object sender, RoutedEventArgs args)
        {
            string file = OpenFile(".txt")[0];

            if (file != "")
            {
                if (editor != null && editor.Dirty)
                {
                    if (!Save())
                    {
                        return;
                    }
                }
                else if (editor == null)
                {
                    editor = new Editor(this);
                }

                editor.LoadMap(file);
            }
        }

        /// <summary>
        /// Command executed to exit the editor (checks if the map has been saved).
        /// </summary>
        /// <param name="sender">Object triggering the command.</param>
        /// <param name="args">Facultative arguments.</param>
        public void ExitCommand(object sender, RoutedEventArgs args)
        {
            if (editor == null || !editor.Dirty || Save())
            {
                Close();
            }
        }

        /// <summary>
        /// Command executed to add new images.
        /// </summary>
        /// <param name="sender">Object triggering the command.</param>
        /// <param name="args">Facultative arguments.</param>
        public void TileSetCommand(object sender, RoutedEventArgs args)
        {
            if (editor != null)
            {
                List<BitmapImage> bitmapImages = new List<BitmapImage>();
                foreach (string file in OpenFile(".png", true))
                {
                    bitmapImages.Add(new BitmapImage(new Uri(file)));
                }
                editor.DiplayImages(bitmapImages);
            }
        }

        /// <summary>
        /// Command executed to change map properties.
        /// </summary>
        /// <param name="sender">Object triggering the command.</param>
        /// <param name="args">Facultative arguments.</param>
        public void PropertiesCommand(object sender, RoutedEventArgs args)
        {
            if (editor != null)
            {
                MapPropertyWindow mapPropertyWindow = new MapPropertyWindow();
                mapPropertyWindow.ShowDialog();

                editor.SetProperties(int.Parse(mapPropertyWindow.WidthTextBox.Text), int.Parse(mapPropertyWindow.HeightTextBox.Text), int.Parse(mapPropertyWindow.TileWidthTextBox.Text), int.Parse(mapPropertyWindow.TileHeightTextBox.Text));
            }
        }

        /// <summary>
        /// Creates a new map after asking for its properties.
        /// </summary>
        /// <returns>An instance of an Editor containing the map.</returns>
        private Editor NewMap()
        {
            try
            {
                MapPropertyWindow mapPropertyWindow = new MapPropertyWindow();
                mapPropertyWindow.ShowDialog();

                zoom = 1;
                offsetX = 0;
                offsetY = 0;

                return new Editor(this, int.Parse(mapPropertyWindow.WidthTextBox.Text), int.Parse(mapPropertyWindow.HeightTextBox.Text), int.Parse(mapPropertyWindow.TileWidthTextBox.Text), int.Parse(mapPropertyWindow.TileHeightTextBox.Text));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the "container" canvas of the map.
        /// </summary>
        /// <param name="width">Number of horizontal squares.</param>
        /// <param name="height">Number of vertical squares.</param>
        /// <param name="tileWidth">Width of a tile.</param>
        /// <param name="tileHeight">Height of a tile.</param>
        public void SetCanvas(int width, int height, int tileWidth, int tileHeight)
        {
            Canvas1.Children.Clear();
            Canvas1.Children.Add(new Rectangle()
            {
                Width = width,
                Height = height,
                Stroke = Brushes.LightGray,
                Fill = Brushes.LightGray
            });

            for (int i = 0; i < height; i += tileHeight)
            {
                for (int j = 0; j < width; j += tileWidth)
                {
                    Canvas1.Children.Add(new Rectangle()
                    {
                        Width = tileWidth,
                        Height = tileHeight,
                        Margin = new Thickness(j, i, 0, 0),
                        Stroke = Brushes.Gray,
                        StrokeThickness = 1
                    });
                }
            }
        }

        /// <summary>
        /// Sets the content of the map
        /// </summary>
        /// <param name="map">Existing map to fill this content.</param>
        /// <param name="width">Number of horizontal squares.</param>
        /// <param name="height">Number of vertical squares.</param>
        /// <param name="tileWidth">Width of a tile.</param>
        /// <param name="tileHeight">Height of a tile.</param>
        internal void SetCanvasContent(int[,] map, int width, int height, int tileWidth, int tileHeight)
        {
            if (editor != null)
            {
                Canvas2.Children.Clear();

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int index = map[j, i];

                        if (index != -1 && index < ListViewImages.Items.Count)
                        {
                            Image image = (Image) ListViewImages.Items[index];
                            Image imageToDraw = new Image();
                            imageToDraw.Source = image.Source;
                            imageToDraw.Width = tileWidth;
                            imageToDraw.Height = tileHeight;
                            imageToDraw.Margin = new Thickness(j * tileWidth, i * tileHeight, 0, 0);
                            Canvas2.Children.Add(imageToDraw);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the new state of the map when drawing.
        /// </summary>
        /// <param name="x">Horizontal position.</param>
        /// <param name="y">Vertical position.</param>
        /// <param name="tileWidth">Width of a tile.</param>
        /// <param name="tileHeight">Height of a tile.</param>
        /// <param name="index">Index of an image in the ListView.</param>
        internal void SetCanvasContentDrawOver(int x, int y, int tileWidth, int tileHeight, int index)
        {
            if (editor != null)
            {
                if (index != -1)
                {
                    Image image = (Image) ListViewImages.Items[index];
                    Image imageToDraw = new Image();
                    imageToDraw.Source = image.Source;
                    imageToDraw.Width = tileWidth;
                    imageToDraw.Height = tileHeight;
                    imageToDraw.Margin = new Thickness(x*tileWidth, y*tileHeight, 0, 0);
                    Canvas2.Children.Add(imageToDraw);
                }
                else
                {
                    Canvas2.Children.Add(new Rectangle()
                    {
                        Width = tileWidth,
                        Height = tileHeight,
                        Margin = new Thickness(x*tileWidth, y*tileHeight, 0, 0),
                        Stroke = Brushes.Gray,
                        Fill = Brushes.LightGray,
                        StrokeThickness = 1
                    });
                }
            }
        }

        /// <summary>
        /// Dialog to load one or multiple files.
        /// </summary>
        /// <param name="fileType">Type of the file (.png, .jpg, etc...).</param>
        /// <param name="multi">Sets if the dialog should allow multiple selection or not.</param>
        /// <returns>Returns an array containing the files to load.</returns>
        public string[] OpenFile(string fileType, bool multi = false)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = fileType;
            openFileDialog.Multiselect = multi;
            openFileDialog.Filter = string.Format($"{fileType}|*{fileType}");
            bool? result = openFileDialog.ShowDialog();

            return result == true ? openFileDialog.FileNames : new string[0];
        }

        /// <summary>
        /// Dialog to save a map.
        /// </summary>
        /// <returns>Returns true if the map has been saved.</returns>
        public bool Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "IMIE Editor|*.txt";

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                editor.Save(saveFileDialog.FileName);
                editor.Dirty = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads the images in the ListView ready to be used to paint the map.
        /// </summary>
        /// <param name="images">Images to add to the ListView.</param>
        public void SetImages(List<CroppedBitmap> images)
        {
            ListViewImages.Items.Clear();
            foreach (var image in images)
            {
                ListViewImages.Items.Add(new Image() { Source = image });
            }
            ListViewImages.Width = images[0].Width * 2 + 32;
        }

        /// <summary>
        /// Allows the zoom on the map.
        /// </summary>
        private void MoveZoom()
        {
            if (editor != null)
            {
                TransformGroup transformGroup = new TransformGroup();
                transformGroup.Children.Add(new ScaleTransform(zoom, zoom));
                transformGroup.Children.Add(new TranslateTransform(offsetX, offsetY));

                Canvas1.RenderTransform = transformGroup;
                Canvas2.RenderTransform = transformGroup;
            }
        }

        /// <summary>
        /// Exetutes different behaviours on the map depending on the mouse button being pressed.
        /// Left button draws the selected image of the ListView at the current position.
        /// Right button erases the image at the current position.
        /// Middle button pans on the map.
        /// </summary>
        /// <param name="sender">Object triggering the event.</param>
        /// <param name="e">Facultative arguments.</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (editor != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    editor.Draw(e.GetPosition(Canvas1), ListViewImages.SelectedIndex);
                }
                else if (e.RightButton == MouseButtonState.Pressed)
                {
                    editor.Draw(e.GetPosition(Canvas1), -1);
                }
                else if (e.MiddleButton == MouseButtonState.Pressed)
                {
                    var position = e.GetPosition(this);
                    var diff = position - prevPosition;
                    offsetX += diff.X;
                    offsetY += diff.Y;

                    MoveZoom();
                }

                prevPosition = e.GetPosition(this);
            }
        }

        /// <summary>
        /// Calls the appropriate action when pressing a mouse button.
        /// </summary>
        /// <param name="sender">Object triggering the event.</param>
        /// <param name="e">Facultative arguments.</param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (editor != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
                {
                    OnMouseMove(sender, e);
                }
                else if (e.MiddleButton == MouseButtonState.Pressed)
                {
                    prevPosition = e.GetPosition(this);
                }
            }
        }

        /// <summary>
        /// Triggers the map repainting when releasing mouse buttons.
        /// </summary>
        /// <param name="sender">Object triggering the event.</param>
        /// <param name="e">Facultative arguments.</param>
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (editor != null)
            {
                editor.Repaint();
            }
        }

        /// <summary>
        /// Zooms in and out on the map using the scroll wheel.
        /// </summary>
        /// <param name="sender">Object triggering the event.</param>
        /// <param name="e">Facultative arguments.</param>
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (editor != null)
            {
                if (e.Delta > 0)
                {
                    zoom *= 1.25;
                }
                else if (e.Delta < 0)
                {
                    zoom /= 1.25;
                }

                MoveZoom();
            }
        }

        #endregion
    }
}
