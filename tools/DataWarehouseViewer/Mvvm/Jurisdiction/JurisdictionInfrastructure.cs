namespace DataWarehouseViewer.Mvvm
{
    public class JurisdictionInfrastructure
    {
        public Jurisdiction Model { get; }
        public JurisdictionViewModel ViewModel { get; }
        public JurisdictionView View { get; }

        public JurisdictionInfrastructure(Jurisdiction model, JurisdictionViewModel viewModel, JurisdictionView view)
        {
            Model = model;
            ViewModel = viewModel;
            View = view;
        }
    }
}
