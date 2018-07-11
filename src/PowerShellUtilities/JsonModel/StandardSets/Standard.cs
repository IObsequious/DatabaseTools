using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.StandardSets
{

    public class Standard
    {
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
        public List<string> AncestorIds { get; set; }
    }
}
