using System;
using System.ComponentModel;
using System.Globalization;

namespace Maui.FreakyControls;

/// <summary>
/// Represents a source that can be played by <see cref="FreakyDeoPlayer"/>.
/// </summary>
[TypeConverter(typeof(VideoSourceConverter))]
public abstract class VideoSource : Element
{
    readonly WeakEventManager weakEventManager = new();

    internal event EventHandler SourceChanged
    {
        add => weakEventManager.AddEventHandler(value);
        remove => weakEventManager.RemoveEventHandler(value);
    }

    /// <summary>
    /// An implicit operator to convert a string value into a <see cref="VideoSource"/>.
    /// </summary>
    /// <param name="source">Full path to a local file (starting with <c>file://</c>) or an absolute URI.</param>
    public static implicit operator VideoSource?(string? source) =>
        Uri.TryCreate(source, UriKind.Absolute, out var uri) && uri.Scheme != "file"
            ? FromUri(uri)
            : FromFile(source);

    /// <summary>
    /// An implicit operator to convert a <see cref="Uri"/> object into a <see cref="UriVideoSource"/>.
    /// </summary>
    /// <param name="uri">Absolute URI to load.</param>
    public static implicit operator VideoSource?(Uri? uri) => FromUri(uri);

    /// <summary>
    /// Creates a <see cref="ResourceVideoSource"/> from an absolute URI.
    /// </summary>
    /// <param name="path">Full path to the resource file, relative to the application's resources folder.</param>
    /// <returns>A <see cref="ResourceVideoSource"/> instance.</returns>
    public static VideoSource FromResource(string? path) => new ResourceVideoSource { Path = path };

    /// <summary>
    /// Creates a <see cref="UriVideoSource"/> from an string that contains an absolute URI.
    /// </summary>
    /// <param name="uri">String representation or an absolute URI to load.</param>
    /// <returns>A <see cref="UriVideoSource"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="uri"/> is not an absolute URI.</exception>
    public static VideoSource? FromUri(string uri) => FromUri(new Uri(uri));

    /// <summary>
    /// Creates a <see cref="FileVideoSource"/> from a local path.
    /// </summary>
    /// <param name="path">Full path to the file to load.</param>
    /// <returns>A <see cref="FileVideoSource"/> instance.</returns>
    public static VideoSource FromFile(string? path) => new FileVideoSource { Path = path };

    /// <summary>
    /// Creates a <see cref="UriVideoSource"/> from an absolute URI.
    /// </summary>
    /// <param name="uri">Absolute URI to load.</param>
    /// <returns>A <see cref="UriVideoSource"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="uri"/> is not an absolute URI.</exception>
    public static VideoSource? FromUri(Uri? uri)
    {
        if (uri is null)
        {
            return null;
        }

        if (!uri.IsAbsoluteUri)
        {
            throw new ArgumentException("Uri must be absolute", nameof(uri));
        }

        return new UriVideoSource { Uri = uri };
    }

    /// <summary>
    /// Triggers the <see cref="SourceChanged"/> event.
    /// </summary>
    protected void OnSourceChanged() => weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(SourceChanged));
}

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

/// <summary>
/// Represents a source, loaded from a remote URI, that can be played by <see cref="FreakyDeoPlayer"/>.
/// </summary>
public sealed class UriVideoSource : VideoSource
{
    /// <summary>
    /// Backing store for the <see cref="Uri"/> property.
    /// </summary>
    public static readonly BindableProperty UriProperty =
        BindableProperty.Create(nameof(Uri), typeof(Uri), typeof(UriVideoSource), propertyChanged: OnUriSourceChanged, validateValue: UriValueValidator);

    /// <summary>
    /// Gets or sets the URI to use as a media source.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>The URI has to be absolute.</remarks>
    [TypeConverter(typeof(Microsoft.Maui.Controls.UriTypeConverter))]
    public Uri? Uri
    {
        get => (Uri?)GetValue(UriProperty);
        set => SetValue(UriProperty, value);
    }

    /// <inheritdoc/>
    public override string ToString() => $"Uri: {Uri}";

    /// <summary>
    /// An implicit operator to convert a string value into a <see cref="UriVideoSource"/>.
    /// </summary>
    /// <param name="uri">Full path to the resource file, relative to the application's resources folder.</param>
    public static implicit operator UriVideoSource?(string uri) => (UriVideoSource?)FromUri(uri);

    /// <summary>
    /// An implicit operator to convert a <see cref="UriVideoSource"/> into a string value.
    /// </summary>
    /// <param name="uriVideoSource">A <see cref="UriVideoSource"/> instance to convert to a string value.</param>
    public static implicit operator string?(UriVideoSource? uriVideoSource) => uriVideoSource?.Uri?.ToString();

    static bool UriValueValidator(BindableObject bindable, object value) =>
        value is null || ((Uri)value).IsAbsoluteUri;

    static void OnUriSourceChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((UriVideoSource)bindable).OnSourceChanged();
}

/// <summary>
/// A <see cref="TypeConverter"/> specific to converting a string value to a <see cref="FileVideoSource"/>.
/// </summary>
[TypeConverter(typeof(FileVideoSource))]
public sealed class FileVideoSourceConverter : TypeConverter
{
    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="value"/> is <see langword="null"/> or empty.</exception>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        var filePath = value?.ToString() ?? string.Empty;

        return string.IsNullOrWhiteSpace(filePath)
            ? (FileVideoSource)VideoSource.FromFile(filePath)
            : throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(FileVideoSource)}");
    }
}

/// <summary>
/// A <see cref="TypeConverter"/> specific to converting a string value to a <see cref="VideoSource"/>.
/// </summary>
public sealed class VideoSourceConverter : TypeConverter
{
    const string embeddedResourcePrefix = "embed://";
    const string fileSystemPrefix = "filesystem://";

    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            => sourceType == typeof(string);

    /// <inheritdoc/>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => destinationType == typeof(string);

    /// <inheritdoc/>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        var valueAsString = value?.ToString() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(valueAsString))
        {
            return null;
        }

        var valueAsStringLowercase = valueAsString.ToLowerInvariant();

        if (valueAsStringLowercase.StartsWith(embeddedResourcePrefix))
        {
            return VideoSource.FromResource(
                valueAsString[embeddedResourcePrefix.Length..]);
        }
        else if (valueAsStringLowercase.StartsWith(fileSystemPrefix))
        {
            return VideoSource.FromFile(valueAsString[fileSystemPrefix.Length..]);
        }

        return Uri.TryCreate(valueAsString, UriKind.Absolute, out var uri) && uri.Scheme != "file"
            ? VideoSource.FromUri(uri)
            : VideoSource.FromFile(valueAsString);
    }

    /// <inheritdoc/>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) => value switch
    {
        UriVideoSource uriVideoSource => uriVideoSource.ToString(),
        FileVideoSource fileVideoSource => fileVideoSource.ToString(),
        ResourceVideoSource resourceVideoSource => resourceVideoSource.ToString(),
        VideoSource => string.Empty,
        _ => throw new ArgumentException($"Invalid Media Source", nameof(value))
    };
}