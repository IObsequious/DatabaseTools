using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.Jurisdictions
{
    [JsonArray]
    public class JurisdictionCollection : List<Jurisdiction>
    {
        public JurisdictionCollection()
        {
        }

        public JurisdictionCollection(int capacity) : base(capacity)
        {
        }

        public JurisdictionCollection(IEnumerable<Jurisdiction> collection) : base(collection)
        {
        }
    }
}
