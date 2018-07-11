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
    public abstract class AbstractPSCmdlet : PSCmdlet
    {
        [Parameter]
        public string ApiKey { get; set; } = Settings.Default.ApiKey;

        [Parameter]
        public string RequestUri { get; set; } = Settings.Default.JurisdictionsListURL;

        [Parameter]
        public SwitchParameter ReturnJObject { get; set; }

        protected string InvokeWebRequest()
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Api-Key", ApiKey);

            string url = RequestUri;

            if (this is GetJurisdictionCommand getJurisdictionCommand)
            {
                url += $"/{getJurisdictionCommand.Id}";
            }

            var request = WebRequest.CreateHttp(url);
            request.CachePolicy = RestApi.CachePolicy;
            request.Headers = headers;
            Stopwatch stopWatch = Stopwatch.StartNew();
            var responseAsync = request.GetResponseAsync();
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
                return reader.ReadToEnd();
            }
        }

        protected string InvokeWebRequest(JurisdictionListDataItem item)
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Api-Key", ApiKey);

            var request = WebRequest.CreateHttp($"{Settings.Default.JurisdictionsListURL}/{item.Id}");
            request.CachePolicy = RestApi.CachePolicy;
            request.Headers = headers;
            Stopwatch stopWatch = Stopwatch.StartNew();
            var responseAsync = request.GetResponseAsync();
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
                return reader.ReadToEnd();
            }
        }
    }
}
