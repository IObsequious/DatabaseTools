using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.StandardSets
{

    [JsonArray]
    public class Standards : List<Standard>
    {
        public Standards()
        {
        }

        public Standards(int capacity) : base(capacity)
        {
        }

        public Standards(IEnumerable<Standard> collection) : base(collection)
        {
        }
    }
}
