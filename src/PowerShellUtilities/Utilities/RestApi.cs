using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Cache;
using System.Net;
using System.Management.Automation;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace PowerShellUtilities.Utilities
{
    internal static class RestApi
    {
        public static HttpRequestCachePolicy CachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAge, TimeSpan.FromDays(1));
        public static string Get(Uri uri, WebHeaderCollection headers, PSCmdlet cmdlet)
        {
            


            WebRequest request = WebRequest.CreateHttp(uri);
            request.CachePolicy = CachePolicy;
            request.Headers = headers;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var responseAsync = request.GetResponseAsync();

            while (!responseAsync.IsCompleted)
            {
                cmdlet.WriteProgress(
                    new ProgressRecord(1, "Waiting for response", $"Time Elapsed: {stopWatch.Elapsed.Seconds} seconds")
                    {
                        CurrentOperation = "Waiting Patiently",
                        RecordType = ProgressRecordType.Processing
                    });
            }

            HttpWebResponse response = (HttpWebResponse) responseAsync.Result;

            //cmdlet.WriteVerbose($"Received from cache: {response.IsFromCache}");

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string text = reader.ReadToEnd();

                if (response.ContentType == "application/json")
                {
                    JObject obj = JObject.Parse(text);

                    return obj.ToString();
                }

                return text;

            }
        }
    }
}
