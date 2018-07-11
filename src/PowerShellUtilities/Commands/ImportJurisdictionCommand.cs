using System;
using System.Data;
using System.Data.SqlClient;
using System.Management.Automation;
using PowerShellUtilities.JsonModel.Jurisdictions;
using PowerShellUtilities.Utilities;

namespace PowerShellUtilities.Commands
{
    [Cmdlet(VerbsData.Import, "Jurisdiction")]
    public class ImportJurisdictionCommand : PSCmdlet
    {
        public const string Jurisdictions_DB_Connection = "Data Source=BACKTRACK;Initial Catalog=Jurisdictions_DB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";
        public const string Standards_DB_Connection = "Data Source=BACKTRACK;Initial Catalog=Standards_DB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";


        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public Jurisdiction[] Jurisdiction { get; set; }


        protected override void ProcessRecord()
        {
            using (SqlConnection connection = new SqlConnection(Jurisdictions_DB_Connection))
            {
                connection.Open();
                var count = Jurisdiction.Length;
                for (var i = 0; i < count; i++)
                {
                    WriteProgress(ProgressRecordFactory.Progress(
                         1, "Importing jurisdiction", $"Importing {i} of {count} jurisditions", $"Importing {Jurisdiction[i].Data.Title}"));
                    ImportJurisdiction(Jurisdiction[i]);
                }
            }
        }

        private void ImportJurisdiction(Jurisdiction jurisdiction)
        {
            Guid tbl_juris_key = Guid.NewGuid();


            WriteProgress(
                ProgressRecordFactory.Progress(2, "Inserting into table tbl_jurisdictions"));

            using (SqlConnection connection = new SqlConnection(Jurisdictions_DB_Connection))
            using (SqlCommand command = new SqlCommand("sp_insert_tbl_jurisdictions", connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add(new SqlParameter("@jurisdictions_key", tbl_juris_key));
                command.Parameters.Add(new SqlParameter("@jurisdictions_id", Guid.Parse(jurisdiction.Data.Id)));
                command.Parameters.Add(new SqlParameter("@jurisdictions_title", jurisdiction.Data.Title));
                command.Parameters.Add(new SqlParameter("@jurisdictions_type", jurisdiction.Data.Type));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                };
                reader.Close();
            }

            WriteProgress(
                ProgressRecordFactory.Progress(3, "Inserting into table tbl_standard_sets"));

            foreach (var standardSet in jurisdiction.Data.StandardSets)
            {

                using (SqlConnection connection = new SqlConnection(Jurisdictions_DB_Connection))
                using (SqlCommand command = new SqlCommand("sp_insert_tbl_standard_sets", connection))
                {
                    connection.Open();

                    Guid tbl_ss_key = Guid.NewGuid();

                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                    retVal.Direction = ParameterDirection.ReturnValue;

                    command.Parameters.Add(new SqlParameter("@standard_sets_key", tbl_ss_key));
                    command.Parameters.Add(new SqlParameter("@jurisdictions_key", tbl_juris_key));
                    command.Parameters.Add(new SqlParameter("@standard_sets_id", standardSet.Id));
                    command.Parameters.Add(new SqlParameter("@standard_sets_subject", standardSet.Subject));
                    command.Parameters.Add(new SqlParameter("@standard_sets_title", standardSet.Title));
                    command.Parameters.Add(new SqlParameter("@standard_sets_educationLevels", standardSet.GetEducationLevels()));

                    command.Parameters.Add(new SqlParameter("@standard_sets_document_id", standardSet.Document.Id));
                    command.Parameters.Add(new SqlParameter("@standard_sets_document_asnIdentifier", standardSet.Document.AsnIdentifier));
                    command.Parameters.Add(new SqlParameter("@standard_sets_document_publicationStatus", standardSet.Document.PublicationStatus));
                    command.Parameters.Add(new SqlParameter("@standard_sets_document_title", standardSet.Document.Title));
                    command.Parameters.Add(new SqlParameter("@standard_sets_document_valid", standardSet.Document.Valid));
                    command.Parameters.Add(new SqlParameter("@standard_sets_document_sourceURL", standardSet.Document.Id));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                    };
                    reader.Close();
                }
            }
        }
    }
}
