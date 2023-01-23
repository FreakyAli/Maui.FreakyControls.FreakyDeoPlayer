using System.ComponentModel;
using System.Globalization;

namespace Maui.FreakyControls;

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