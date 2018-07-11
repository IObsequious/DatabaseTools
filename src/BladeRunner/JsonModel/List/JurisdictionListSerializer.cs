using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.List
{
    internal static class JurisdictionListSerializer
    {
        public static JurisdictionList Deserialize(string json)
        {
            return (JurisdictionList)JsonConvert.DeserializeObject<JurisdictionList>(json);
        }

    }
}
