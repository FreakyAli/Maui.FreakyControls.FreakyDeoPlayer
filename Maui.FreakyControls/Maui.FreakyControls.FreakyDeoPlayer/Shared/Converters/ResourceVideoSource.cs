using System.ComponentModel;

namespace Maui.FreakyControls;

/// <summary>
/// Represents a source, loaded from the application's resources, that can be played by <see cref="FreakyDeoPlayer"/>.
/// </summary>
[TypeConverter(typeof(FileVideoSourceConverter))]
public sealed class ResourceVideoSource : VideoSource
{
    /// <summary>
    /// Backing store for the <see cref="Path"/> property.
    /// </summary>
    public static readonly BindableProperty PathProperty
        = BindableProperty.Create(nameof(Path), typeof(string), typeof(ResourceVideoSource), propertyChanged: OnResourceVideoSourceVideoSourceChanged);

    /// <summary>
    /// Gets or sets the full path to the resource file to use as a media source.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// Path is relative to the application's resources folder.
    /// It can only be just a filename if the resource file is in the root of the resources folder.
    /// </remarks>
    public string? Path
    {
        get => (string?)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    /// <inheritdoc/>
    public override string ToString() => $"Resource: {Path}";

    /// <summary>
    /// An implicit operator to convert a string value into a <see cref="ResourceVideoSource"/>.
    /// </summary>
    /// <param name="path">Full path to the resource file, relative to the application's resources folder.</param>
    public static implicit operator ResourceVideoSource(string path) => (ResourceVideoSource)FromFile(path);

    /// <summary>
    /// An implicit operator to convert a <see cref="ResourceVideoSource"/> into a string value.
    /// </summary>
    /// <param name="resourceVideoSource">A <see cref="ResourceVideoSource"/> instance to convert to a string value.</param>
    public static implicit operator string?(ResourceVideoSource? resourceVideoSource) => resourceVideoSource?.Path;

    static void OnResourceVideoSourceVideoSourceChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((ResourceVideoSource)bindable).OnSourceChanged();
}
