using System.Windows;

namespace Map
{
    /// <summary>
    /// Interaction logic for MapPropertyWindow.xaml
    /// </summary>
    public partial class MapPropertyWindow : Window
    {
        public MapPropertyWindow()
        {
            InitializeComponent();
        }

        private void MapValidationButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
