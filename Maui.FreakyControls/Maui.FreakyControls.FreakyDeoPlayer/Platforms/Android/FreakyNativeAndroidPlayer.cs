using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.UI;

namespace Maui.FreakyControls;

public class FreakyNativeAndroidPlayer : StyledPlayerView
{
    public FreakyNativeAndroidPlayer(Context context) :
        base(context)
    {
    }

    public FreakyNativeAndroidPlayer(Context context, IAttributeSet attrs) :
        base(context, attrs)
    {
    }

    public FreakyNativeAndroidPlayer(Context context, IAttributeSet attrs, int defStyleAttr) :
        base(context, attrs, defStyleAttr)
    {
    }
}


