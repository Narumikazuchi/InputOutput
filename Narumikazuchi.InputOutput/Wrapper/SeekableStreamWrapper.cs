namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can utilize seeking (<see cref="Stream.CanSeek"/>) into the
/// <see cref="ISeekableStream"/> interface.
/// </summary>
public readonly partial struct SeekableStreamWrapper
{
#pragma warning disable CS1591 // XML Comment
    static public implicit operator SeekableStreamWrapper(Stream stream)
    {
        return new(stream);
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of the <see cref="SeekableStreamWrapper"/> struct.
    /// </summary>
    public SeekableStreamWrapper()
    {
        m_Stream = Stream.Null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SeekableStreamWrapper"/> struct.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <exception cref="ArgumentException"/>
    public SeekableStreamWrapper(Stream stream)
    {
        if (!stream.CanSeek)
        {
            throw new ArgumentException(message: "The stream you are trying to wrap is nto seekable.",
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
            return m_Stream ?? Stream.Null;
        }
    }

    internal readonly Stream m_Stream = Stream.Null;
}