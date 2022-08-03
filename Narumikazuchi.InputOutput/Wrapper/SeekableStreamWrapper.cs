namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can utilize seeking (<see cref="Stream.CanSeek"/>) into the
/// <see cref="ISeekableStream"/> interface.
/// </summary>
public readonly partial struct SeekableStreamWrapper
{
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
            throw new ArgumentException("can't seek");
        }
        else
        {
            m_Stream = stream;
        }
    }

#pragma warning disable CS1591 // XML Comment
    public static implicit operator SeekableStreamWrapper(Stream stream)
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
partial struct SeekableStreamWrapper
{
    internal readonly Stream m_Stream = Stream.Null;
}

// IAsyncDisposable
partial struct SeekableStreamWrapper : IAsyncDisposable
{
    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await m_Stream.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}

// IDisposable
partial struct SeekableStreamWrapper : IDisposable
{
    /// <inheritdoc/>
    public void Dispose()
    {
        m_Stream.Dispose();
        GC.SuppressFinalize(this);
    }
}

// ISeekableStream
partial struct SeekableStreamWrapper : ISeekableStream
{
    /// <inheritdoc/>
    public void Close() =>
        this.Dispose();

    /// <inheritdoc/>
    public Int64 Seek(Int64 offset,
                      SeekOrigin origin) =>
        m_Stream.Seek(offset: offset,
                      origin: origin);

    /// <inheritdoc/>
    public Int64 Length
    {
        get
        {
            if (m_Stream.CanSeek)
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
            if (m_Stream.CanSeek)
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
            if (m_Stream.CanSeek)
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
            else
            {
                throw new ObjectDisposedException(null);
            }
        }
    }
}