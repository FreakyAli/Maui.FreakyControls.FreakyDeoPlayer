using System;
namespace Maui.FreakyControls;

public partial class FreakyDeoPlayerHandler
{
    protected override FreakyNativeiOSPlayer CreatePlatformView()
    {
        return new FreakyNativeiOSPlayer();
    }

    private void UpdateSource()
    {

    }

    private void UpdateAutoPlay() { }
}

