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

        /// <summary>
        /// Closes the properties window when validating.
        /// </summary>
        /// <param name="sender">Object triggering the event.</param>
        /// <param name="e">Facultative arguments.</param>
        private void MapValidationButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
