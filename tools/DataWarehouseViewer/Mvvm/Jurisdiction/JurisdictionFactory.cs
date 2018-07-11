namespace DataWarehouseViewer.Mvvm
{
    public static class JurisdictionFactory
    {
        public static JurisdictionInfrastructure Create()
        {
            Jurisdiction model = new Jurisdiction();
            JurisdictionViewModel viewModel = new JurisdictionViewModel(model);
            JurisdictionView view = new JurisdictionView(viewModel);
            return new JurisdictionInfrastructure(model, viewModel, view);
        }
    }
}
