using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.Jurisdictions
{
    public class Document
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("asnIdentifier")]
        public string AsnIdentifier { get; set; }

        [JsonProperty("publicationStatus")]
        public string PublicationStatus { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("valid")]
        public int Valid { get; set; }

        [JsonProperty("sourceURL")]
        public string SourceURL { get; set; }


        public override string ToString()
        {
            return $"{Title} {SourceURL}";
        }
    }
}
