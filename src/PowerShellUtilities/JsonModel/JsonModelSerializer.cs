using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PowerShellUtilities.JsonModel.List;
using PowerShellUtilities.JsonModel.Jurisdictions;
using PowerShellUtilities.JsonModel.StandardSets;

namespace PowerShellUtilities.JsonModel
{
    public static class JsonModelSerializer
    {
        public static JurisdictionList DeserializeList(string json)
        {
            return (JurisdictionList)JsonConvert.DeserializeObject<JurisdictionList>(json);
        }

        public static PowerShellUtilities.JsonModel.Jurisdictions.Jurisdiction DeserializeJurisdiction(string json)
        {
            return (PowerShellUtilities.JsonModel.Jurisdictions.Jurisdiction)JsonConvert.DeserializeObject<PowerShellUtilities.JsonModel.Jurisdictions.Jurisdiction>(json);
        }

        public static PowerShellUtilities.JsonModel.StandardSets.StandardSet DeserializeStandardSet(string json)
        {
            return (PowerShellUtilities.JsonModel.StandardSets.StandardSet)JsonConvert.DeserializeObject<PowerShellUtilities.JsonModel.StandardSets.StandardSet>(json);
        }

        public static string SerializeCollection(JurisdictionCollection collection)
        {
            return JsonConvert.SerializeObject(collection, Formatting.Indented);
        }

        public static string SerializeCollection(StandardSetCollection collection)
        {
            return JsonConvert.SerializeObject(collection, Formatting.Indented);
        }
    }
}
