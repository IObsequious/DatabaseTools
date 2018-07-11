using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.Jurisdictions
{
    internal static class JurisdictionsSerializer
    {
        public static Jurisdiction Deserialize(string json)
        {
            return (Jurisdiction)JsonConvert.DeserializeObject<Jurisdiction>(json);
        }
    }
}
