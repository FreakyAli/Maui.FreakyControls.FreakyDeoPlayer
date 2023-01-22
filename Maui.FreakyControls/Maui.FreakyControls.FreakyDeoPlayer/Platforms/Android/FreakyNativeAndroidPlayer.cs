using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace Maui.FreakyControls
{
    public class FreakyNativeAndroidPlayer : VideoView
    {
        public FreakyNativeAndroidPlayer(Context context) : base(context)
        {
        }

        public FreakyNativeAndroidPlayer(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public FreakyNativeAndroidPlayer(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public FreakyNativeAndroidPlayer(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }
    }
}

