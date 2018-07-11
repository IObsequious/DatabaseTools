using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataWarehouseViewer.Mvvm
{
    public class JurisdictionViewModel : INotifyPropertyChanged
    {
        private readonly Jurisdiction _model;

        public JurisdictionViewModel(Jurisdiction model)
        {
            _model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public Guid j_id { get { return _model.j_id; } set { if (_model.j_id != value) { _model.j_id = value; OnPropertyChanged(); } } }
        public string j_name { get { return _model.j_name; } set { if (_model.j_name != value) { _model.j_name = value; OnPropertyChanged(); } } }
        public string j_type { get { return _model.j_type; } set { if (_model.j_type != value) { _model.j_type = value; OnPropertyChanged(); } } }
        public string s_id { get { return _model.s_id; } set { if (_model.s_id != value) { _model.s_id = value; OnPropertyChanged(); } } }
        public string s_subject { get { return _model.s_subject; } set { if (_model.s_subject != value) { _model.s_subject = value; OnPropertyChanged(); } } }
        public string s_name { get { return _model.s_name; } set { if (_model.s_name != value) { _model.s_name = value; OnPropertyChanged(); } } }
        public string s_educationLevels { get { return _model.s_educationLevels; } set { if (_model.s_educationLevels != value) { _model.s_educationLevels = value; OnPropertyChanged(); } } }
        public Guid s_doc_id { get { return _model.s_doc_id; } set { if (_model.s_doc_id != value) { _model.s_doc_id = value; OnPropertyChanged(); } } }
        public string s_doc_asnIdentifier { get { return _model.s_doc_asnIdentifier; } set { if (_model.s_doc_asnIdentifier != value) { _model.s_doc_asnIdentifier = value; OnPropertyChanged(); } } }
        public string s_doc_pub_status { get { return _model.s_doc_pub_status; } set { if (_model.s_doc_pub_status != value) { _model.s_doc_pub_status = value; OnPropertyChanged(); } } }
        public string s_doc_name { get { return _model.s_doc_name; } set { if (_model.s_doc_name != value) { _model.s_doc_name = value; OnPropertyChanged(); } } }
        public int s_doc_valid { get { return _model.s_doc_valid; } set { if (_model.s_doc_valid != value) { _model.s_doc_valid = value; OnPropertyChanged(); } } }
        public string s_doc_source { get { return _model.s_doc_source; } set { if (_model.s_doc_source != value) { _model.s_doc_source = value; OnPropertyChanged(); } } }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
