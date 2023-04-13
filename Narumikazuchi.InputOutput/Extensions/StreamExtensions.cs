namespace Narumikazuchi.InputOutput;

/// <summary>
/// Extends the abstract <see cref="Stream"/> class to allow easy wrapping.
/// </summary>
static public class StreamExtensions
{
    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="INonContinousStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="NonContinousStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="INonContinousStream"/>.</returns>
    static public NonContinousStreamWrapper AsNonContinousStreamStream(this Stream stream)
    {
        return new(stream);
    }

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="IFlushableStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="FlushableStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="IFlushableStream"/>.</returns>
    static public FlushableStreamWrapper AsFlushableStream(this Stream stream)
    {
        return new(stream);
    }

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="IReadableStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="ReadableStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="IReadableStream"/>.</returns>
    static public ReadableStreamWrapper AsReadableStream(this Stream stream)
    {
        return new(stream);
    }

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="ISeekableStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="SeekableStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="ISeekableStream"/>.</returns>
    static public SeekableStreamWrapper AsSeekableStream(this Stream stream)
    {
        return new(stream);
    }

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="ITimeoutStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="TimeoutStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="ITimeoutStream"/>.</returns>
    static public TimeoutStreamWrapper AsTimeoutStream(this Stream stream)
    {
        return new(stream);
    }

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="IWriteableStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="WriteableStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="IWriteableStream"/>.</returns>
    static public WriteableStreamWrapper AsWriteableStream(this Stream stream)
    {
        return new(stream);
    }

    /// <summary>
    /// Wraps the current <see cref="Stream"/> into an <see cref="IWriteableNonContinousStream"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <returns>A <see cref="WriteableNonContinousStreamWrapper"/> that allows the <see cref="Stream"/> to be used as <see cref="IWriteableNonContinousStream"/>.</returns>
    static public WriteableNonContinousStreamWrapper AsWriteableNonContinousStream(this Stream stream)
    {
        return new(stream);
    }
}