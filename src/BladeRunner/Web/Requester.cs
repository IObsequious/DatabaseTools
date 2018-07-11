using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BladeRunner.JsonModel.List;
using System.Net;
using System.Net.Cache;
using BladeRunner.JsonModel.StandardSets;
using BladeRunner.JsonModel.Jurisdictions;
using Newtonsoft.Json.Linq;

namespace BladeRunner.Web
{
    internal static class Requester
    {
        public const string ApiKey = "dcCDY5fhLLdYymBNRLPwaYao";

        public const string AuthorizationHeader = "?ApiKey=dcCDY5fhLLdYymBNRLPwaYao";

        public const string JurisdictionsBaseURL = "http://commonstandardsproject.com/api/v1/jurisdictions";

        public const string StandardSetsBaseURL = "http://commonstandardsproject.com/api/v1/standard_sets";

        private static JurisdictionList GetJurisdictionList()
        {
            string jsonList = InvokeWebRequest(JurisdictionsBaseURL);

            JurisdictionList list = JurisdictionListSerializer.Deserialize(jsonList);
            return list;
        }

        public static JurisdictionCollection GetJurisdictions()
        {
            JurisdictionCollection collection = new JurisdictionCollection();
            JurisdictionList list = GetJurisdictionList();

            int total = list.Data.Count;

            for (var i = 0; i < total; i++)
            {
                var data = list.Data[i];
                string jurisdictionURL = JurisdictionsBaseURL + "/" + data.Id;

                int percent = (int)Math.Round(i * ((decimal)100 / total));

                // ConsoleEx.WriteProgress("Getting Jurisdictions", "Jurisdictions", data.Title, percent);

                string jurisdictionJson = InvokeWebRequest(jurisdictionURL);

                var jurisdiction = JurisdictionsSerializer.Deserialize(jurisdictionJson);

                collection.Add(jurisdiction);
            }

            Console.WriteLine();

            return collection;
        }

        public static BladeRunner.JsonModel.StandardSets.StandardSet GetStandardSet(BladeRunner.JsonModel.Jurisdictions.StandardSet standardSet)
        {
            string url = StandardSetsBaseURL + "/" + standardSet.Id;

            string json = InvokeWebRequest(url);

            JObject jObject = JObject.Parse(json);

            JObject fixedObject = FixProperty(jObject);

            json = fixedObject.ToString();

            return StandardSetsSerializer.Deserialize(json);

        }

        private static JObject FixProperty(JObject @object)
        {
            var property = (JObject)@object["data"]["standards"];

            JArray array = new JArray();

            foreach (var subProperty in property.Properties())
            {
                JObject extract = subProperty.Value as JObject;
                array.Add(extract);
            }

            @object["data"]["standards"] = array;

            return @object;
        }


        private static string InvokeWebRequest(string url)
        {
            WebRequest request = WebRequest.CreateHttp(new Uri(url));
            request.CachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAge, TimeSpan.FromDays(1));
            request.Headers.Add("Api-Key", ApiKey);

            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
