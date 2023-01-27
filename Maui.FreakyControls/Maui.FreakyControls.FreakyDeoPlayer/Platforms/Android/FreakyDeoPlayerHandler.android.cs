using System;
using AndroidX.Fragment.App;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.UI;
using Com.Google.Android.Exoplayer2.Upstream;
using Microsoft.Maui.Controls.PlatformConfiguration;
using LayoutParams = Android.Views.ViewGroup.LayoutParams;
namespace Maui.FreakyControls;

public partial class FreakyDeoPlayerHandler
{
    private readonly PlayerListener listener = new PlayerListener();

    protected override FreakyNativeAndroidPlayer CreatePlatformView()
    {
        var HttpDataSourceFactory = new DefaultHttpDataSource.Factory().SetAllowCrossProtocolRedirects(true);
        var MainDataSource = new ProgressiveMediaSource.Factory(HttpDataSourceFactory);
        var exoplayer = new IExoPlayer.Builder(this.Context).
            SetSeekBackIncrementMs(10000).
            SetSeekForwardIncrementMs(10000).
            SetMediaSourceFactory(MainDataSource).
            Build();

        var exoPlayerView = new FreakyNativeAndroidPlayer(this.Context)
        {
            Player = exoplayer,
            ControllerShowTimeoutMs=4000,
           
            ControllerHideOnTouch = true,
            LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent),
        };
        exoPlayerView.Player.AddListener(listener);
        exoPlayerView.Player.Prepare();
        exoPlayerView.Player.PlayWhenReady = true;
        exoPlayerView.FullscreenButtonClick += ExoPlayerView_FullscreenButtonClick;
        return exoPlayerView;
    }

    private void ExoPlayerView_FullscreenButtonClick(object sender, StyledPlayerView.FullscreenButtonClickEventArgs e)
    {

    }

    private void UpdateVolume()
    {
        PlatformView.Player.Volume = (float)VirtualView.Volume;
    }

    private void UpdateAutoPlay()
    {
        PlatformView.Player.PlayWhenReady = VirtualView.AutoPlay;
    }

    private void UpdateSource()
    {
        var hasSetSource = false;

        if (PlatformView.Player is null)
        {
            return;
        }

        if (VirtualView.Source is null)
        {
            PlatformView.Player.ClearMediaItems();
            //MediaElement.Duration = TimeSpan.Zero;
            //MediaElement.CurrentStateChanged(MediaElementState.None);

            return;
        }

        //MediaElement.CurrentStateChanged(MediaElementState.Opening);

        PlatformView.Player.PlayWhenReady = VirtualView.AutoPlay;

        if (VirtualView.Source is UriVideoSource uriSource)
        {
            var uri = uriSource.Uri;
            if (!string.IsNullOrWhiteSpace(uri?.AbsoluteUri))
            {
                PlatformView.Player.SetMediaItem(MediaItem.FromUri(uri.AbsoluteUri));
                PlatformView.Player.Prepare();

                hasSetSource = true;
            }
        }
        else if (VirtualView.Source is FileVideoSource fileSource)
        {
            var filePath = fileSource.Path;
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                PlatformView.Player.SetMediaItem(MediaItem.FromUri(filePath));
                PlatformView.Player.Prepare();

                hasSetSource = true;
            }
        }
        else if (VirtualView.Source is ResourceVideoSource resourceSource)
        {
            var package = PlatformView?.Context?.PackageName ?? "";
            var path = resourceSource.Path;
            if (!string.IsNullOrWhiteSpace(path))
            {
                string assetFilePath = "asset://" + package + "/" + path;

                PlatformView.Player.SetMediaItem(MediaItem.FromUri(assetFilePath));
                PlatformView.Player.Prepare();

                hasSetSource = true;
            }
        }

        if (hasSetSource && PlatformView.Player.PlayerError is null)
        {
            //VirtualView.MediaOpened();
        }

        PlatformView.Player.Play();
    }
}