using System.ComponentModel;

namespace Maui.FreakyControls;

/// <summary>
/// Represents a source, loaded from local filesystem, that can be played by <see cref="FreakyDeoPlayer"/>.
/// </summary>
[TypeConverter(typeof(FileVideoSourceConverter))]
public sealed class FileVideoSource : VideoSource
{
    /// <summary>
    /// Backing store for the <see cref="Path"/> property.
    /// </summary>
    public static readonly BindableProperty PathProperty
        = BindableProperty.Create(nameof(Path), typeof(string), typeof(FileVideoSource), propertyChanged: OnFileVideoSourceChanged);

    /// <summary>
    /// Gets or sets the full path to the local file to use as a media source.
    /// This is a bindable property.
    /// </summary>
    public string? Path
    {
        get => (string?)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    /// <summary>
    /// An implicit operator to convert a string value into a <see cref="FileVideoSource"/>.
    /// </summary>
    /// <param name="path">Full path to the local file. Can be a relative or absolute path.</param>
    public static implicit operator FileVideoSource(string path) => (FileVideoSource)FromFile(path);

    /// <summary>
    /// An implicit operator to convert a <see cref="FileVideoSource"/> into a string value.
    /// </summary>
    /// <param name="fileVideoSource">A <see cref="FileVideoSource"/> instance to convert to a string value.</param>
    public static implicit operator string?(FileVideoSource? fileVideoSource) => fileVideoSource?.Path;

    /// <inheritdoc/>
    public override string ToString() => $"File: {Path}";

    static void OnFileVideoSourceChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((FileVideoSource)bindable).OnSourceChanged();
}
