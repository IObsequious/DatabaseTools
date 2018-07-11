using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseViewer.Mvvm;
using System.Windows.Controls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Documents;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using System.Xml.Serialization;
using System.Windows.Media;

namespace DataWarehouseViewer
{
    public static class MvvmUtilities
    {
        public static IEnumerable<JurisdictionType> GetJurisdictionTypes => Enum.GetValues(typeof(JurisdictionType)).Cast<JurisdictionType>();

        public static IEnumerable<PublicationStatus> GetPublicationStatuses => Enum.GetValues(typeof(PublicationStatus)).Cast<PublicationStatus>();

        public static IEnumerable<TabItem> TabItems
        {
            get
            {
                List<TabItem> list = new List<TabItem>();

                string ConString = ConfigurationManager.ConnectionStrings["Jurisdictions_DB"].ConnectionString;
                string CmdString = string.Empty;
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    string query = "USE Jurisdictions_DB;\r\n" +
                                   "SELECT * FROM sys.tables;";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                    DataTable dt = new DataTable("tables");

                    adapter.Fill(dt);

                    foreach (DataRowView view in dt.DefaultView)
                    {
                        DataRow row = view.Row;

                        string name = row.Field<string>(dt.Columns[0]);
                        string displayName = name.Substring(3);

                        TabItem item = new TabItem();
                        item.Header = displayName;
                        item.Background = new SolidColorBrush() { Color = Colors.Tan };

                        DockPanel panel = new DockPanel();

                        panel.Background = item.Background;

                        panel.Children.Add(CreateDataGrid(name));

                        item.Content = panel;
                        list.Add(item);
                    }

                    return list;
                }
            }
        }

        private static DataGrid CreateDataGrid(string tableName)
        {
            DataGrid dataGrid = new DataGrid
            {
                CanUserAddRows = true,
                CanUserDeleteRows = true,
                CanUserReorderColumns = false,
                CanUserResizeColumns = true,
                CanUserResizeRows = true,
                CanUserSortColumns = true,
                Background = new SolidColorBrush { Color = Colors.Gray},
                AlternatingRowBackground = new SolidColorBrush() { Color = Colors.LightBlue },
                AutoGenerateColumns = false,
                SelectionMode = DataGridSelectionMode.Extended,
                SelectionUnit = DataGridSelectionUnit.FullRow,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
                ContextMenu = new ContextMenu
                {
                    ItemsSource = new ObservableCollection<MenuItem>
                    {
                        new MenuItem{ Header = "Copy", Command = ApplicationCommands.Copy}
                    }
                }
            };

            switch (tableName)
            {
                case "tbl_jurisdictions":
                    {
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "id", Binding = new Binding("jurisdictions_id") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "title", Binding = new Binding("jurisdictions_title") });
                        dataGrid.Columns.Add(new DataGridComboBoxColumn { Width = 250, Header = "Type", ItemsSource = GetJurisdictionTypes, SelectedItemBinding = new Binding("jurisdictions_type") { Converter = new StringEnumConverter<JurisdictionType>() } });
                        dataGrid.ItemsSource = GetDataTable("tbl_jurisdictions", "jurisdictions_id,jurisdictions_title,jurisdictions_type").DefaultView;
                        break;
                    }
                case "v_jurisdictions":
                    {
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "id", Binding = new Binding("j_id") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "name", Binding = new Binding("j_name") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "type", Binding = new Binding("j_type") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "s_id", Binding = new Binding("s_id") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "subject", Binding = new Binding("s_subject") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "s_name", Binding = new Binding("s_name") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "s_educationLevels", Binding = new Binding("s_educationLevels") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "s_doc_id", Binding = new Binding("s_doc_id") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "s_doc_asnIdentifier", Binding = new Binding("s_doc_asnIdentifier") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "s_doc_pub_status", Binding = new Binding("s_doc_pub_status") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "s_doc_name", Binding = new Binding("s_doc_name") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Width = 250, Header = "s_doc_valid", Binding = new Binding("s_doc_valid") });
                        dataGrid.Columns.Add(new DataGridHyperlinkColumn { Width = 250, Header = "s_doc_source", Binding = new Binding("s_doc_source") });
                        dataGrid.ItemsSource = GetDataTable("v_jurisdictions", "j_id, j_name, j_type, s_id, s_subject, s_name, s_educationLevels, s_doc_id, s_doc_asnIdentifier, s_doc_pub_status, s_doc_name, s_doc_valid, s_doc_source").DefaultView;
                        break;
                    }
                case "tbl_standard_sets":
                    {
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_id", Width = 250, Binding = new Binding("standard_sets_id") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_subject", Width = 250, Binding = new Binding("standard_sets_subject") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_title", Width = 250, Binding = new Binding("standard_sets_title") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_educationLevels", Width = 250, Binding = new Binding("standard_sets_educationLevels") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_document_id", Width = 250, Binding = new Binding("standard_sets_document_id") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_document_asnIdentifier", Width = 250, Binding = new Binding("standard_sets_document_asnIdentifier") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_document_publicationStatus", Width = 250, Binding = new Binding("standard_sets_document_publicationStatus") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_document_title", Width = 250, Binding = new Binding("standard_sets_document_title") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_document_valid", Width = 250, Binding = new Binding("standard_sets_document_valid") });
                        dataGrid.Columns.Add(new DataGridTextColumn { Header = "standard_sets_document_sourceURL", Width = 250, Binding = new Binding("standard_sets_document_sourceURL") });
                        dataGrid.ItemsSource = GetDataTable("tbl_standard_sets", "standard_sets_key, jurisdictions_key, standard_sets_id, standard_sets_subject, standard_sets_title, standard_sets_educationLevels, standard_sets_document_id, standard_sets_document_asnIdentifier, standard_sets_document_publicationStatus, standard_sets_document_title, standard_sets_document_valid, standard_sets_document_sourceURL").DefaultView;
                        break;
                    }

                default:
                    break;
            }

            return dataGrid;
        }

        public class StringEnumConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return System.Enum.Parse(typeof(JurisdictionType),  CodeIdentifier.MakePascal(value.ToString()));
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class StringEnumConverter<TEnum> : IValueConverter
            where TEnum: struct
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return System.Enum.Parse(typeof(TEnum), CodeIdentifier.MakePascal(value.ToString()));
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        private static DataTable GetDataTable(string tableName, string columnNames)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Jurisdictions_DB"].ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT {columnNames} FROM {tableName};", connection);

                DataTable table = new DataTable(tableName);

                adapter.Fill(table);

                return table;
            }
        }
    }
}
