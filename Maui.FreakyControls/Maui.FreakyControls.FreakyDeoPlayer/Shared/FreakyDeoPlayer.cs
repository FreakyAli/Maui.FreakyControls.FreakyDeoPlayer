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

    public static readonly BindableProperty VolumeProperty = BindableProperty.Create(
        nameof(Volume),
        typeof(double),
        typeof(FreakyDeoPlayer),
        1.0,
        BindingMode.TwoWay,
        new BindableProperty.ValidateValueDelegate(ValidateVolume));

    private static bool ValidateVolume(BindableObject bindable, object value)
    {
        var volume = (double)value;
        return volume >= 0.0 && volume <= 1.0;
    }

    public double Volume
    {
        get => (double)GetValue(VolumeProperty);
        set => SetValue(VolumeProperty, value);
    }


}

