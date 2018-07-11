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

    public static class UpdateSearcherHelper
    {
        public static IUpdateSearcher2 CreateSearcher(this UpdateSession session) => (IUpdateSearcher2) session.CreateUpdateSearcher();


        public static SearchSession BeginSearch(this PSCmdlet cmdlet, bool force, string criterea)
        {
            IUpdateService2 service = cmdlet.GetService(force);


            UpdateSession updateSession = new UpdateSession();
            IUpdateSearcher2 searcher = updateSession.CreateSearcher();
            searcher.ServerSelection = ServerSelection.ssOthers;
            searcher.ServiceID = service.ServiceID;


            SearchCompletedCallback callback = new SearchCompletedCallback(searcher);

            object state = new object();

            searcher.BeginSearch(criterea, callback, state);


            return new SearchSession(callback);
        }

        public class SearchCompletedCallback : ISearchCompletedCallback
        {
            public SearchCompletedCallback(IUpdateSearcher2 updateSearcher)
            {
                UpdateSearcher = updateSearcher;
            }

            public ISearchResult SearchResult { get; private set; }
            public IUpdateSearcher2 UpdateSearcher { get; }
            public ISearchJob Job { get; private set; }
            public void Invoke(ISearchJob searchJob, ISearchCompletedCallbackArgs callbackArgs)
            {
                Job = searchJob;
                SearchResult = UpdateSearcher.EndSearch(searchJob);
            }
        }

        public class SearchSession : IDisposable
        {
            private readonly SearchCompletedCallback _completedCallback;

            internal SearchSession(SearchCompletedCallback completedCallback)
            {
                _completedCallback = completedCallback;
            }

            public bool Searching => _completedCallback.Job == null;

            public ISearchResult SearchResult => _completedCallback.SearchResult;

            public UpdateCollection<IUpdate5> Updates 
                => SearchResult.Updates.AsCollection<IUpdate5>();

            public ICategoryCollection Categories => SearchResult.RootCategories;

            public void Dispose()
            {
            }
        }
    }
}
