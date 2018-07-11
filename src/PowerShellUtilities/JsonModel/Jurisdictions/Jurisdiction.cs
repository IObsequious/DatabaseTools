using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.Jurisdictions
{
    public class Jurisdiction
    {
        [JsonProperty("data")]
        public JurisdictionData Data { get; set; }
    }
}
