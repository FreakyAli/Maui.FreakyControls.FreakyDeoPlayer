using System;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Upstream;
using LayoutParams = Android.Views.ViewGroup.LayoutParams;

namespace Maui.FreakyControls;

public partial class FreakyDeoPlayerHandler
{
    protected override FreakyNativeAndroidPlayer CreatePlatformView()
    {
        var exoPlayerView = new FreakyNativeAndroidPlayer(this.Context);

        var HttpDataSourceFactory = new DefaultHttpDataSource.Factory().SetAllowCrossProtocolRedirects(true);
        var MainDataSource = new ProgressiveMediaSource.Factory(HttpDataSourceFactory);
        var Exoplayer = new IExoPlayer.Builder(this.Context).SetMediaSourceFactory(MainDataSource).Build();

        exoPlayerView.Player = Exoplayer;
        exoPlayerView.Player.Prepare();
        exoPlayerView.Player.PlayWhenReady = false;
        exoPlayerView.LayoutParameters = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
        return exoPlayerView;
    }

}