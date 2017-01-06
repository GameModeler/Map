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

        private Editor editor;
        private double offsetX;
        private double offsetY;
        private double zoom = 1;
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
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Replace, TilesetCommand));

            InitializeComponent();
        }

        #endregion

        #region Methods

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

        public void SaveCommand(object sender, RoutedEventArgs args)
        {
            if (editor != null)
            {
                Save();
            }
        }

        public void OpenCommand(object sender, RoutedEventArgs args)
        {
            string file = OpenFile(".txt");

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
                    editor = new Editor(this, new BitmapImage(new Uri(OpenFile(".png"))));
                }

                editor.LoadMap(file);
            }
        }

        public void ExitCommand(object sender, RoutedEventArgs args)
        {
            if (editor == null || !editor.Dirty || Save())
            {
                Close();
            }
        }

        public void TilesetCommand(object sender, RoutedEventArgs args)
        {
            if (editor != null)
            {
                var image = new BitmapImage(new Uri(OpenFile(".png")));
                editor.SetTileset(image);
            }
        }

        public void PropertiesCommand(object sender, RoutedEventArgs args)
        {
            if (editor != null)
            {
                MapPropertyWindow mapPropertyWindow = new MapPropertyWindow();
                mapPropertyWindow.ShowDialog();

                editor.SetProperties(int.Parse(mapPropertyWindow.WidthTextBox.Text), int.Parse(mapPropertyWindow.HeightTextBox.Text), int.Parse(mapPropertyWindow.TileWidthTextBox.Text), int.Parse(mapPropertyWindow.TileHeightTextBox.Text));
            }
        }

        private Editor NewMap()
        {
            try
            {
                MapPropertyWindow mapPropertyWindow = new MapPropertyWindow();
                mapPropertyWindow.ShowDialog();

                var image = new BitmapImage(new Uri(OpenFile(".png")));

                zoom = 1;
                offsetX = 0;
                offsetY = 0;

                return new Editor(this, int.Parse(mapPropertyWindow.WidthTextBox.Text), int.Parse(mapPropertyWindow.HeightTextBox.Text), int.Parse(mapPropertyWindow.TileWidthTextBox.Text), int.Parse(mapPropertyWindow.TileHeightTextBox.Text), image);
            }
            catch (Exception)
            {
                return null;
            }
        }

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

        public string OpenFile(string fileType)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = fileType;
            openFileDialog.Filter = string.Format($"{fileType}|*{fileType}");
            bool? result = openFileDialog.ShowDialog();

            return result == true ? openFileDialog.FileName : "";
        }

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

        public void SetImages(List<CroppedBitmap> images)
        {
            ListViewImages.Items.Clear();
            foreach (var image in images)
            {
                ListViewImages.Items.Add(new Image() { Source = image });
            }
            ListViewImages.Width = images[0].Width * 2 + 32;
        }

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

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (editor != null)
            {
                editor.Repaint();
            }
        }

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
