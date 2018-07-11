using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.StandardSets
{

    [JsonArray(ItemConverterType = typeof(StandardConverter))]
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
