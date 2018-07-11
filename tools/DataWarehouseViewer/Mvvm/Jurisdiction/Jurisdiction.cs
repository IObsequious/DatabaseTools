using System;

namespace DataWarehouseViewer.Mvvm
{
    public class Jurisdiction
    {
        public Guid j_id { get; set; }
        public string j_name { get; set; }
        public string j_type { get; set; }
        public string s_id { get; set; }
        public string s_subject { get; set; }
        public string s_name { get; set; }
        public string s_educationLevels { get; set; }
        public Guid s_doc_id { get; set; }
        public string s_doc_asnIdentifier { get; set; }
        public string s_doc_pub_status { get; set; }
        public string s_doc_name { get; set; }
        public int s_doc_valid { get; set; }
        public string s_doc_source { get; set; }
    }
}
