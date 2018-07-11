using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BladeRunner.JsonModel.StandardSets;
namespace BladeRunner.Sql
{
    internal static class StandardDBImporter
    {

        private static readonly string ConnectionString = "Data Source=BACKTRACK;Initial Catalog=Standard_DB;Integrated Security=True";

        public static void Import(StandardSet standardSet)
        {
            Guid standardSetsKey = Guid.NewGuid();

            // Console.WriteLine("Importing {0} standards from the {1} standard set", standardSet.Data.Standards.Count, standardSet.Data.Title);

            if (standardSet.Data != null)
                ImportStandardSetData(standardSet.Data, standardSetsKey);

            if (standardSet.Data.Document != null)
                ImportDocument(standardSet.Data.Document, standardSetsKey);

            if (standardSet.Data.Jurisdiction != null)
                ImportJurisdiction(standardSet.Data.Jurisdiction, standardSetsKey);

            if (standardSet.Data.License != null)
                ImportLicense(standardSet.Data.License, standardSetsKey);
            
            if (standardSet.Data.Standards != null)
                ImportStandards(standardSet.Data.Standards, standardSetsKey);

        }

        private static void ImportStandards(Standards standards, Guid standardSetsKey)
        {

            foreach (var standard in standards)
            {
                ImportStandard(standardSetsKey, standard);
            }
        }

        private static void ImportStandard(Guid standardSetsKey, BladeRunner.JsonModel.StandardSets.Standard standard)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("sp_insert_tbl_standards", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(new SqlParameter("@standards_key", Guid.NewGuid()));
                command.Parameters.Add(new SqlParameter("@standard_sets_key", standardSetsKey));
                command.Parameters.Add(new SqlParameter("@standards_id", standard.Id));
                command.Parameters.Add(new SqlParameter("@standards_asnIdentifier", standard.AsnIdentifier));
                command.Parameters.Add(new SqlParameter("@standards_position", standard.Position));
                command.Parameters.Add(new SqlParameter("@standards_depth", standard.Depth));
                command.Parameters.Add(new SqlParameter("@standards_statementNotation", standard.StatementNotation));
                command.Parameters.Add(new SqlParameter("@standards_statementLabel", standard.StatementLabel));
                command.Parameters.Add(new SqlParameter("@standards_listId", standard.ListId));
                command.Parameters.Add(new SqlParameter("@standards_description", standard.Description));
                command.Parameters.Add(new SqlParameter("@standards_ancestorIds", standard.GetAncestorIds()));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
            }
        }

        private static void ImportLicense(License license, Guid standardSetsKey)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("sp_insert_tbl_licenses", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(new SqlParameter("@licenses_key", Guid.NewGuid()));
                command.Parameters.Add(new SqlParameter("@standard_sets_key", standardSetsKey));
                command.Parameters.Add(new SqlParameter("@licenses_title", license.Title));
                command.Parameters.Add(new SqlParameter("@licenses_url", license.URL));
                command.Parameters.Add(new SqlParameter("@licenses_rightsHolder", license.RightsHolder));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
            }
        }

        private static void ImportJurisdiction(Jurisdiction jurisdiction, Guid standardSetsKey)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("sp_insert_tbl_jurisdictions", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(new SqlParameter("@jurisdictions_key", Guid.NewGuid()));
                command.Parameters.Add(new SqlParameter("@standard_sets_key", standardSetsKey));
                command.Parameters.Add(new SqlParameter("@jurisdictions_id", jurisdiction.Id));
                command.Parameters.Add(new SqlParameter("@jurisdictions_title", jurisdiction.Title));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
            }
        }

        private static void ImportDocument(Document document, Guid standardSetsKey)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("sp_insert_tbl_documents", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(new SqlParameter("@documents_key", Guid.NewGuid()));
                command.Parameters.Add(new SqlParameter("@standard_sets_key", standardSetsKey));
                command.Parameters.Add(new SqlParameter("@documents_id", document.Id));
                command.Parameters.Add(new SqlParameter("@documents_asnIdentifier", document.AsnIdentifier));
                command.Parameters.Add(new SqlParameter("@documents_published", document.PublicationStatus == null ? "1" : "0"));
                command.Parameters.Add(new SqlParameter("@documents_title", document.Title));
                command.Parameters.Add(new SqlParameter("@documents_valid", document.Valid));
                command.Parameters.Add(new SqlParameter("@documents_sourceURL", document.Id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
            }
        }

        private static void ImportStandardSetData(StandardSetData standardSetData, Guid standardSetsKey)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("sp_insert_tbl_standard_sets", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(new SqlParameter("@standard_sets_key", standardSetsKey));
                command.Parameters.Add(new SqlParameter("@standard_sets_id", standardSetData.Id));
                command.Parameters.Add(new SqlParameter("@standard_sets_subject", standardSetData.Subject));
                command.Parameters.Add(new SqlParameter("@standard_sets_title", standardSetData.Title));
                command.Parameters.Add(new SqlParameter("@standard_sets_educationLevels", standardSetData.GetEducationLevels()));
                command.Parameters.Add(new SqlParameter("@standard_sets_rightsHolder", standardSetData.RightsHolder));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
            }
        }
    }
}
