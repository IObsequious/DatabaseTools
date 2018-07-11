using System.Windows;

namespace DataWarehouseViewer.Mvvm
{
    public partial class JurisdictionView : Window
    {
        public JurisdictionView()
        {
            InitializeComponent();
        }

        internal JurisdictionView(JurisdictionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
