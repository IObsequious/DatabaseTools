using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.StandardSets
{

    public class Standard
    {
        public Standard()
        {
            AncestorIds = new List<string>();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("asnIdentifier")]
        public string AsnIdentifier { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("depth")]
        public int Depth { get; set; }

        [JsonProperty("statementNotation")]
        public string StatementNotation { get; set; }

        [JsonProperty("statementLabel")]
        public string StatementLabel { get; set; }

        [JsonProperty("listId")]
        public string ListId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ancestorIds")]
        public IList<string> AncestorIds { get; set; }

        public string GetAncestorIds()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");

            for (var i = 0; i < AncestorIds.Count; i++)
            {
                sb.Append('"');
                sb.Append(AncestorIds[i]);
                sb.Append('"');
                if (i != AncestorIds.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(" ]");
            return sb.ToString();
        }
    }
}
