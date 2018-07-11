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

    public class WindowsUpdate : IUpdate5
    {
        private readonly IUpdate5 _update;

        public WindowsUpdate(IUpdate5 update)
        {
            _update = update;
        }

        public void AcceptEula()
        {
            _update.AcceptEula();
        }

        public void CopyFromCache(string path, bool toExtractCabFiles)
        {
            _update.CopyFromCache(path, toExtractCabFiles);
        }

        public void CopyToCache(StringCollection pFiles)
        {
            _update.CopyToCache(pFiles);
        }

        public string Title
        {
            get
            {
                return _update.Title;
            }
        }

        public bool AutoSelectOnWebSites
        {
            get
            {
                return _update.AutoSelectOnWebSites;
            }
        }

        public IReadOnlyList<IUpdate5> BundledUpdates
        {
            get
            {
                return _update.BundledUpdates.AsCollection<IUpdate5>();
            }
        }

        UpdateCollection IUpdate.BundledUpdates
        {
            get
            {
                return _update.BundledUpdates;
            }
        }
        UpdateCollection IUpdate2.BundledUpdates
        {
            get
            {
                return _update.BundledUpdates;
            }
        }

        UpdateCollection IUpdate3.BundledUpdates
        {
            get
            {
                return _update.BundledUpdates;
            }
        }

        UpdateCollection IUpdate4.BundledUpdates
        {
            get
            {
                return _update.BundledUpdates;
            }
        }
        UpdateCollection IUpdate5.BundledUpdates
        {
            get
            {
                return _update.BundledUpdates;
            }
        }

        public bool CanRequireSource
        {
            get
            {
                return _update.CanRequireSource;
            }
        }

        public ICategoryCollection Categories
        {
            get
            {
                return _update.Categories;
            }
        }

        public dynamic Deadline
        {
            get
            {
                return _update.Deadline;
            }
        }

        public bool DeltaCompressedContentAvailable
        {
            get
            {
                return _update.DeltaCompressedContentAvailable;
            }
        }

        public bool DeltaCompressedContentPreferred
        {
            get
            {
                return _update.DeltaCompressedContentPreferred;
            }
        }

        public string Description
        {
            get
            {
                return _update.Description;
            }
        }

        public bool EulaAccepted
        {
            get
            {
                return _update.EulaAccepted;
            }
        }

        public string EulaText
        {
            get
            {
                return _update.EulaText;
            }
        }

        public string HandlerID
        {
            get
            {
                return _update.HandlerID;
            }
        }

        public IUpdateIdentity Identity
        {
            get
            {
                return _update.Identity;
            }
        }

        public IImageInformation Image
        {
            get
            {
                return _update.Image;
            }
        }

        public IInstallationBehavior InstallationBehavior
        {
            get
            {
                return _update.InstallationBehavior;
            }
        }

        public bool IsBeta
        {
            get
            {
                return _update.IsBeta;
            }
        }

        public bool IsDownloaded
        {
            get
            {
                return _update.IsDownloaded;
            }
        }

        public bool IsHidden
        {
            get
            {
                return _update.IsHidden;
            }

            set
            {
                _update.IsHidden = value;
            }
        }

        public bool IsInstalled
        {
            get
            {
                return _update.IsInstalled;
            }
        }

        public bool IsMandatory
        {
            get
            {
                return _update.IsMandatory;
            }
        }

        public bool IsUninstallable
        {
            get
            {
                return _update.IsUninstallable;
            }
        }

        public StringCollection Languages
        {
            get
            {
                return _update.Languages;
            }
        }

        public DateTime LastDeploymentChangeTime
        {
            get
            {
                return _update.LastDeploymentChangeTime;
            }
        }

        public decimal MaxDownloadSize
        {
            get
            {
                return _update.MaxDownloadSize;
            }
        }

        public decimal MinDownloadSize
        {
            get
            {
                return _update.MinDownloadSize;
            }
        }

        public StringCollection MoreInfoUrls
        {
            get
            {
                return _update.MoreInfoUrls;
            }
        }

        public string MsrcSeverity
        {
            get
            {
                return _update.MsrcSeverity;
            }
        }

        public int RecommendedCpuSpeed
        {
            get
            {
                return _update.RecommendedCpuSpeed;
            }
        }

        public int RecommendedHardDiskSpace
        {
            get
            {
                return _update.RecommendedHardDiskSpace;
            }
        }

        public int RecommendedMemory
        {
            get
            {
                return _update.RecommendedMemory;
            }
        }

        public string ReleaseNotes
        {
            get
            {
                return _update.ReleaseNotes;
            }
        }

        public StringCollection SecurityBulletinIDs
        {
            get
            {
                return _update.SecurityBulletinIDs;
            }
        }

        public StringCollection SupersededUpdateIDs
        {
            get
            {
                return _update.SupersededUpdateIDs;
            }
        }

        public string SupportUrl
        {
            get
            {
                return _update.SupportUrl;
            }
        }

        public UpdateType Type
        {
            get
            {
                return _update.Type;
            }
        }

        public string UninstallationNotes
        {
            get
            {
                return _update.UninstallationNotes;
            }
        }

        public IInstallationBehavior UninstallationBehavior
        {
            get
            {
                return _update.UninstallationBehavior;
            }
        }

        public StringCollection UninstallationSteps
        {
            get
            {
                return _update.UninstallationSteps;
            }
        }

        public StringCollection KBArticleIDs
        {
            get
            {
                return _update.KBArticleIDs;
            }
        }

        public DeploymentAction DeploymentAction
        {
            get
            {
                return _update.DeploymentAction;
            }
        }

        public DownloadPriority DownloadPriority
        {
            get
            {
                return _update.DownloadPriority;
            }
        }

        public IReadOnlyList<string> DownloadUrls
        {
            get
            {
                return DownloadContents?.ToList().Select(c=>c.DownloadUrl).ToList();
            }
        }

        public IUpdateDownloadContentCollection DownloadContents
        {
            get
            {
                return _update.DownloadContents;
            }
        }

        public bool RebootRequired
        {
            get
            {
                return _update.RebootRequired;
            }
        }

        public bool IsPresent
        {
            get
            {
                return _update.IsPresent;
            }
        }

        public StringCollection CveIDs
        {
            get
            {
                return _update.CveIDs;
            }
        }

        public bool BrowseOnly
        {
            get
            {
                return _update.BrowseOnly;
            }
        }

        public bool PerUser
        {
            get
            {
                return _update.PerUser;
            }
        }

        public AutoSelectionMode AutoSelection
        {
            get
            {
                return _update.AutoSelection;
            }
        }

        public AutoDownloadMode AutoDownload
        {
            get
            {
                return _update.AutoDownload;
            }
        }
    }
}
