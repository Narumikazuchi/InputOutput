namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that has a known length into the
/// <see cref="INonContinousStream"/> interface.
/// </summary>
public readonly partial struct NonContinousStreamWrapper
{
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
            throw new ArgumentException("can't read");
        }
        else
        {
            m_Stream = stream;
        }
    }

    /// <summary>
    /// Gets the underlying <see cref="Stream"/> object for this wrapper.
    /// </summary>
    public Stream UnderlyingStream =>
        m_Stream ?? Stream.Null;

#pragma warning disable CS1591 // XML Comment
    public static implicit operator NonContinousStreamWrapper(Stream stream)
    {
        if (!stream.CanSeek)
        {
            throw new ArgumentException("", nameof(stream));
        }
        else
        {
            return new(stream);
        }
    }
#pragma warning restore
}

// Non-Public
partial struct NonContinousStreamWrapper
{
    internal readonly Stream m_Stream = Stream.Null;
}

// IAsyncDisposable
partial struct NonContinousStreamWrapper : IAsyncDisposable
{
    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (m_Stream is not null)
        {
            await m_Stream.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}

// IDisposable
partial struct NonContinousStreamWrapper : IDisposable
{
    /// <inheritdoc/>
    public void Dispose()
    {
        if (m_Stream is not null)
        {
            m_Stream.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

// IDisposable
partial struct NonContinousStreamWrapper : INonContinousStream
{
    /// <inheritdoc/>
    public void Close() =>
        this.Dispose();

    /// <inheritdoc/>
    public Int64 Length
    {
        get
        {
            if (m_Stream is null)
            {
                return 0;
            }
            else if (m_Stream.CanSeek)
            {
                return m_Stream.Length;
            }
            else
            {
                throw new ObjectDisposedException(null);
            }
        }
    }

    /// <inheritdoc/>
    public Int64 Position
    {
        get
        {
            if (m_Stream is null)
            {
                return 0;
            }
            else if (m_Stream.CanSeek)
            {
                return m_Stream.Position;
            }
            else
            {
                throw new ObjectDisposedException(null);
            }
        }
        set
        {
            if (m_Stream is not null &&
                m_Stream.CanSeek)
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                else
                {
                    m_Stream.Position = value;
                }
            }
            else if (m_Stream is not null)
            {
                throw new ObjectDisposedException(null);
            }
        }
    }
}