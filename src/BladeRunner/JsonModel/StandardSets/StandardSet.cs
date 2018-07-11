using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.StandardSets
{
    public class StandardSet
    {
        public StandardSet()
        {
            Data = new StandardSetData();
        }

        [JsonProperty("data")]
        public StandardSetData Data { get; set; }
    }
}
