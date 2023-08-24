namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can timeout (<see cref="Stream.CanTimeout"/>) into the
/// <see cref="ITimeoutStream"/> interface.
/// </summary>
public readonly partial struct TimeoutStreamWrapper
{
#pragma warning disable CS1591 // XML Comment
    static public implicit operator TimeoutStreamWrapper(Stream stream)
    {
        return new(stream);
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeoutStreamWrapper"/> struct.
    /// </summary>
    public TimeoutStreamWrapper()
    {
        m_Stream = Stream.Null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeoutStreamWrapper"/> struct.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <exception cref="ArgumentException"/>
    public TimeoutStreamWrapper(Stream stream)
    {
        if (!stream.CanTimeout)
        {
            throw new ArgumentException(message: "The stream you are trying to wrap can not timeout.",
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