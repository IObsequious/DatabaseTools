using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Threading;
using PowerShellUtilities.Utilities;
using WUApiLib;

namespace PowerShellUtilities.Commands
{
    [Cmdlet(VerbsCommon.Get, "WUUpdate")]
    public class GetWUUpdateCommand : PSCmdlet
    {

        [Parameter]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {

            UpdateSearcherHelper.SearchSession searchSession = this.BeginSearch(Force, "IsInstalled=0");

            while (searchSession.Searching)
            {
                WriteProgress(ProgressRecordFactory.Progress("Searching for applicable packages"));
            }

            if (searchSession.SearchResult.Updates.Count == 0)
            {
                WriteWarning("There are no applicable updates.");
                return;
            }

            UpdateDownloaderHelper.DownloadSession downloadSession = this.BeginDownload(searchSession.SearchResult);

            while (downloadSession.Downloading)
            {
                
            }
        
            WriteObject(downloadSession.Dictionary, true);
        }
    }
}
