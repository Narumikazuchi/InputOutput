namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that has a known length into the
/// <see cref="INonContinousStream"/> interface.
/// </summary>
public readonly partial struct NonContinousStreamWrapper
{
#pragma warning disable CS1591 // XML Comment
    static public implicit operator NonContinousStreamWrapper(Stream stream)
    {
        return new(stream);
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of the <see cref="NonContinousStreamWrapper"/> struct.
    /// </summary>
    public NonContinousStreamWrapper()
    {
        m_Stream = Stream.Null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NonContinousStreamWrapper"/> struct.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <exception cref="ArgumentException"/>
    public NonContinousStreamWrapper(Stream stream)
    {
        if (!stream.CanRead)
        {
            throw new ArgumentException(message: "The stream you are trying to wrap can not be read from.",
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