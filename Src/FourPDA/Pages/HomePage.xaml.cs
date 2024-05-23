// HomePage

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Security.Cryptography.Certificates;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static FourPDA.MainPage;
using Windows.System;
//using Octokit;
using System.Threading;
using ExceptionHelper;
using System.Net.NetworkInformation;
//using SharpCompress.Archives;
//using SharpCompress.Common;
//using SharpCompress.Readers;


// FourPDA namespace
namespace FourPDA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Windows.UI.Xaml.Controls.Page
    {
        /// <summary>
        /// MUST CHANGE THESE BEFORE EACH PUBLIC GITHUB RELEASE
        /// 
        /// MUST CHANGE THESE BEFORE EACH PUBLIC GITHUB RELEASE
        /// 
        /// MUST CHANGE THESE BEFORE EACH PUBLIC GITHUB RELEASE
        /// </summary>
        public static string CurrentBuildVersion = "2.0.0-alpha";//"1.13.20-prerelease";
        public static string PreviousBuildVersion = "1.0.35";
        public static string NextBuildVersion = "2.0.1-alpha";
        public static string UploadedFileName = "4PDAClient.zip";
        public static string AppxUpdateName = "4PDAClient_x86_x64_arm_Debug.appxbundle";
        /// <summary>
        /// 
        /// </summary>
        
        public StorageFolder folder { get; set; }
        public StorageFile file { get; set; }
        public bool isLatestBuild { get; set; }
        public static string UpdateURL { get; set; }
        DownloadOperation downloadOperation;
        CancellationTokenSource cancellationToken;

        BackgroundDownloader backgroundDownloader = new BackgroundDownloader();
        public HomePage()
        {
            this.InitializeComponent();
            DLUpdate.IsEnabled = true;
            DLUpdate.Visibility = Visibility.Visible;
            InstallUpdateBtn.Visibility = Visibility.Collapsed;

            ProgressBarDownload.Visibility = Visibility.Collapsed;
            DLUpdate.Visibility = Visibility.Collapsed;
            HomePageTxt.Text = $"[Version: {CurrentBuildVersion}]\n"; 
            HomePageTxt.Text += "This is in ongoing development, suggestions and feedback are welcome. Features and UI not final" + "\n\n";
            HomePageTxt.Text += "A simple tool to help users:" + "\n" +
                             "• Read News" + "\n"
                           + "• Communicate Forum" + "\n"
                           + "• Search info and Download files" + "\n"
                           + "• Make catalog of Favorites" + "\n"
                           + "• Interact with UI easily";

            UpdateOut.Text += $"Whats New?\n" +
                       $"- some UI experemintations";
            ///
            /// Network Check and Check for Updates
            bool isNetworkConnected = NetworkInterface.GetIsNetworkAvailable();
            if (isNetworkConnected == true)
            {
                //CheckForUpdate();                
            }
            else
            {                
                ProgressBarDownload.Visibility = Visibility.Collapsed;
                DLUpdate.Visibility = Visibility.Collapsed;
            }
            
        }
        /// <summary>
        /// Check Github for latest Release tag, and download if available
        /// </summary>
        private async void CheckForUpdate()
        {
            /*
            await Windows.Storage.ApplicationData.Current.ClearAsync(ApplicationDataLocality.LocalCache);
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Easy-Fetch-UWP"));
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("Empyreal96", "Easy-Fetch-UWP");
            var latestRelease = releases[0];
            
            if (latestRelease.Assets != null && latestRelease.Assets.Count > 0)
            {
                if (latestRelease.TagName == CurrentBuildVersion)
                {
                    isLatestBuild = true;
                    
                    ProgressBarDownload.Visibility = Visibility.Collapsed;
                    DLUpdate.Visibility = Visibility.Collapsed;
                }
                //Test Function
                // if(latestRelease.TagName == PreviousBuildVersion)
                // {
                //     UpdateOut.Text = "You are on an unreleased build";
                // }

                else
                {
                    var updateURL = latestRelease.Assets[0].BrowserDownloadUrl;
                    UpdateURL = $"https://github.com/Empyreal96/Easy-Fetch-UWP/releases/download/{latestRelease.TagName}/{UploadedFileName}";
                    // string updateURL = $"https://github.com/Empyreal96/Easy-Fetch-UWP/releases/download/1.13.16-prerelease/Easy-Fetch_1.13.16.0_Debug_Test.zip";
                    UpdateOut.Visibility = Visibility.Visible;
                    ProgressBarDownload.Visibility = Visibility.Visible;
                    DLUpdate.Visibility = Visibility.Visible;
                    UpdateOut.Text = $"Latest Build: {latestRelease.TagName}\n";
                    UpdateOut.Text += $"Current Build: {CurrentBuildVersion}\n";
                    UpdateOut.Text += $"Date Update Published: {latestRelease.PublishedAt}\n\n";
                   
                    UpdateOut.Text += $"Download URL: {UpdateURL}\n";
                }
            }
            */
        }
       
        /// <summary>
        /// Download the Update Package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DLUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                FolderPicker folderPicker = new FolderPicker();
                folderPicker.SuggestedStartLocation = PickerLocationId.Downloads;
                folderPicker.ViewMode = PickerViewMode.Thumbnail;
                folderPicker.FileTypeFilter.Add("*");
                folder = await folderPicker.PickSingleFolderAsync();
                if (folder == null)
                {
                    return;
                }
                file = await folder.CreateFileAsync($"{UploadedFileName}", CreationCollisionOption.GenerateUniqueName);

                downloadOperation = backgroundDownloader.CreateDownload(new Uri(UpdateURL), file);

                Progress<DownloadOperation> progress = new Progress<DownloadOperation>(progressChanged);
                cancellationToken = new CancellationTokenSource();
                await downloadOperation.StartAsync().AsTask(cancellationToken.Token, progress);
                InstallUpdateBtn.Visibility = Visibility.Visible;
                DLUpdate.Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                Exceptions.ThrownExceptionError(ex);
            }


        }

        private void InstallUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
           
            
            InstallUpdate();
        }

        private async void InstallUpdate()
        {
            /*
            UpdateOut.Text = "Extracting Update to App Cache";
            
            try
            {
                Stream zipStream = await file.OpenStreamForReadAsync();
                using (var zipArchive = ArchiveFactory.Open(zipStream))
                {
                    //It should support 7z, zip, rar, gz, tar
                    var reader = zipArchive.ExtractAllEntries();

                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            await reader.WriteEntryToDirectory(Windows.Storage.ApplicationData.Current.LocalCacheFolder, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }
                var options = new Windows.System.LauncherOptions();
                options.PreferredApplicationPackageFamilyName = "Microsoft.DesktopAppInstaller_8wekyb3d8bbwe";
                options.PreferredApplicationDisplayName = "App Installer";
                UpdateOut.Text = "Attempting to Install Update Package, Please Wait";
                await Windows.System.Launcher.LaunchFileAsync(await Windows.Storage.ApplicationData.Current.LocalCacheFolder.GetFileAsync(AppxUpdateName), options);
                
            } catch (Exception ex)
            {
                UpdateOut.Text = $"An error occured while trying to install: \n{ex.Message}";
            }
            */
        }

        /// <summary>
        /// Progress for Download
        /// </summary>
        /// <param name="downloadOperation"></param>
        private void progressChanged(DownloadOperation downloadOperation)
        {
            int progress = (int)(100 * ((double)downloadOperation.Progress.BytesReceived / (double)downloadOperation.Progress.TotalBytesToReceive));
            //TextBlockProgress.Text = String.Format("{0} of {1} kb. downloaded - {2}% complete.", downloadOperation.Progress.BytesReceived / 1024, downloadOperation.Progress.TotalBytesToReceive / 1024, progress);
            ProgressBarDownload.Value = progress;
            switch (downloadOperation.Progress.Status)
            {
                case BackgroundTransferStatus.Running:
                    {
                        UpdateOut.Text = $"Downloading from {UpdateURL}";
                        //ButtonPauseResume.Content = "Pause";
                        break;
                    }
                case BackgroundTransferStatus.PausedByApplication:
                    {
                        UpdateOut.Text = "Download paused.";
                        //ButtonPauseResume.Content = "Resume";
                        break;
                    }
                case BackgroundTransferStatus.PausedCostedNetwork:
                    {
                        UpdateOut.Text = "Download paused because of metered connection.";
                        //ButtonPauseResume.Content = "Resume";
                        break;
                    }
                case BackgroundTransferStatus.PausedNoNetwork:
                    {
                        UpdateOut.Text = "No network detected. Please check your internet connection.";
                        break;
                    }
                case BackgroundTransferStatus.Error:
                    {
                        UpdateOut.Text = "An error occured while downloading.";
                        break;
                    }
            }
            if (progress >= 100)
            {
                UpdateOut.Text = $"Download complete. Update downloaded to {folder.Path}\\{UploadedFileName}";
               // ButtonCancel.IsEnabled = false;
                //ButtonPauseResume.IsEnabled = false;
                //ButtonDownload.IsEnabled = true;
                downloadOperation = null;
            }
        }
    }
    
}
