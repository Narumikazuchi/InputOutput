namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can timeout (<see cref="Stream.CanTimeout"/>) into the
/// <see cref="ITimeoutStream"/> interface.
/// </summary>
public readonly partial struct TimeoutStreamWrapper
{
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
            throw new ArgumentException("can't timeout");
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
    public static implicit operator TimeoutStreamWrapper(Stream stream)
    {
        if (!stream.CanTimeout)
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
partial struct TimeoutStreamWrapper
{
    internal readonly Stream m_Stream = Stream.Null;
}

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
// IAsyncDisposable
partial struct TimeoutStreamWrapper : IAsyncDisposable
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
#endif

// IDisposable
partial struct TimeoutStreamWrapper : IDisposable
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

// ITimeoutStream
partial struct TimeoutStreamWrapper : ITimeoutStream
{
    /// <inheritdoc/>
    public void Close() =>
        this.Dispose();

    /// <inheritdoc/>
    public Int32 ReadTimeout
    {
        get
        {
            if (m_Stream is null)
            {
                return -1;
            }
            else
            {
                return m_Stream.ReadTimeout;
            }
        }
        set
        {
            if (m_Stream is not null)
            {
                m_Stream.ReadTimeout = value;
            }
        }
    }

    /// <inheritdoc/>
    public Int32 WriteTimeout
    {
        get
        {
            if (m_Stream is null)
            {
                return -1;
            }
            else
            {
                return m_Stream.WriteTimeout;
            }
        }
        set
        {
            if (m_Stream is not null)
            {
                m_Stream.WriteTimeout = value;
            }
        }
    }
}