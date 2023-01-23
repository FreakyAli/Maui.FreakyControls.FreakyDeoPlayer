﻿using System;
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
