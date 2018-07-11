using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BladeRunner.JsonModel.Jurisdictions;

namespace BladeRunner.Sql
{
    internal static class JurisdictionDBImporter
    {
        public const string ConnectionString = "Data Source=BACKTRACK;Initial Catalog=Jurisdiction_DB;Integrated Security=True";

        public static void Import(JurisdictionCollection collection)
        {
            int total = collection.Count;

            for (var i = 0; i < total; i++)
            {
                var jurisdiction = collection[i];

                int percent = (int)Math.Round(i * ((decimal)100 / total));

                ConsoleEx.WriteProgress("Importing Jurisdictions", $"Jurisdiction {i} of {total}", jurisdiction.Data.Title, percent);

                Guid jurisdictionsKey = Guid.NewGuid();

                ImportJurisdictionData(jurisdiction.Data, jurisdictionsKey);

                ImportStandardSets(jurisdiction.Data.StandardSets, jurisdictionsKey);
            }
        }

        private static void ImportJurisdictionData(JurisdictionData jurisdiction, Guid key)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("sp_insert_tbl_jurisdictions", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(new SqlParameter("@jurisdictions_key", key));
                command.Parameters.Add(new SqlParameter("@jurisdictions_id", jurisdiction.Id));
                command.Parameters.Add(new SqlParameter("@jurisdictions_title", jurisdiction.Title));
                command.Parameters.Add(new SqlParameter("@jurisdictions_type", jurisdiction.Type));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
            }
        }

        private static void ImportStandardSets(StandardSets standardSets, Guid jurisdictionsKey)
        {
            foreach (var standardSet in standardSets)
            {
                ImportStndardSet(jurisdictionsKey, standardSet);
            }
        }

        private static void ImportStndardSet(Guid jurisdictionsKey, StandardSet standardSet)
        {
            Guid standardSetsKey = Guid.NewGuid();

            ImportStandardSetCore(jurisdictionsKey, standardSetsKey, standardSet);

            ImportDocument(standardSet.Document, standardSetsKey);
        }

        private static void ImportStandardSetCore(Guid jurisdictionsKey, Guid standardSetsKey, StandardSet standardSet)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("sp_insert_tbl_standard_sets", connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(new SqlParameter("@standard_sets_key", standardSetsKey));
                command.Parameters.Add(new SqlParameter("@jurisdictions_key", jurisdictionsKey));
                command.Parameters.Add(new SqlParameter("@standard_sets_id", standardSet.Id));
                command.Parameters.Add(new SqlParameter("@standard_sets_subject", standardSet.Subject));
                command.Parameters.Add(new SqlParameter("@standard_sets_title", standardSet.Title));
                command.Parameters.Add(new SqlParameter("@standard_sets_educationLevels", standardSet.GetEducationLevels()));

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
                command.Parameters.Add(new SqlParameter("@documents_publicationStatus", document.PublicationStatus));
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
    }
}
