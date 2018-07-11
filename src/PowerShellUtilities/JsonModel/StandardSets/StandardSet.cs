using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.StandardSets
{
    public class StandardSet
    {

        [JsonProperty("data")]
        public StandardSetData Data { get; set; }
    }
}
