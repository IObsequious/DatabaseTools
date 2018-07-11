using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.List
{
    public class JurisdictionList
    {
        [JsonProperty("data")]
        public JurisdictionListData Data { get; set; }
    }
}
