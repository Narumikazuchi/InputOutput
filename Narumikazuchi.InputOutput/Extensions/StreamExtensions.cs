namespace Narumikazuchi.InputOutput;

/// <summary>
/// Extends the abstract <see cref="Stream"/> class to allow easy wrapping.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="IReadableStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="ReadableStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="IReadableStream"/>.</returns>
    public static ReadableStreamWrapper AsReadableStream(this Stream stream) =>
        new(stream);

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="ISeekableStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="SeekableStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="ISeekableStream"/>.</returns>
    public static SeekableStreamWrapper AsSeekableStream(this Stream stream) =>
        new(stream);

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="ITimeoutStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="TimeoutStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="ITimeoutStream"/>.</returns>
    public static TimeoutStreamWrapper AsTimeoutStream(this Stream stream) =>
        new(stream);

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="IWriteableStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="WriteableStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="IWriteableStream"/>.</returns>
    public static WriteableStreamWrapper AsWriteableStream(this Stream stream) =>
        new(stream);
}