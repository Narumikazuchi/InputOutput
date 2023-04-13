namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can be writen to (<see cref="Stream.CanWrite"/>) and can be flushed into the
/// <see cref="IFlushableStream"/> interface.
/// </summary>
public readonly partial struct FlushableStreamWrapper
{
#pragma warning disable CS1591 // XML Comment
    static public implicit operator FlushableStreamWrapper(Stream stream)
    {
        return new(stream);
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of the <see cref="FlushableStreamWrapper"/> struct.
    /// </summary>
    public FlushableStreamWrapper()
    {
        m_Stream = Stream.Null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FlushableStreamWrapper"/> struct.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <exception cref="ArgumentException"/>
    public FlushableStreamWrapper(Stream stream)
    {
        m_Stream = stream;
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