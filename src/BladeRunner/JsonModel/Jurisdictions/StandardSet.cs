using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.Jurisdictions
{
    public class StandardSet
    {
        public StandardSet()
        {
            EducationLevels = new List<string>();
            Document = new Document();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("educationLevels")]
        public List<string> EducationLevels { get; set; }

        [JsonProperty("document")]
        public Document Document { get; set; }


        internal string GetEducationLevels()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");

            for (var i = 0; i < EducationLevels.Count; i++)
            {
                sb.Append('"');
                sb.Append(EducationLevels[i]);
                sb.Append('"');
                if (i != EducationLevels.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(" ]");
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"{Title} {Subject} {EducationLevels}";
        }
    }
}
