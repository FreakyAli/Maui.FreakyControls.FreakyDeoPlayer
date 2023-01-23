using System;
namespace Maui.FreakyControls;

public class FreakyDeoPlayer : View
{
    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
    nameof(Source),
    typeof(VideoSource),
    typeof(FreakyDeoPlayer),
    default(VideoSource));

    public VideoSource Source
    {
        get => (VideoSource)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(
    nameof(AutoPlay),
    typeof(bool),
    typeof(FreakyDeoPlayer),
    default(bool));

    public bool AutoPlay
    {
        get => (bool)GetValue(AutoPlayProperty);
        set => SetValue(AutoPlayProperty, value);
    }

}

