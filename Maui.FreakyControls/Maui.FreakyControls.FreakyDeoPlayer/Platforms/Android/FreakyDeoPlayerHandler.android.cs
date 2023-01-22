using System;
namespace Maui.FreakyControls;

public partial class FreakyDeoPlayerHandler
{
    protected override FreakyNativeAndroidPlayer CreatePlatformView()
    {
        return new FreakyNativeAndroidPlayer(this.Context);
    }
}

