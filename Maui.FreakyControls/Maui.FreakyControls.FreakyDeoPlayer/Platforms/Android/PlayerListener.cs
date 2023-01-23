using System;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Audio;
using Com.Google.Android.Exoplayer2.Metadata;
using Com.Google.Android.Exoplayer2.Text;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Video;

namespace Maui.FreakyControls;

public class PlayerListener : Java.Lang.Object, IPlayer.IListener
{
    public void OnAudioAttributesChanged(AudioAttributes audioAttributes)
    {
    }

    public void OnAudioSessionIdChanged(int audioSessionId)
    {
    }

    public void OnAvailableCommandsChanged(IPlayer.Commands availableCommands)
    {
        
    }

    public void OnCues(CueGroup cueGroup)
    {
        
    }

    public void OnDeviceInfoChanged(Com.Google.Android.Exoplayer2.DeviceInfo deviceInfo)
    {
        
    }

    public void OnDeviceVolumeChanged(int volume, bool muted)
    {
        
    }

    public void OnEvents(IPlayer player, IPlayer.Events events)
    {
        
    }

    public void OnIsLoadingChanged(bool isLoading)
    {
        
    }

    public void OnIsPlayingChanged(bool isPlaying)
    {
        
    }

    public void OnLoadingChanged(bool isLoading)
    {
        
    }

    public void OnMaxSeekToPreviousPositionChanged(long maxSeekToPreviousPositionMs)
    {
        
    }

    public void OnMediaItemTransition(MediaItem mediaItem, int reason)
    {
        
    }

    public void OnMediaMetadataChanged(MediaMetadata mediaMetadata)
    {
        
    }

    public void OnMetadata(Metadata metadata)
    {
        
    }

    public void OnPlaybackParametersChanged(PlaybackParameters playbackParameters)
    {
        
    }

    public void OnPlaybackStateChanged(int playbackState)
    {
        
    }

    public void OnPlaybackSuppressionReasonChanged(int playbackSuppressionReason)
    {
        
    }

    public void OnPlayerError(PlaybackException error)
    {
        
    }

    public void OnPlayerErrorChanged(PlaybackException error)
    {
        
    }

    public void OnPlayerStateChanged(bool playWhenReady, int playbackState)
    {
        
    }

    public void OnPlaylistMetadataChanged(MediaMetadata mediaMetadata)
    {
        
    }

    public void OnPlayWhenReadyChanged(bool playWhenReady, int reason)
    {
        
    }

    public void OnPositionDiscontinuity(int reason)
    {
        
    }

    public void OnRenderedFirstFrame()
    {
        
    }

    public void OnRepeatModeChanged(int repeatMode)
    {
        
    }

    public void OnSeekBackIncrementChanged(long seekBackIncrementMs)
    {
        
    }

    public void OnSeekForwardIncrementChanged(long seekForwardIncrementMs)
    {
        
    }

    public void OnSeekProcessed()
    {
        
    }

    public void OnShuffleModeEnabledChanged(bool shuffleModeEnabled)
    {
        
    }

    public void OnSkipSilenceEnabledChanged(bool skipSilenceEnabled)
    {
        
    }

    public void OnSurfaceSizeChanged(int width, int height)
    {
        
    }

    public void OnTimelineChanged(Timeline timeline, int reason)
    {
        
    }

    public void OnTracksChanged(Tracks tracks)
    {
        
    }

    public void OnTrackSelectionParametersChanged(TrackSelectionParameters parameters)
    {
        
    }

    public void OnVideoSizeChanged(VideoSize videoSize)
    {
        
    }

    public void OnVolumeChanged(float volume)
    {
        
    }
}

