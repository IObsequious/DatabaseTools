using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.StandardSets
{

    public class StandardSetData
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("educationLevels")]
        public IList<string> EducationLevels { get; set; }

        [JsonProperty("license")]
        public License License { get; set; }

        [JsonProperty("rightsHolder")]
        public object RightsHolder { get; set; }

        [JsonProperty("document")]
        public Document Document { get; set; }

        [JsonProperty("jurisdiction")]
        public Jurisdiction Jurisdiction { get; set; }

        [JsonProperty("standards")]
        public Standards Standards { get; set; }
    }
}
