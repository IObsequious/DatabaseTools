using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.Jurisdictions
{
    [JsonArray]
    public class StandardSets : List<StandardSet>
    {
        public StandardSets()
        {
        }

        public StandardSets(int capacity) : base(capacity)
        {
        }

        public StandardSets(IEnumerable<StandardSet> collection) : base(collection)
        {
        }
    }
}
