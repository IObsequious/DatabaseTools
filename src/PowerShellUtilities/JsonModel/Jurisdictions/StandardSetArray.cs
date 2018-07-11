using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.Jurisdictions
{
    [JsonArray]
    public class StandardSetArray : List<StandardSet>
    {
        public StandardSetArray()
        {
        }

        public StandardSetArray(int capacity) : base(capacity)
        {
        }

        public StandardSetArray(IEnumerable<StandardSet> collection) : base(collection)
        {
        }
    }
}
