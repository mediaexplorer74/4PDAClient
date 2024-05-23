// Downloaders Page

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;

using ExceptionHelper;
using Windows.UI.Popups;

using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

using Windows.Networking.BackgroundTransfer;

using System.Threading;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.Media.Editing;
using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;

using Windows.UI.Core;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

//using VideoLibrary;

//using FFmpegInterop;



// FourPDA namespace
namespace FourPDA
{
    // Downloaders class
    public sealed partial class Downloaders : Page
    {
        /*
                /// <summary>
                /// Youtube "entities"
                /// </summary>
                public string link { get; set; }
                public string Title { get; set; }
                public string FileName { get; set; }
                public string Extension { get; set; }
                public string Length { get; set; }
                public string AudioBitrate { get; set; }
                public string AudioFormats { get; set; }
                public string VideoFormat { get; set; }
                public string VideoRes { get; set; }
                public string FPS { get; set; }
                public string VideoID { get; set; }
                public string ChosenQuality { get; set; }
                public int ChosenQualityInt { get; set; }
                public bool IsHDQuality { get; set; }
                public YouTubeVideo video { get; set; }
                public YouTubeVideo maxVideo { get; set; }
                public YouTubeVideo maxBitrate { get; set; }
                public string ThrownEncodingError { get; set; }
                public IEnumerable<YouTubeVideo> videoInfos { get; set; }

                DownloadOperation downloadOperation;
                CancellationTokenSource cancellationToken;

                //DownloadOperation downloadOperationYT;
                IAsyncOperationWithProgress<TranscodeFailureReason, double> renderState;
                CancellationTokenSource cancellationTokenYT;

                BackgroundDownloader backgroundDownloader = new BackgroundDownloader();

                public static string UserFileName { get; set; }



                //////////////////////////////////////////////////////////////////////////////////
                // Start
                //////////////////////////////////////////////////////////////////////////////////
                ///
                /// <summary>
                /// Downloaders
                /// </summary>
                public Downloaders()
                {
                    this.InitializeComponent();
                    HDComboBox.Items.Add("240p"); // 1
                    HDComboBox.Items.Add("360p"); // 2
                    HDComboBox.Items.Add("480p"); // 3
                    HDComboBox.Items.Add("720p"); // 4
                    HDComboBox.Items.Add("1080p"); // 5
                    HDComboBox.Items.Add("2160p"); // 6

                    ChosenQuality = "";

                    progressYT.Visibility = Visibility.Collapsed;
                    progressYT.IsEnabled = false;

                    progressYT.IsEnabled = false;

                    progressYT.Visibility = Visibility.Collapsed;
                    YTDownloadBtn.Visibility = Visibility.Collapsed;
                    YTSearchOutput.Visibility = Visibility.Collapsed;
                    YTThumbnailBorder.Visibility = Visibility.Collapsed;
                    YTDownloadMP3Btn.Visibility = Visibility.Collapsed;

                    // ?
                    contentFrame.Visibility = Visibility.Collapsed;
                    contentFrame.IsEnabled = false;

                    ButtonCancel.Visibility = Visibility.Collapsed;
                    ButtonPauseResume.Visibility = Visibility.Collapsed;
                    ButtonCancelYT.Visibility = Visibility.Collapsed;
                    //ButtonPauseResumeYT.Visibility = Visibility.Collapsed;

                    ProgressBarDownload.Visibility = Visibility.Collapsed;
                }//Downloaders


                /// <summary>
                /// Youtube Downloader Page
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                private async void YTSearchBtn_Click(object sender, RoutedEventArgs e)
                {
                    bool isNetworkConnected = NetworkInterface.GetIsNetworkAvailable();
                    if (isNetworkConnected == false)
                    {
                        ProgressText.Text = "Please connect to the Internet to use";
                        return;

                    }
                    if (ChosenQuality == "")
                    {
                        ProgressText.Text = "Please choose the quality";
                        return;
                    }
                    else
                    {
                        ProgressText.Text = "Searching for Links";
                        if (DownloaderYTHeader.Text == "")
                        {
                            var ThrownException = new MessageDialog("Enter a valid YouTube URL!");
                            ThrownException.Commands.Add(new UICommand("Close"));
                            await ThrownException.ShowAsync();

                            return;
                        }

                        progressYT.IsEnabled = true;

                        progressYT.Visibility = Visibility.Visible;
                        link = DownloaderYTHeader.Text;

                        ///
                        /// Need to fix
                        try
                        {
                            if (link.Contains("http:"))
                            {
                                string fixedlink = link.Replace("http:", "https:");

                                link = fixedlink;
                            }

                            if (link.Contains("m."))
                            {
                                string fixedlink = link.Replace("m.", "www.");
                                link = fixedlink;
                            }

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("[ex] link.Replace error: " + ex.Message);
                            Exceptions.ThrownExceptionError(ex);
                        }

                        if (IsHDQuality == true)
                        {
                            //DownloadHDBitsTest();
                            var youTube = YouTube.Default; // starting point for YouTube actions
                            var Justvideo = youTube.GetVideo(link); // gets a Video object with info about the video

                            //videoInfos = youTube.GetAllVideosAsync(link).GetAwaiter().GetResult();
                            videoInfos = await youTube.GetAllVideosAsync(link);
                            //ProgressText.Text = "";

                            //var video = videoInfos.First(i => i.IsAdaptive); 
                            //videoInfos.First(i => i.Resolution == videoInfos.Max(j => j.Resolution));
                            try
                            {
                                maxVideo = videoInfos.First(i => i.Resolution == ChosenQualityInt);
                            }
                            catch(Exception ex)
                            {
                                Exceptions.ThrownExceptionError(ex);
                                progressYT.IsEnabled = false;
                                progressYT.Visibility = Visibility.Collapsed;
                                return;
                            }

                            //New Code Below
                            maxBitrate = videoInfos.Where(
                                v => v.AudioFormat == AudioFormat.Aac 
                                     && v.AdaptiveKind == AdaptiveKind.Audio).FirstOrDefault();

                            //maxBitrate = videoInfos.First(
                            //i => i.AudioBitrate == videoInfos.Max(j => j.AudioBitrate)); <-- Disabled Code

                            // var mpAudio = videoInfos.First
                            // (i => i.AudioBitrate == videoInfos.Max(j => j.AudioBitrate)); 
                            //FirstOrDefault(x => x.AudioBitrate > 0);

                            string videoURIs = maxVideo.Uri;
                            string audioURIs = maxBitrate.Uri;
                            Title = maxVideo.Title;
                            Extension = maxVideo.FileExtension;
                            string URIs = maxVideo.Uri;

                            var taskVideoSizeCompletionSource = new TaskCompletionSource<bool>();
                            await Task.Run(async () =>
                            {
                                try
                                {
                                    long LengthIn = (long)maxVideo.ContentLength;
                                    Length = LengthIn.ToFileSize();
                                    taskVideoSizeCompletionSource.SetResult(true);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("[i] maxVideo.ContentLength / LengthIn.ToFileSize ex.: " + ex.Message);
                                    taskVideoSizeCompletionSource.SetResult(true);
                                }
                            });
                            await taskVideoSizeCompletionSource.Task;
                            FileName = maxVideo.Title;

                            AudioBitrate = maxBitrate.AudioBitrate.ToString();
                            AudioFormats = maxBitrate.AudioFormat.ToString();
                            VideoFormat = maxVideo.Format.ToString();
                            VideoRes = maxVideo.Resolution.ToString();
                            VideoID = link.Replace("https://www.youtube.com/watch?v=", "");
                            FPS = maxVideo.Fps.ToString();
                            await GetThumbnail();
                            YTDownloadMP3Btn.Visibility = Visibility.Visible;
                            YTThumbnailBorder.Visibility = Visibility.Visible;
                            YTSearchOutput.Visibility = Visibility.Visible;
                            YTSearchOutput.Text = $"Video Information:\n\n" +
                                    $"File Name: {Title}\n" +
                                    $"File Extension: {Extension}\n" +
                                    $"File Size: {Length}\n" +
                                    //$"Audio Bitrate: {AudioBitrate}\n" +
                                    //$"Audio Format: {AudioFormats}\n" +
                                    //$"Video Format: {VideoFormat}\n" +
                                    //$"Video Resolution: {VideoRes}\n" +
                                    //$"Video FPS: {FPS}\n\n" +
                                    $"URI: {URIs}\n\n" +
                                    $"Length: {maxVideo.Info.LengthSeconds}s";//\n{maxVideo.IsAdaptive}\n";

                            YTDownloadBtn.Visibility = Visibility.Visible;

                            progressYT.IsEnabled = false;
                            progressYT.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            //await CheckVideo(link);

                            // starting point for YouTube actions
                            YouTube youTube = YouTube.Default; 

                            // gets a Video object with info about the video
                            YouTubeVideo Justvideo = youTube.GetVideo(link); 
                            videoInfos = youTube.GetAllVideosAsync(link).GetAwaiter().GetResult();

                            //ProgressText.Text = "";

                            try
                            {
                                video = videoInfos.First(i => i.IsAdaptive);
                            }
                            catch { }

                            if (video.Title == null || video.Title == "")
                            {
                                video = videoInfos.First(i => i.Resolution == ChosenQualityInt);
                            }
                            //videoInfos.First(i => i.Resolution == videoInfos.Max(j => j.Resolution));

                            //var maxVideo = videoInfos.First(i => i.Resolution == videoInfos.Max(j => j.Resolution));
                            //var maxBitrate = videoInfos.First(i => i.AudioBitrate == videoInfos.Max(j => j.AudioBitrate));

                            YouTubeVideo minBitrate = videoInfos.First(
                                i => i.AudioBitrate == videoInfos.Min(j => j.AudioBitrate));

                            string URIs = video.Uri;
                            //var adaptive = videoInfos.First(i => i.IsAdaptive);
                            Title = video.Title;
                            Extension = video.FileExtension;
                            //TimeSpan durationinseconds = TimeSpan.FromMilliseconds(video.ContentLength);
                            var taskVideoSizeCompletionSource = new TaskCompletionSource<bool>();
                            await Task.Run(async () =>
                            {
                                try
                                {
                                    long LengthIn = (long)video.ContentLength;
                                    Length = LengthIn.ToFileSize();
                                    taskVideoSizeCompletionSource.SetResult(true);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("[i] video.ContentLength / LengthIn.ToFileSize ex.: " + ex.Message);
                                    taskVideoSizeCompletionSource.SetResult(true);
                                }
                            });

                            await taskVideoSizeCompletionSource.Task;
                            AudioBitrate = video.AudioBitrate.ToString();
                            FileName = video.Title;
                            AudioFormats = video.AudioFormat.ToString();
                            VideoFormat = video.Format.ToString();
                            FPS = video.Fps.ToString();
                            VideoRes = video.Resolution.ToString();

                            VideoID = link.Replace("https://www.youtube.com/watch?v=", "");
                            await GetThumbnail();

                            YTDownloadMP3Btn.Visibility = Visibility.Visible;
                            YTThumbnailBorder.Visibility = Visibility.Visible;
                            YTSearchOutput.Visibility = Visibility.Visible;

                            YTSearchOutput.Text = $"Video Information:\n\n" +
                            $"File Name: {Title}\n" +
                            $"File Extension: {Extension}\n" +
                            $"File Size: {Length}\n" +
                            //$"Audio Bitrate: {AudioBitrate}\n" +
                            //$"Audio Format: {AudioFormats}\n" +
                            //$"Video Format: {VideoFormat}\n" +
                            //$"Video Resolution: {VideoRes}\n" +
                            //$"Video FPS: {FPS}\n\n" +
                            $"URI: {URIs}\n\n" +
                            $"Length: {video.Info.LengthSeconds}s";//\n{video.IsAdaptive}\n";

                            YTDownloadBtn.Visibility = Visibility.Visible;

                            progressYT.IsEnabled = false;
                            progressYT.Visibility = Visibility.Collapsed;
                        }
                    }
                }//YTSearchBtn_Click


                /// <summary>
                /// Get Thumbnail
                /// </summary>
                /// <returns></returns>
                private async Task GetThumbnail()
                {
                    string url = $"http://img.youtube.com/vi/{VideoID}/0.jpg";

                    RandomAccessStreamReference rass = RandomAccessStreamReference.CreateFromUri(new Uri(url));

                    try
                    {
                        using (IRandomAccessStream stream = await rass.OpenReadAsync())
                        {
                            var bitmapImage = new BitmapImage();
                            bitmapImage.SetSource(stream);
                            YTThumbnail.Source = bitmapImage;
                        }
                    }
                    catch 
                    {
                        var bitmapImage = new BitmapImage() { };
                        YTThumbnail.Source = bitmapImage;
                    }
                }//GetThumbnail



                /// <summary>
                /// YTDownloadBtn_Click
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                private async void YTDownloadBtn_Click(object sender, RoutedEventArgs e)
                {
                    if (IsHDQuality == true)
                    {
                        progressYT.IsEnabled = true;
                        progressYT.Visibility = Visibility.Visible;

                        await SaveHDVideoToDisk(link);
                    }
                    else
                    {
                        progressYT.IsEnabled = true;
                        progressYT.Visibility = Visibility.Visible;

                        await SaveVideoToDisk(link);
                    }
                }//YTDownloadBtn_Click



                /// <summary>
                /// SaveVideoToDisk
                /// </summary>
                /// <param name="DLlink"></param>
                /// <returns></returns>
                public async Task SaveVideoToDisk(string DLlink)
                {
                    try
                    {

                        FolderPicker folderPicker = new FolderPicker();
                        folderPicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
                        folderPicker.FileTypeFilter.Add("*");
                        StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                        ProgressText.Text = "Downloading, Please Wait";

                        //ButtonCancelYT.Visibility = Visibility.Visible;

                        progressYT.Visibility = Visibility.Visible;
                        progressYT.IsEnabled = true;


                        cancellationTokenYT = new CancellationTokenSource();

                        string fixedname = fixName(video.Title);

                        StorageFile file = await folder.CreateFileAsync(
                            fixedname + video.FileExtension, 
                            CreationCollisionOption.GenerateUniqueName);

                        await FileIO.WriteBytesAsync(file, await video.GetBytesAsync());

                        ProgressText.Text = "Finished";


                        await DownloadComplete(Title);

                        progressYT.IsEnabled = false;
                        progressYT.Visibility = Visibility.Collapsed;

                        //ButtonCancelYT.Visibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        await DownloadFailed(Title);

                        Debug.WriteLine("[ex] SaveVideoToDisk error: " + ex.Message);

                        Exceptions.ThrownExceptionError(ex);

                        progressYT.IsEnabled = false;
                        progressYT.Visibility = Visibility.Collapsed;

                        //ButtonCancelYT.Visibility = Visibility.Collapsed;
                    }
                }//SaveVideoToDisk


                TaskCompletionSource<bool> EncodingProgress = new TaskCompletionSource<bool>();

                /// <summary>
                /// Save HD Video To Disk
                /// </summary>
                /// <param name="DLlink"></param>
                /// <returns></returns>
                public async Task SaveHDVideoToDisk(string DLlink)
                {
                    try
                    {
                        FolderPicker folderPicker = new FolderPicker();
                        folderPicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

                        folderPicker.FileTypeFilter.Add("*");

                        StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                        ProgressText.Text = "Downloading Video, Please Wait";

                        ButtonCancelYT.Visibility = Visibility.Visible;

                        progressYT.Visibility = Visibility.Visible;
                        progressYT.IsEnabled = true;

                        cancellationTokenYT = new CancellationTokenSource();

                        IProgress<double> VideoProgress = new Progress<double>(async (value) =>
                        {
                            await Windows.ApplicationModel.Core.CoreApplication
                            .MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                progressYT.Value = value;
                                ProgressText.Text = $"Downloading Video ({Math.Round(value)}%), Please Wait";
                            });
                        });

                        StorageFile file = await SaveYoutubeVideo(folder, maxVideo, false, VideoProgress, cancellationTokenYT);


                        ProgressText.Text = "Downloading Audio, Please Wait";

                        IProgress<double> AudioProgress = new Progress<double>(async (value) =>
                        {
                            await Windows.ApplicationModel.Core.CoreApplication
                            .MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                progressYT.Value = value;
                                ProgressText.Text = $"Downloading Audio ({Math.Round(value)}%), Please Wait";
                            });
                        });

                        var file2 = await SaveYoutubeVideo(folder, maxBitrate, true, AudioProgress, cancellationTokenYT);


                        ProgressText.Text = "Encoding Audio and Video streams";
                        await MergeAudioandVideoHDAsync(file, file2, $"{maxVideo.Title}_Merged.mp4", folder);

                        await EncodingProgress.Task;

                        // await DownloadComplete(Title);
                        progressYT.Visibility = Visibility.Collapsed;
                        progressYT.IsEnabled = false;

                        ButtonCancelYT.Visibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        //await DownloadFailed(Title);
                        Exceptions.ThrownExceptionError(ex);
                        YTSearchOutput.Text = $"{ex.Message}\n\n{ex.Source}\n\n{ex.StackTrace}\n\n{ThrownEncodingError}";

                        progressYT.IsEnabled = false;
                        progressYT.Visibility = Visibility.Collapsed;

                        ButtonCancelYT.Visibility = Visibility.Collapsed;
                    }
                }//SaveHDVideoToDisk


                //New Code Below
                public string fixName(string Name)
                {
                    string NotAllowedChars = "[\\/\\:*?\"<>|]";
                    Name = Regex.Replace(Name, NotAllowedChars, "_");

                    return Name;
                }//fixName


                // SaveYoutubeVideo
                public async Task<StorageFile> SaveYoutubeVideo(StorageFolder folder, 
                    YouTubeVideo video, bool AudioFile = false, IProgress<double> progress = null, 
                    CancellationTokenSource cancellationTokenYT = null)
                {
                    var Name = fixName(video.FullName);
                    if (AudioFile)
                    {
                        Name += ".aac";
                    }

                    long totalSize = 0;
                    var taskVideoSizeCompletionSource = new TaskCompletionSource<bool>();
                    await Task.Run(async () =>
                    {
                        try
                        {
                            totalSize = (long)maxVideo.ContentLength;
                            taskVideoSizeCompletionSource.SetResult(true);
                        }
                        catch //(Exception ex)
                        {
                            taskVideoSizeCompletionSource.SetResult(true);
                        }
                    });
                    await taskVideoSizeCompletionSource.Task;


                    //Check if the file exists and completed
                    var testFile = (StorageFile) await folder.TryGetItemAsync(Name);
                    if(testFile!= null)
                    {
                        var fileAttr = await testFile.GetBasicPropertiesAsync();
                        if (fileAttr.Size == 0)
                        {
                            await testFile.DeleteAsync();
                        }else if ((long)fileAttr.Size == totalSize || AudioFile)
                        {
                            //For some reason audio file not return size, for now will consider it's downloaded
                            return testFile;
                        }
                    }
                    var file = await folder.CreateFileAsync(Name, CreationCollisionOption.GenerateUniqueName);

                    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
                    await Task.Run(async () =>
                    {

                        var totalRead = 0L;
                        var buffer = new byte[1024 * 10];
                        var isMoreDataToRead = true;

                        using (var destination = await file.OpenStreamForWriteAsync())
                        {
                            using (var source = await video.StreamAsync())
                            {
                                do
                                {
                                    if (cancellationTokenYT != null)
                                    {
                                        cancellationTokenYT.Token.ThrowIfCancellationRequested();
                                    }

                                    var read = source.Read(buffer, 0, buffer.Length);

                                    if (read == 0)
                                    {
                                        isMoreDataToRead = false;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            // Write data on disk.
                                            destination.Write(buffer, 0, read);
                                        }
                                        catch (Exception e)
                                        {
                                            throw e;
                                        }
                                        totalRead += read;

                                        if (progress != null && totalSize > 0)
                                        {
                                            //await Task.Delay(10);
                                            var progressValue = (totalRead * 1d) / (totalSize * 1d) * 100;
                                            progress.Report(progressValue);
                                        }
                                    }
                                } while (isMoreDataToRead);
                            }
                            taskCompletionSource.SetResult(true);
                        }
                    });

                    await taskCompletionSource.Task;

                    return file;
                }//SaveYoutubeVideo


                /// <summary>
                /// YTDownloadMP3Btn_Click
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                private async void YTDownloadMP3Btn_Click(object sender, RoutedEventArgs e)
                {

                    await SaveAudioToDisk(link);

                }//YTDownloadMP3Btn_Click


                // SaveAudioToDisk
                public async Task SaveAudioToDisk(string DLlink)
                {
                    progressYT.IsEnabled = true;
                    progressYT.Visibility = Visibility.Visible;

                    try
                    {
                        ProgressText.Text = "Downloading, Please Wait";
                        var youTube = YouTube.Default; // starting point for YouTube actions
                        var audio = await YouTube.Default.GetAllVideosAsync(DLlink);
                        var audios = audio.Where(_ => _.AudioFormat == AudioFormat.Aac 
                        && _.AdaptiveKind == AdaptiveKind.Audio).ToList();

                        var mpAudio = audios.First(i => i.AudioBitrate == audios.Max(j => j.AudioBitrate)); 
                        //FirstOrDefault(x => x.AudioBitrate > 0);

                        var video = youTube.GetVideo(DLlink);
                        FolderPicker MP3folderPicker = new FolderPicker();

                        MP3folderPicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

                        MP3folderPicker.FileTypeFilter.Add("*");

                        StorageFolder mp3folder = await MP3folderPicker.PickSingleFolderAsync();

                        if (video.FullName.Contains(".mp4"))
                        {
                            string fixextension = video.FullName.Replace(".mp4", ".mp3");
                            Title = fixextension;
                        }

                        StorageFile mp3file = await mp3folder.CreateFileAsync(
                            Title, CreationCollisionOption.GenerateUniqueName);

                        await FileIO.WriteBytesAsync(mp3file, await mpAudio.GetBytesAsync());

                        ProgressText.Text = "";


                        await DownloadComplete(Title);

                        progressYT.IsEnabled = false;

                        progressYT.Visibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        await DownloadFailed(Title);
                        Exceptions.ThrownExceptionError(ex);
                        progressYT.IsEnabled = false;

                        progressYT.Visibility = Visibility.Collapsed;
                    }
                }//SaveAudioToDisk


                /////////////////////////////////////////////////////////////////
                ///
                /// NOTIFICATINS FOR DOWNLOADS - Likely temporary
                /////////////////////////////////////////////////////////////////


                // DownloadComplete
                public async Task DownloadComplete(string fileName)
                {
                    var DownloadComplete = new MessageDialog($"{fileName} downloaded successfully");
                    DownloadComplete.Commands.Add(new UICommand("Close"));

                    await DownloadComplete.ShowAsync();
                }//DownloadComplete


                // DownloadFailed
                public async Task DownloadFailed(string fileName)
                {
                    var DownloadFailed = new MessageDialog(
                        $"{fileName} Failed to Download. " +
                          $"Try Again or submit an Issue if the problem continues.");

                    DownloadFailed.Commands.Add(new UICommand("Close"));

                    await DownloadFailed.ShowAsync();
                }//DownloadFailed



                /// <summary>
                /// Merge Audio and Video HD Async
                /// </summary>
                /// <param name="videoFile"></param>
                /// <param name="audioFile"></param>
                /// <param name="finalFileName"></param>
                /// <param name="destinationFolder"></param>
                /// <returns></returns>
                public async Task MergeAudioandVideoHDAsync(StorageFile videoFile, StorageFile audioFile, 
                    string finalFileName, StorageFolder destinationFolder)
                {
                    try
                    {
                        EncodingProgress = new TaskCompletionSource<bool>();

                        StorageFile _OutputFile = await destinationFolder.CreateFileAsync(
                            fixName(finalFileName), CreationCollisionOption.GenerateUniqueName);

                        MediaComposition _MediaComposition = new MediaComposition();
                        var clip = await MediaClip.CreateFromFileAsync(videoFile);

                        _MediaComposition.Clips.Add(clip);

                        var audio = await BackgroundAudioTrack.CreateFromFileAsync(audioFile);
                        _MediaComposition.BackgroundAudioTracks.Add(audio);

                        var videoType = "mp4";

                        var Bitrate = clip.GetVideoEncodingProperties().Bitrate;
                        var VideoWidth = clip.GetVideoEncodingProperties().Width;
                        var VideoHeight = clip.GetVideoEncodingProperties().Height;

                        MediaEncodingProfile profile = null;

                        switch (videoType)
                        {
                            case "mp4":
                                profile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD720p);
                                profile.Video.Bitrate = Bitrate;
                                profile.Video.Width = VideoWidth;
                                profile.Video.Height = VideoHeight;
                                string AudioType = profile.Audio.Type;
                                break;
                        }

                        //IAsyncOperationWithProgress<TranscodeFailureReason, double> 
                        renderState 
                            = _MediaComposition.RenderToFileAsync(_OutputFile, 
                            MediaTrimmingPreference.Precise, profile);

                        renderState.Progress =
                            new AsyncOperationProgressHandler<TranscodeFailureReason, double>(
                                async (info, progress) =>
                                {
                                    await Windows.ApplicationModel.Core.CoreApplication
                                      .MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                    {
                                        try
                                        {
                                        //Update your UI progress here
                                        ProgressText.Text = "Encoding Audio and Video streams now";

                                        YTSearchOutput.Text =
                                            $"Bitrate: {profile.Video.Bitrate}\n" +
                                            $"Video Res: {profile.Video.Width} x {profile.Video.Height}\n" +
                                            $"Video ProfileID: {profile.Video.ProfileId}\n" +
                                            $"Video Pixel Aspect:{profile.Video.PixelAspectRatio}\n" +
                                            $"Video Properties: {profile.Video.Properties}\n\n" +
                                            $"Audio Type: {profile.Audio.Type}\n" +
                                            $"Audio Bitrate: {profile.Audio.Bitrate}\n" +
                                            $"Audio Properties: {profile.Audio.Properties.ToString()}" +
                                            $"Container Type: {profile.Container.Type}";
                                            progressYT.Value = progress;
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine(ex.Message);
                                            Exceptions.ThrownExceptionError(ex);
                                        }
                                    });
                        });

                        renderState.Completed = 
                            new AsyncOperationWithProgressCompletedHandler<TranscodeFailureReason, double>(
                                async (info, status) =>
                        {
                            await Windows.ApplicationModel.Core.CoreApplication
                            .MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                            {
                                try
                                {
                                    TranscodeFailureReason results = TranscodeFailureReason.Unknown;

                                    if (info != null)
                                    {
                                        try
                                        {
                                            results = info.GetResults();
                                        }
                                        catch { }
                                    }


                                    ThrownEncodingError = $"Operation finished";
                                }
                                finally
                                {
                                    // Update UI whether the operation succeeded or not
                                    ProgressText.Text = ThrownEncodingError;
                                }

                                EncodingProgress.SetResult(true);
                            });
                        });

                    } catch (Exception ex)
                    {
                        YTSearchOutput.Text = $"Error:\n\n{ex.Message}\n\n{ex.StackTrace}\n\n{ex.InnerException}";
                    }

                }//MergeAudioandVideoHDAsync


                // VideoProfileBySize
                public static VideoEncodingQuality VideoProfileBySize(uint width, uint height)
                {
                    var defaultProfile = VideoEncodingQuality.HD1080p;
                    try
                    {
                        if ((width >= 7680 && height >= 4320) || (width >= 4320 && height >= 7680))
                        {
                            defaultProfile = VideoEncodingQuality.Uhd4320p;
                        }
                        else
                        if ((width >= 4096 && height >= 2160) || (width >= 2160 && height >= 4096))
                        {
                            defaultProfile = VideoEncodingQuality.Uhd2160p;
                        }
                        else
                        if ((width >= 3840 && height >= 2160) || (width >= 2160 && height >= 3840))
                        {
                            defaultProfile = VideoEncodingQuality.Uhd2160p;
                        }
                        else
                        if ((width >= 1920 && height >= 1080) || (width >= 1080 && height >= 1920))
                        {
                            defaultProfile = VideoEncodingQuality.HD1080p;
                        }
                        else if ((width >= 1280 && height >= 720) || (width >= 720 && height >= 1280))
                        {
                            defaultProfile = VideoEncodingQuality.HD720p;
                        }
                        else if ((width >= 768 && height >= 480) || (width >= 480 && height >= 768))
                        {
                            defaultProfile = VideoEncodingQuality.Wvga;
                        }
                        else if ((width >= 640 && height >= 480) || (width >= 480 && height >= 640))
                        {
                            defaultProfile = VideoEncodingQuality.Vga;
                        }
                        else if ((width >= 320 && height >= 240) || (width >= 240 && height >= 320))
                        {
                            defaultProfile = VideoEncodingQuality.Qvga;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                    return defaultProfile;

                }//VideoProfileBySize




                /// <summary>
                /// FileDownloadBtn_Click
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                private async void FileDownloadBtn_Click(object sender, RoutedEventArgs e)
                {
                    ButtonCancel.Visibility = Visibility.Visible;
                    ButtonPauseResume.Visibility = Visibility.Visible;
                    ProgressBarDownload.Visibility = Visibility.Visible;

                    string link = DownloaderFileHeader.Text;
                    string fileNamefetched = Path.GetFileName(link);

                    try
                    {
                        TextBox fileNameUser = new TextBox();
                        fileNameUser.PlaceholderText = "Enter a File Name";
                        fileNameUser.Text = fileNamefetched;
                        fileNameUser.Height = 45;
                        ContentDialog dialog = new ContentDialog();
                        dialog.Content = fileNameUser;
                        dialog.IsSecondaryButtonEnabled = true;
                        dialog.PrimaryButtonText = "Confirm";
                        dialog.SecondaryButtonText = "Cancel";

                        if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                        {
                            if (fileNameUser.Text == "")
                            {
                                fileNameUser.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                            }
                            else
                            {
                                fileNameUser.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Green);
                                UserFileName = fileNameUser.Text;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Exceptions.ThrownExceptionError(ex);
                    }

                    FolderPicker folderPicker = new FolderPicker();
                    folderPicker.SuggestedStartLocation = PickerLocationId.Downloads;
                    folderPicker.ViewMode = PickerViewMode.Thumbnail;
                    folderPicker.FileTypeFilter.Add("*");
                    StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                    if (folder != null)
                    {
                        StorageFile file = await folder.CreateFileAsync(UserFileName,
                            CreationCollisionOption.GenerateUniqueName);

                        // download link to file
                        downloadOperation = backgroundDownloader.CreateDownload(new Uri(link), file);

                        Progress<DownloadOperation> progress = new Progress<DownloadOperation>(progressChanged);

                        cancellationToken = new CancellationTokenSource();

                        ButtonDownload.IsEnabled = false;

                        ButtonCancel.IsEnabled = true;
                        ButtonPauseResume.IsEnabled = true;

                        try
                        {
                            TextBlockStatus.Text = "Initializing...";
                            await downloadOperation.StartAsync().AsTask(cancellationToken.Token, progress);
                        }
                        catch (TaskCanceledException)
                        {
                            TextBlockStatus.Text = "Download canceled.";
                            await downloadOperation.ResultFile.DeleteAsync();
                            ButtonPauseResume.Content = "Resume";

                            ButtonCancel.IsEnabled = false;
                            ButtonPauseResume.IsEnabled = false;                    
                            ButtonDownload.IsEnabled = true;

                            downloadOperation = null;
                        }
                    }

                }//FileDownloadBtn_Click


                // progressChanged
                private void progressChanged(DownloadOperation downloadOperation)
                {
                    int progress = (int)(100 * ((double)downloadOperation.Progress.BytesReceived 
                        / (double)downloadOperation.Progress.TotalBytesToReceive));

                    TextBlockProgress.Text = String.Format("{0} of {1} kb. downloaded - {2}% complete.",
                        downloadOperation.Progress.BytesReceived / 1024, 
                        downloadOperation.Progress.TotalBytesToReceive / 1024, progress);

                    ProgressBarDownload.Value = progress;

                    switch (downloadOperation.Progress.Status)
                    {
                        case BackgroundTransferStatus.Running:
                            {
                                TextBlockStatus.Text = "Downloading...";
                                ButtonPauseResume.Content = "Pause";
                                break;
                            }
                        case BackgroundTransferStatus.PausedByApplication:
                            {
                                TextBlockStatus.Text = "Download paused.";
                                ButtonPauseResume.Content = "Resume";
                                break;
                            }
                        case BackgroundTransferStatus.PausedCostedNetwork:
                            {
                                TextBlockStatus.Text = "Download paused because of metered connection.";
                                ButtonPauseResume.Content = "Resume";
                                break;
                            }
                        case BackgroundTransferStatus.PausedNoNetwork:
                            {
                                TextBlockStatus.Text = "No network detected. Please check your internet connection.";
                                break;
                            }
                        case BackgroundTransferStatus.Error:
                            {
                                TextBlockStatus.Text = "An error occured while downloading.";
                                break;
                            }
                    }
                    if (progress >= 100)
                    {
                        TextBlockStatus.Text = "Download complete.";

                        ButtonCancel.IsEnabled = false;
                        ButtonPauseResume.IsEnabled = false;

                        ButtonDownload.IsEnabled = true;
                        downloadOperation = null;
                    }
                }//progressChanged


                // ButtonPauseResume_Click
                private void ButtonPauseResume_Click(object sender, RoutedEventArgs e)
                {
                    if (ButtonPauseResume.Content.ToString() == "Pause")
                    {
                        try
                        {
                            downloadOperation.Pause();
                        }
                        catch //(InvalidOperationException)
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            downloadOperation.Resume();
                        }
                        catch //(InvalidOperationException)
                        {
                        }
                    }
                }//ButtonPauseResume_Click




                // ButtonCancel_Click
                private void ButtonCancel_Click(object sender, RoutedEventArgs e)
                {
                    try
                    {
                        cancellationToken.Cancel();
                    }
                    catch 
                    {
                        return;
                    }

                    try
                    {
                        cancellationToken.Dispose();
                    }
                    catch 
                    { 
                    }

                }//ButtonCancel_Click


                // ButtonCancelYT_Click
                private void ButtonCancelYT_Click(object sender, RoutedEventArgs e)
                {
                    try
                    { 
                    cancellationTokenYT.Cancel();
                    }
                    catch
                    {
                        return;
                    }

                    try
                    { 
                    cancellationTokenYT.Dispose();
                    }
                    catch
                    {             
                    }
                }//ButtonCancel_Click


                // ShowNotification
                public static void ShowNotification(string title, string stringContent, int expireTime = 15)
                {
                    try
                    {
                        ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();

                        Windows.Data.Xml.Dom.XmlDocument toastXml 
                            = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

                        Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
                        toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
                        toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
                        IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                        Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
                        audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

                        ToastNotification toast = new ToastNotification(toastXml);
                        if (expireTime > 0)
                        {
                            toast.ExpirationTime = DateTime.Now.AddSeconds(expireTime);
                        }
                        ToastNotifier.Show(toast);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }//ShowNotification


                // HDComboBox_SelectionChanged
                private void HDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    var combo = sender as ComboBox;
                    var selecteditem = combo.SelectedItem;
                    //TextBlockStatus.Text = selecteditem.ToString();
                    ChosenQuality = selecteditem.ToString();
                    if (ChosenQuality == "720p")
                    {
                        IsHDQuality = true;
                        ChosenQualityInt = 720;
                    }
                    if (ChosenQuality == "1080p")
                    {
                        IsHDQuality = true;
                        ChosenQualityInt = 1080;
                    }
                    if (ChosenQuality == "2160p")
                    {
                        IsHDQuality = true;
                        ChosenQualityInt = 2160;
                    }
                    if (ChosenQuality == "240p")
                    {
                        IsHDQuality = false;
                        ChosenQualityInt = 240;
                    }
                    if (ChosenQuality == "360p")
                    {
                        IsHDQuality = false;
                        ChosenQualityInt = 360;

                    }
                    if (ChosenQuality == "480p")
                    {
                        IsHDQuality = false;
                        ChosenQualityInt = 480;
                    }

                    //or, since ComboBox DOESN'T support multiple selection, you can get the item like:
                    //var selecteditems = e.AddedItems.FirstOrDefault();

                }//HDComboBox_SelectionChanged


                // Test
                private void Test()
                {
                }//Test
                */

        private void ButtonPauseResume_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void YTSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void YTDownloadMP3Btn_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void ButtonCancelYT_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void HDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }

        private void YTDownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void FileDownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }//class end
}//namespace end

