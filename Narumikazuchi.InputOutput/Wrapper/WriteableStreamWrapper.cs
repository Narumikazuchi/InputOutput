namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can be writen to (<see cref="Stream.CanWrite"/>) into the
/// <see cref="IWriteableStream"/> interface.
/// </summary>
public readonly partial struct WriteableStreamWrapper
{
#pragma warning disable CS1591 // XML Comment
    static public implicit operator WriteableStreamWrapper(Stream stream)
    {
        return new(stream);
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteableStreamWrapper"/> struct.
    /// </summary>
    public WriteableStreamWrapper()
    {
        m_Stream = Stream.Null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteableStreamWrapper"/> struct.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <exception cref="ArgumentException"/>
    public WriteableStreamWrapper(Stream stream)
    {
        if (!stream.CanWrite)
        {
            throw new ArgumentException(message: "The stream you are trying to wrap can not be written to.",
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