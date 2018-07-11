using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WUApiLib;
using System.Management.Automation;
using System.Net;

namespace PowerShellUtilities.Commands
{
    internal static class UpdateServiceManagerHelper
    {
        private static PSCmdlet _command;
        public static IUpdateService2 GetService(this PSCmdlet command, bool force)
        {
            _command = command;

            DownloadWusscn2(force);

            IUpdateServiceManager manager = new UpdateServiceManager();

            return (IUpdateService2) manager.AddScanPackageService("Offline Sync Service", "D:\\wsusscn2.cab", 1);
        }

        private static void DownloadWusscn2(bool force)
        {
            if (force)
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += OnDownloadProgressChanged;
                    client.DownloadFileAsync(new Uri("http://go.microsoft.com/fwlink/p/?linkid=74689"), "D:\\wsusscn2.cab");
                    

                }
            }
        }

        private static void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            ProgressRecord record = new ProgressRecord(1, "Downloading wsusscn2.cab", "");
            record.RecordType = ProgressRecordType.Processing;
            record.PercentComplete = e.ProgressPercentage;
            record.CurrentOperation = $"{e.BytesReceived} of {e.TotalBytesToReceive}";

            _command?.WriteProgress(record);
        }
    }
}
