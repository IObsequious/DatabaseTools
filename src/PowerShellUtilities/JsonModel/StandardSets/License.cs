using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.StandardSets
{

    public class License
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("URL")]
        public string URL { get; set; }

        [JsonProperty("rightsHolder")]
        public string RightsHolder { get; set; }
    }
}
