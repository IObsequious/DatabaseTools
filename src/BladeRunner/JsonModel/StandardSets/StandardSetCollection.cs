using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.StandardSets
{
    [JsonArray]
    public class StandardSetCollection : List<StandardSet>
    {
        public StandardSetCollection()
        {
        }

        public StandardSetCollection(int capacity) : base(capacity)
        {
        }

        public StandardSetCollection(IEnumerable<StandardSet> collection) : base(collection)
        {
        }
    }
}
