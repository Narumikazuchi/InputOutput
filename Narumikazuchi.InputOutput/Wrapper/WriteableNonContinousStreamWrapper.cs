namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can be writen to (<see cref="Stream.CanWrite"/>) and has a known length into the
/// <see cref="IWriteableNonContinousStream"/> interface.
/// </summary>
public readonly partial struct WriteableNonContinousStreamWrapper
{
#pragma warning disable CS1591 // XML Comment
    static public implicit operator WriteableNonContinousStreamWrapper(Stream stream)
    {
        return new(stream);
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteableNonContinousStreamWrapper"/> struct.
    /// </summary>
    public WriteableNonContinousStreamWrapper()
    {
        m_Stream = Stream.Null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteableNonContinousStreamWrapper"/> struct.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <exception cref="ArgumentException"/>
    public WriteableNonContinousStreamWrapper(Stream stream)
    {
        if (!stream.CanSeek)
        {
            throw new ArgumentException(message: "The stream you are trying to wrap is not seekable.",
                                        paramName: nameof(stream));
        }
        else
        {
            m_Stream = stream;
        }
    }

    /// <summary>
    /// Gets the underlying <see cref="Stream"/> object for this wrapper.
    /// </summary>
    public Stream UnderlyingStream
    {
        get
        {
            return m_Stream.UnderlyingStream;
        }
    }

    internal readonly WriteableStreamWrapper m_Stream = Stream.Null;
}