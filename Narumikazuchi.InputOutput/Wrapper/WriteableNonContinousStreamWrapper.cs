namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can be writen to (<see cref="Stream.CanWrite"/>) and has a known length into the
/// <see cref="IWriteableNonContinousStream"/> interface.
/// </summary>
public readonly partial struct WriteableNonContinousStreamWrapper
{
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
            throw new ArgumentException("can't seek position");
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
        m_Stream.UnderlyingStream;

#pragma warning disable CS1591 // XML Comment
    public static implicit operator WriteableNonContinousStreamWrapper(Stream stream)
    {
        if (!stream.CanWrite)
        {
            throw new ArgumentException("", nameof(stream));
        }
        else if (!stream.CanSeek)
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
partial struct WriteableNonContinousStreamWrapper
{
    internal readonly WriteableStreamWrapper m_Stream = Stream.Null;
}

// IAsyncDisposable
partial struct WriteableNonContinousStreamWrapper : IAsyncDisposable
{
    /// <inheritdoc/>
    public ValueTask DisposeAsync() =>
        m_Stream.DisposeAsync();
}

// IDisposable
partial struct WriteableNonContinousStreamWrapper : IDisposable
{
    /// <inheritdoc/>
    public void Dispose() =>
        m_Stream.Dispose();
}

// IWriteableNonContinousStream
partial struct WriteableNonContinousStreamWrapper : IWriteableNonContinousStream
{
    /// <inheritdoc/>
    public void Close() =>
        this.Dispose();

    /// <inheritdoc/>
    public void Flush() =>
        m_Stream.Flush();

    /// <inheritdoc/>
    public ValueTask FlushAsync() =>
        m_Stream.FlushAsync();

    /// <inheritdoc/>
    public void Write(ReadOnlySpan<Byte> buffer) =>
        m_Stream.Write(buffer: buffer);

    /// <inheritdoc/>
    public ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer,
                                CancellationToken cancellationToken) =>
        m_Stream.WriteAsync(buffer: buffer,
                            cancellationToken: cancellationToken);

    /// <inheritdoc/>
    public void WriteByte(Byte value) =>
        m_Stream.WriteByte(value);

    /// <inheritdoc/>
    public void SetLength(Int64 length) =>
        m_Stream.UnderlyingStream.SetLength(length);

    /// <inheritdoc/>
    public Int64 Length
    {
        get
        {
            if (m_Stream.UnderlyingStream is null)
            {
                return 0;
            }
            else if (m_Stream.UnderlyingStream.CanSeek)
            {
                return m_Stream.UnderlyingStream.Length;
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
            if (m_Stream.UnderlyingStream is null)
            {
                return 0;
            }
            else if (m_Stream.UnderlyingStream.CanSeek)
            {
                return m_Stream.UnderlyingStream.Position;
            }
            else
            {
                throw new ObjectDisposedException(null);
            }
        }
        set
        {
            if (m_Stream.UnderlyingStream is not null &&
                m_Stream.UnderlyingStream.CanSeek)
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                else
                {
                    m_Stream.UnderlyingStream.Position = value;
                }
            }
            else if (m_Stream.UnderlyingStream is not null)
            {
                throw new ObjectDisposedException(null);
            }
        }
    }
}