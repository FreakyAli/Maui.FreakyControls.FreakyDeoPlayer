using System.ComponentModel;
using System.Globalization;

namespace Maui.FreakyControls;

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
