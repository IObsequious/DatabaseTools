using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.List
{
    [JsonArray]
    public class JurisdictionListData : List<JurisdictionListDataItem>
    {
        public JurisdictionListData()
        {
        }

        public JurisdictionListData(int capacity) : base(capacity)
        {
        }

        public JurisdictionListData(IEnumerable<JurisdictionListDataItem> collection) : base(collection)
        {
        }
    }
}
