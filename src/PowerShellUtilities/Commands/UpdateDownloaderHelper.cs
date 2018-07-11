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
    public static class UpdateDownloaderHelper
    {

        public static List<IUpdateDownloadContent> ToList(this IUpdateDownloadContentCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            List<IUpdateDownloadContent> list = new List<IUpdateDownloadContent>();
            for (int i = 0; i < collection.Count; i++)
            {
                IUpdateDownloadContent content = collection[i];
                list.Add(content);
            }
            return list;
        }
        public static UpdateDownloader CreateDownloader(this IUpdateSession updateSession)
        {
            return updateSession.CreateUpdateDownloader();
        }

        public static UpdateCollection<TUpdate> AsCollection<TUpdate>(this UpdateCollection collection) where TUpdate: IUpdate
        {
            return new UpdateCollection<TUpdate>(collection);
        }


        public static DownloadSession BeginDownload(this PSCmdlet cmdlet, ISearchResult searchResult)
        {
            UpdateSession session = new UpdateSession();
            UpdateDownloader downloader = session.CreateDownloader();

            downloader.Updates = searchResult.Updates;
            downloader.Priority = DownloadPriority.dpExtraHigh;

            ProgressAdapter adapter = new ProgressAdapter(cmdlet, downloader);

            DownloadProgressChangedCallback progressCallback = new DownloadProgressChangedCallback(adapter);

            DownloadCompletedCallback completedCallback = new DownloadCompletedCallback(downloader);

            object state = new object();

            downloader.BeginDownload(progressCallback, completedCallback, state);

            return DownloadSession.From(completedCallback);

        }

        private class DownloadProgressChangedCallback : IDownloadCompletedCallback
        {
            private readonly IProgress<IDownloadProgress> _reporter;

            public DownloadProgressChangedCallback(IProgress<IDownloadProgress> reporter)
            {
                _reporter = reporter;
            }

            public void Invoke(IDownloadJob downloadJob, IDownloadCompletedCallbackArgs callbackArgs)
            {
                _reporter?.Report(downloadJob.GetProgress());
            }
        }

        internal class DownloadCompletedCallback : IDownloadCompletedCallback
        {
            public UpdateDownloader Downloader { get; }

            public IDownloadJob DownloadJob { get; private set; }

            public IDownloadResult DownloadResult { get; private set; }

            public DownloadCompletedCallback(UpdateDownloader downloader)
            {
                Downloader = downloader;
            }

            public void Invoke(IDownloadJob downloadJob, IDownloadCompletedCallbackArgs callbackArgs)
            {
                DownloadJob = downloadJob;
                DownloadResult = Downloader?.EndDownload(DownloadJob);
            }
        }

        public class DownloadSession : IDisposable
        {
            private readonly DownloadCompletedCallback _completedCallback;
            
            internal static DownloadSession From(DownloadCompletedCallback callback)
            {
                return new DownloadSession(callback);
            }

            private DownloadSession(DownloadCompletedCallback completedCallback)
            {
                _completedCallback = completedCallback;
            }

            public bool Downloading => DownloadResult == null;

            public UpdateDownloader Downloader => _completedCallback.Downloader;

            public IDownloadJob DownloadJob => _completedCallback.DownloadJob;

            public IDownloadResult DownloadResult => _completedCallback.DownloadResult;

            public List<IUpdateDownloadResult> UpdateResults
            {
                get
                {
                    List<IUpdateDownloadResult> list = new List<IUpdateDownloadResult>();
                    int count = Downloader?.Updates?.Count ?? 0;
                    for (int i = 0; i < count; i++)
                    {
                        IUpdateDownloadResult result = DownloadResult.GetUpdateResult(i);
                        list.Add(result);
                    }
                    return list;
                }
            }

            public UpdateCollection<IUpdate5> Updates 
                => Downloader.Updates.AsCollection<IUpdate5>();


            public Dictionary<WindowsUpdate, IUpdateDownloadResult> Dictionary
            {
                get
                {
                    Dictionary<WindowsUpdate, IUpdateDownloadResult> dictionary = new Dictionary<WindowsUpdate, IUpdateDownloadResult>();
                    for (int i = 0; i < Downloader.Updates.Count; i++)
                    {
                        dictionary[new WindowsUpdate(Updates[i])] = UpdateResults[i];
                    }
                    return dictionary;
                }
            }

            public void Dispose()
            {
                
            }
        }

        private class ProgressAdapter : IProgress<IDownloadProgress>
        {
            private readonly PSCmdlet _cmdlet;
            private readonly UpdateDownloader _updateDownloader;

            public ProgressAdapter(PSCmdlet cmdlet, UpdateDownloader updateDownloader)
            {
                _cmdlet = cmdlet;
                _updateDownloader = updateDownloader;
            }

            public void Report(IDownloadProgress value)
            {
                IUpdate5 currentUpdate = (IUpdate5)_updateDownloader.Updates[value.CurrentUpdateIndex];

                _cmdlet.WriteProgress(
                    ProgressRecordFactory.Progress(
                        0,
                        "Downloading Update Collection",
                        $"Download Phase {value.CurrentUpdateDownloadPhase}",
                        value.CurrentUpdateIndex / _updateDownloader.Updates.Count));

                _cmdlet.WriteProgress(
                    ProgressRecordFactory.Progress(
                        1,
                        "Dowloading update",
                        $"Downlading {value.CurrentUpdateBytesDownloaded} of {value.CurrentUpdateBytesToDownload}",
                        $"Downlading {value.CurrentUpdateIndex} of {_updateDownloader.Updates.Count}",
                        value.CurrentUpdatePercentComplete));
            }
        }
    }
}
