using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Map.WPF.Commands;
using Map.WPF.Graphics.UserControls;
using Map.WPF.Interfaces;
using Point = System.Drawing.Point;

namespace Map.WPF.Graphics.Layers
{
    public class GridLayer : Grid
    {
        #region Dependency properties

        public static readonly DependencyProperty RowsProperty = DependencyProperty.RegisterAttached("Rows",
            typeof(string), typeof(GridLayer),
            new FrameworkPropertyMetadata("",
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                RowsPropertyChanged));

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.RegisterAttached("Columns",
            typeof(string), typeof(GridLayer),
            new FrameworkPropertyMetadata("",
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                ColumnsPropertyChanged));

        public string Rows
        {
            get => (string) GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        public string Columns
        {
            get => (string) GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        #endregion

        #region Properties

        public ObservableCollection<Cell> CellPlaceholders { get; set; }

        public Cell SelectedCell { get; set; }

        public GmUserControl SelectedControl { get; set; }

        #endregion

        #region Commands

        public ICommand DetectCellCommand { get; set; }

        #endregion

        #region Constructors

        public GridLayer()
        {
            CellPlaceholders = new ObservableCollection<Cell>();

            DetectCellCommand = new RelayCommand(DetectCell, null);
        }

        #endregion

        #region Methods

        private static void RowsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var gridLayer = obj as GridLayer;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;

            if (gridLayer == null || oldValue == null || newValue == null || oldValue == newValue)
            {
                return;
            }

            gridLayer.ColumnDefinitions.Clear();
            newValue
                .Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(Parse)
                .Select(l => new RowDefinition
                {
                    Height = l
                })
                .ToList()
                .ForEach(gridLayer.RowDefinitions.Add);

            DrawRowsColumns(obj);
        }

        private static void ColumnsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var gridLayer = obj as GridLayer;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;

            if (gridLayer == null || oldValue == null || newValue == null || oldValue == newValue)
            {
                return;
            }

            gridLayer.ColumnDefinitions.Clear();
            newValue
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(Parse)
                .Select(l => new ColumnDefinition
                {
                    Width = l
                })
                .ToList()
                .ForEach(gridLayer.ColumnDefinitions.Add);

            DrawRowsColumns(obj);
        }

        private static IEnumerable<GridLength> Parse(string text)
        {
            if (!text.Contains("#"))
            {
                return new[] {ParseGridLength(text)};
            }

            var parts = text.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            var count = int.Parse(parts[1].Trim());

            return Enumerable.Repeat(ParseGridLength(parts[0]), count);
        }

        private static GridLength ParseGridLength(string text)
        {
            text = text.Trim();

            if (text.ToLower() == "auto")
            {
                return GridLength.Auto;
            }

            if (text.Contains("*"))
            {
                var startCount = text.ToCharArray().Count(c => c == '*');
                var pureNumber = text.Replace("*", "");
                var ratio = string.IsNullOrWhiteSpace(pureNumber) ? 1 : double.Parse(pureNumber);

                return new GridLength(startCount * ratio, GridUnitType.Star);
            }

            var pixelsCount = double.Parse(text);
            return new GridLength(pixelsCount, GridUnitType.Pixel);
        }

        private static void DrawRowsColumns(DependencyObject obj)
        {
            var gridLayer = obj as GridLayer;

            if (gridLayer == null)
            {
                return;
            }

            for (var i = 0; i < gridLayer.RowDefinitions.Count; i++)
            {
                for (var j = 0; j < gridLayer.ColumnDefinitions.Count; j++)
                {
                    var placeholder = new Cell
                    {
                        Position = new Point(j, i),
                        Opacity = 0
                    };

                    Cell.SetMouseEnterCommand(placeholder, gridLayer.DetectCellCommand);

                    SetRow(placeholder, i);
                    SetColumn(placeholder, j);
                    gridLayer.Children.Add(placeholder);

                    gridLayer.CellPlaceholders.Add(placeholder);
                }
            }
        }

        private void DetectCell(object parameter)
        {
            var placeholder = parameter as Cell;

            if (placeholder == null)
            {
                return;
            }

            SelectedCell = placeholder;

            //MessageBox.Show(string.Format($"Click on Grid [{placeholder.Position.X}, {placeholder.Position.Y}]"));
        }

        public void SetRowColumn(object obj, Point point)
        {
            var element = obj as GmUserControl;
            var model = element?.DataContext as IMovable;

            if (model == null)
            {
                return;
            }

            SelectedControl = element;

            var piecePositionBinding = new Binding()
            {
                Path = new PropertyPath("Position"),
                Source = model
            };

            BindingOperations.SetBinding(SelectedControl, GmUserControl.PositionProperty, piecePositionBinding);

            model.Position = new Point(point.Y, point.X);
        }

        public Point? MoveObject(object obj)
        {
            var element = obj as GmUserControl;
            var model = element?.DataContext as IMovable;

            if (model == null || !model.IsActive || !model.CanMove(SelectedCell.Position))
            {
                return null;
            }

            element.Position = new Point(SelectedCell.Position.Y, SelectedCell.Position.X);

            return model.Position;
        }

        #endregion
    }
}
