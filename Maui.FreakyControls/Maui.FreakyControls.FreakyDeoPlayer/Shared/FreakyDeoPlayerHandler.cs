using System;
using Microsoft.Maui.Handlers;
#if ANDROID
using NativeVideoPlayer = Maui.FreakyControls.FreakyNativeAndroidPlayer;
#elif IOS
using NativeVideoPlayer = Maui.FreakyControls.FreakyNativeiOSPlayer;
#endif
namespace Maui.FreakyControls;

#if ANDROID || IOS
public partial class FreakyDeoPlayerHandler : ViewHandler<FreakyDeoPlayer, NativeVideoPlayer>
{

    public static PropertyMapper<FreakyDeoPlayer, FreakyDeoPlayerHandler> Mapper =
        new(ViewHandler.ViewMapper)
        {
            [nameof(FreakyDeoPlayer.Source)] = MapSource,
            [nameof(FreakyDeoPlayer.AutoPlay)]= MapAutoPlay,
        };

    private static void MapAutoPlay(FreakyDeoPlayerHandler handler, FreakyDeoPlayer view)
    {
        handler.UpdateAutoPlay();
    }

    private static void MapSource(FreakyDeoPlayerHandler handler, FreakyDeoPlayer view)
    {
        handler.UpdateSource();
    }

    public static CommandMapper<FreakyDeoPlayer, FreakyDeoPlayerHandler> CommandMapper =
        new(ViewHandler.ViewCommandMapper)
        {
        };

    public FreakyDeoPlayerHandler() : base(Mapper)
    {
    }

    public FreakyDeoPlayerHandler(IPropertyMapper? mapper)
        : base(mapper ?? Mapper, CommandMapper)
    {
    }

    public FreakyDeoPlayerHandler(IPropertyMapper? mapper, CommandMapper? commandMapper)
        : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
    {
    }

}
#endif