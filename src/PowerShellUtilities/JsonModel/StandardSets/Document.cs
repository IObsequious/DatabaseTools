using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.StandardSets
{

    public class Document
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("valid")]
        public string Valid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("sourceURL")]
        public string SourceURL { get; set; }

        [JsonProperty("asnIdentifier")]
        public string AsnIdentifier { get; set; }

        [JsonProperty("publicationStatus")]
        public string PublicationStatus { get; set; }
    }
}
