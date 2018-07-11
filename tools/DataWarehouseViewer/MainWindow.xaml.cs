using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using DataWarehouseViewer.Mvvm;

namespace DataWarehouseViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(JurisdictionViewModel viewModel)
        {
            InitializeComponent();
            
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
            }
        }

        private void OnOKButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void FillDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["Standards_DW"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT id, title, type FROM v_jurisdictions";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Jurisdictions");
                sda.Fill(dt);

                ObservableCollection<JurisdictionViewModel> collection = new ObservableCollection<JurisdictionViewModel>();

                foreach(DataRowView view in  dt.DefaultView)
                {
                    DataRow row = view.Row;

                    Guid j_id = row.Field<Guid>(dt.Columns[0]);
                    string j_name = row.Field<string>(dt.Columns[1]);
                    string j_type = row.Field<string>(dt.Columns[2]);
                    string s_id = row.Field<string>(dt.Columns[3]);
                    string s_subject = row.Field<string>(dt.Columns[4]);
                    string s_name = row.Field<string>(dt.Columns[5]);
                    string s_educationLevels = row.Field<string>(dt.Columns[6]);
                    Guid s_doc_id = row.Field<Guid>(dt.Columns[7]);
                    string s_doc_asnIdentifier = row.Field<string>(dt.Columns[8]);
                    string s_doc_pub_status = row.Field<string>(dt.Columns[9]);
                    string s_doc_name = row.Field<string>(dt.Columns[10]);
                    int s_doc_valid = row.Field<int>(dt.Columns[11]);
                    string s_doc_source = row.Field<string>(dt.Columns[12]);

                    //Jurisdiction model = new Jurisdiction { Id = id, Title = title, Type = (JurisdictionType)Enum.Parse(typeof(JurisdictionType), type) };
                    //collection.Add(
                    //    new JurisdictionViewModel(model));
                }

                //DatabaseGrid.ItemsSource = CollectionViewSource.GetDefaultView(collection);
            }
        }

        private void OnExecuteCommandOpen(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void OnExecutedCommandClose(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnExecutedCommandSaveAs(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void OnExecutedCommandNew(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void OnExecutedCommandCopy(object sender, ExecutedRoutedEventArgs e)
        {
            //StringBuilder sb = new StringBuilder();
            //foreach(var seletedCell in DatabaseGrid.SelectedCells)
            //{
            //    sb.Append(seletedCell.Item.ToString());
            //    sb.Append("\t");
            //}

            //Clipboard.SetText(sb.ToString());
        }
    }
}
