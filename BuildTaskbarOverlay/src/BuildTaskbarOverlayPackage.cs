using System;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace FourWalledCubicle.BuildTaskbarOverlay
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideOptionPageAttribute(typeof(OptionsPage), "Extensions", "Icon Taskbar Overlay", 15600, 1912, true)]
    [Guid(GuidList.guidBuildTaskbarOverlayPkgString)]
    [ProvideAutoLoad(UIContextGuids.NoSolution)]
    public sealed class BuildTaskbarOverlayPackage : Package
    {
        private readonly DTE mDTE;
        private readonly SolutionEvents mSolutionEvents;
        private readonly BuildEvents mBuildEvents;
        private readonly TaskbarManager mWindowsTaskBar;

        private bool mBuildError;

        private OptionsPage mSettings;

        public BuildTaskbarOverlayPackage()
        {
            try
            {
                mWindowsTaskBar = TaskbarManager.Instance;

                mDTE = Package.GetGlobalService(typeof(DTE)) as DTE;

                mSolutionEvents = mDTE.Events.SolutionEvents;
                mSolutionEvents.AfterClosing += new _dispSolutionEvents_AfterClosingEventHandler(mSolutionEvents_AfterClosing);
                mSolutionEvents.ProjectAdded += new _dispSolutionEvents_ProjectAddedEventHandler(mSolutionEvents_ProjectAltered);
                mSolutionEvents.ProjectRemoved += new _dispSolutionEvents_ProjectRemovedEventHandler(mSolutionEvents_ProjectAltered);

                mBuildEvents = mDTE.Events.BuildEvents;
                mBuildEvents.OnBuildBegin += new _dispBuildEvents_OnBuildBeginEventHandler(mBuildEvents_OnBuildBegin);
                mBuildEvents.OnBuildProjConfigDone += new _dispBuildEvents_OnBuildProjConfigDoneEventHandler(mBuildEvents_OnBuildProjConfigDone);
                mBuildEvents.OnBuildDone += new _dispBuildEvents_OnBuildDoneEventHandler(mBuildEvents_OnBuildDone);
            }
            catch {}
        }

        protected override void Initialize()
        {
            base.Initialize();

            try
            {
                mSettings = GetDialogPage(typeof(OptionsPage)) as OptionsPage;
            }
            catch
            {
                mSettings = new OptionsPage();
            }
        }

        void ResetOverlay()
        {
            mWindowsTaskBar.SetProgressState(TaskbarProgressBarState.NoProgress);
            mWindowsTaskBar.SetOverlayIcon(null, "");
        }

        void mSolutionEvents_AfterClosing()
        {
            ResetOverlay();
        }

        void mSolutionEvents_ProjectAltered(Project Project)
        {
            ResetOverlay();
        }

        void mBuildEvents_OnBuildBegin(vsBuildScope Scope, vsBuildAction Action)
        {
            if (!TaskbarManager.IsPlatformSupported)
                return;

            mWindowsTaskBar.SetOverlayIcon(IconTheme.IconBuilding(mSettings.Theme), "Building in Progress");

            if (mSettings.UseProgressBar)
                mWindowsTaskBar.SetProgressState(TaskbarProgressBarState.Indeterminate);

            mBuildError = false;
        }

        void mBuildEvents_OnBuildProjConfigDone(string Project, string ProjectConfig, string Platform, string SolutionConfig, bool Success)
        {
            if (!Success)
                mBuildError = true;
        }

        void mBuildEvents_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            if (!TaskbarManager.IsPlatformSupported)
                return;

            mWindowsTaskBar.SetProgressState(TaskbarProgressBarState.NoProgress);

            if (mBuildError)
                mWindowsTaskBar.SetOverlayIcon(IconTheme.IconBuildFail(mSettings.Theme), "Build Failed");
            else
                mWindowsTaskBar.SetOverlayIcon(IconTheme.IconBuildOK(mSettings.Theme), "Build Successful");
        }
    }
}
