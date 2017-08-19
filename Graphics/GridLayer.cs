using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Map.Graphics
{
    public class GridLayer : Grid
    {
        #region Dependency properties

        public static readonly DependencyProperty RowsProperty = DependencyProperty.RegisterAttached("Rows",
            typeof(string), typeof(GridLayer),
            new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                RowsPropertyChanged));

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.RegisterAttached("Columns",
            typeof(string), typeof(GridLayer),
            new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                ColumnsPropertyChanged));

        public static readonly DependencyProperty ValidPositionStrokeThicknessProperty =
            DependencyProperty.RegisterAttached("ValidPositionStrokeThickness", typeof(int), typeof(GridLayer),
                new PropertyMetadata(0, ValidPositionStrokeThicknessPropertyChanged));

        public static readonly DependencyProperty ValidPositionStrokeColorProperty =
            DependencyProperty.RegisterAttached("ValidPositionStrokeColor", typeof(Brush), typeof(GridLayer),
                new PropertyMetadata(Brushes.Transparent, ValidPositionStrokeColorPropertyChanged));

        public static readonly DependencyProperty ValidPositionBackgroundProperty =
            DependencyProperty.RegisterAttached("ValidPositionBackground", typeof(Brush), typeof(GridLayer),
                new PropertyMetadata(Brushes.Transparent, ValidPositionBackgroundPropertyChanged));

        /// <summary>
        /// Rows dependency property
        /// </summary>
        public string Rows
        {
            get => (string) GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        /// <summary>
        /// Columns dependency property
        /// </summary>
        public string Columns
        {
            get => (string) GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        /// <summary>
        /// Valid placeholder's stroke thickness
        /// </summary>
        public int ValidPositionStrokeThickness
        {
            get => (int) GetValue(ValidPositionStrokeThicknessProperty);
            set => SetValue(ValidPositionStrokeThicknessProperty, value);
        }

        /// <summary>
        /// Valid placeholder's stroke color
        /// </summary>
        public Brush ValidPositionStrokeColor
        {
            get => (Brush) GetValue(ValidPositionStrokeColorProperty);
            set => SetValue(ValidPositionStrokeColorProperty, value);
        }

        /// <summary>
        /// Valid placeholder's background color
        /// </summary>
        public Brush ValidPositionBackground
        {
            get => (Brush) GetValue(ValidPositionBackgroundProperty);
            set => SetValue(ValidPositionBackgroundProperty, value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Grid placeholders
        /// </summary>
        internal ObservableCollection<Placeholder> Placeholders { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public GridLayer()
        {
            Placeholders = new ObservableCollection<Placeholder>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Callback method called when the rows dependency property changes
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <param name="e">Dependency changes arguments</param>
        private static void RowsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var grid = obj as GridLayer;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;

            if (grid == null || oldValue == null || newValue == null || oldValue == newValue)
            {
                return;
            }

            grid.RowDefinitions.Clear();
            newValue
                .Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(Parse)
                .Select(l => new RowDefinition
                {
                    Height = l
                })
                .ToList()
                .ForEach(grid.RowDefinitions.Add);

            CreatePlaceholders(grid);
        }

        /// <summary>
        /// Callback method called when the columns dependency property changes
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <param name="e">Dependency changes arguments</param>
        private static void ColumnsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var grid = obj as GridLayer;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;

            if (grid == null || oldValue == null || newValue == null || oldValue == newValue)
            {
                return;
            }

            grid.ColumnDefinitions.Clear();
            newValue
                .Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(Parse)
                .Select(l => new ColumnDefinition
                {
                    Width = l
                })
                .ToList()
                .ForEach(grid.ColumnDefinitions.Add);

            CreatePlaceholders(grid);
        }

        /// <summary>
        /// Create a placeholder in every cell of the grid
        /// </summary>
        /// <param name="grid">Grid to create the placeholders in</param>
        private static void CreatePlaceholders(GridLayer grid)
        {
            for (var i = 1; i <= grid.RowDefinitions.Count; i++)
            {
                for (var j = 1; j <= grid.ColumnDefinitions.Count; j++)
                {
                    var placeholder = new Placeholder(new Point(j, i));

                    SetRow(placeholder, i - 1);
                    SetColumn(placeholder, j - 1);
                    grid.Placeholders.Add(placeholder);
                    grid.Children.Add(placeholder);
                }
            }
        }

        /// <summary>
        /// Callback method called when the valid position stroke thickness dependency property changes
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <param name="e">Dependency changes arguments</param>
        private static void ValidPositionStrokeThicknessPropertyChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            var grid = obj as GridLayer;
            var oldValue = (int) e.OldValue;
            var newValue = (int) e.NewValue;

            if (grid == null || oldValue == newValue)
            {
                return;
            }

            grid.Placeholders
                .Where(p => p.IsActive)
                .ToList()
                .ForEach(p => p.BorderThickness = new Thickness(newValue));
        }

        /// <summary>
        /// Callback method called when the valid position stroke color dependency property changes
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <param name="e">Dependency changes arguments</param>
        private static void ValidPositionStrokeColorPropertyChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            var grid = obj as GridLayer;
            var oldValue = e.OldValue as Brush;
            var newValue = e.NewValue as Brush;

            if (grid == null || oldValue == null || newValue == null || Equals(oldValue, newValue))
            {
                return;
            }

            grid.Placeholders
                .Where(p => p.IsActive)
                .ToList()
                .ForEach(p => p.BorderBrush = newValue);
        }

        /// <summary>
        /// Callback method called when the valid position background color dependency property changes
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <param name="e">Dependency changes arguments</param>
        private static void ValidPositionBackgroundPropertyChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            var grid = obj as GridLayer;
            var oldValue = e.OldValue as Brush;
            var newValue = e.NewValue as Brush;

            if (grid == null || oldValue == null || newValue == null || Equals(oldValue, newValue))
            {
                return;
            }

            grid.Placeholders
                .Where(p => p.IsActive)
                .ToList()
                .ForEach(p => p.Background = newValue);
        }

        /// <summary>
        /// Parse a value
        /// </summary>
        /// <param name="value">Value to be parsed</param>
        /// <returns>The parsed value</returns>
        private static IEnumerable<GridLength> Parse(string value)
        {
            if (!value.Contains("#"))
            {
                return new[]
                {
                    ParseGridLength(value)
                };
            }

            var parts = value.Split(new[] {'#'}, StringSplitOptions.RemoveEmptyEntries);
            var count = int.Parse(parts[1].Trim());

            return Enumerable.Repeat(ParseGridLength(parts[0]), count);
        }

        /// <summary>
        /// Parse a grid length value
        /// </summary>
        /// <param name="value">Grid length value to be parsed</param>
        /// <returns>The parsed value</returns>
        private static GridLength ParseGridLength(string value)
        {
            value = value.Trim();

            if (value.ToLower() == "auto")
            {
                return GridLength.Auto;
            }

            if (value.Contains("*"))
            {
                var startCount = value.ToCharArray().Count(c => c == '*');
                var pureNumber = value.Replace("*", "");
                var ratio = string.IsNullOrWhiteSpace(pureNumber) ? 1 : double.Parse(pureNumber);

                return new GridLength(startCount * ratio, GridUnitType.Star);
            }

            var pixelsCount = double.Parse(value);

            return new GridLength(pixelsCount, GridUnitType.Pixel);
        }

        #endregion
    }
}
