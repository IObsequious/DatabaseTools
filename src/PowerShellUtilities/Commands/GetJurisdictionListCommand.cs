using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PowerShellUtilities.JsonModel;
using PowerShellUtilities.JsonModel.List;
using PowerShellUtilities.Utilities;

namespace PowerShellUtilities.Commands
{
    [Cmdlet(VerbsCommon.Get, "JurisdictionList", SupportsPaging = true)]
    public class GetJurisdictionListCommand : AbstractPSCmdlet
    {
        protected override void ProcessRecord()
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Api-Key", ApiKey);

            HttpWebRequest request = HttpWebRequest.CreateHttp(RequestUri);
            request.CachePolicy = RestApi.CachePolicy;
            request.Headers = headers;

            Stopwatch stopWatch = Stopwatch.StartNew();
            var responseAsync = GetWebResponseAsync(request);
            while (!responseAsync.IsCompleted)
            {
                WriteProgress(ProgressRecordFactory.Progress($"Waiting for response from {RequestUri}...", $"Time Elapsed: {stopWatch.Elapsed.Seconds}"));
            }

            HttpWebResponse response = (HttpWebResponse)responseAsync.Result;

            if (response.IsFromCache)
            {
                WriteWarning("Respose was received from the cash!");
            }

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string text = reader.ReadToEnd();

                JObject obj = JObject.Parse(text);


                if (ReturnJObject)
                {
                    WriteObject(obj);
                }
                else
                {
                    JurisdictionList list = JsonModelSerializer.DeserializeList(obj.ToString());

                    PagingUtilities.Page(this, list.Data.ToArray());
                }

            }

        }


        private Task<WebResponse> GetWebResponseAsync(HttpWebRequest request)
        {
            return request.GetResponseAsync();
        }
    }
}
