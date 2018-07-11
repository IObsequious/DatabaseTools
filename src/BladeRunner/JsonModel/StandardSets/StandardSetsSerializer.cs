using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.StandardSets
{
    internal static class StandardSetsSerializer
    {
        public static StandardSet Deserialize(string json)
        {
            return (StandardSet)JsonConvert.DeserializeObject<StandardSet>(json);
        }

        public static string SerializeCollection(StandardSetCollection collection)
        {
            return JsonConvert.SerializeObject(collection, Formatting.Indented);
        }
    }
}
