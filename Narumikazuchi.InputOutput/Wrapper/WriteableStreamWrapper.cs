namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can be writen to (<see cref="Stream.CanWrite"/>) into the
/// <see cref="IWriteableStream"/> interface.
/// </summary>
public readonly partial struct WriteableStreamWrapper
{
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
            throw new ArgumentException("can't write");
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
    public static implicit operator WriteableStreamWrapper(Stream stream)
    {
        if (!stream.CanWrite)
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
partial struct WriteableStreamWrapper
{
    internal readonly Stream m_Stream = Stream.Null;
}

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
// IAsyncDisposable
partial struct WriteableStreamWrapper : IAsyncDisposable
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
partial struct WriteableStreamWrapper : IDisposable
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

// IWriteableStream
partial struct WriteableStreamWrapper : IWriteableStream
{
    /// <inheritdoc/>
    public void Close() =>
        this.Dispose();

    /// <inheritdoc/>
    public void Flush()
    {
        if (m_Stream is not null)
        {
            m_Stream.Flush();
        }
    }

    /// <inheritdoc/>
#if NET5_0_OR_GREATER
    public async ValueTask FlushAsync()
    {
        if (m_Stream is not null)
        {
            await m_Stream.FlushAsync();
        }
    }
#else
    public async Task FlushAsync()
    {
        if (m_Stream is not null)
        {
            await m_Stream.FlushAsync();
        }
    }
#endif

#if NET5_0_OR_GREATER
    /// <inheritdoc/>
    public void Write(ReadOnlySpan<Byte> buffer)
    {
        if (m_Stream is not null)
        {
            m_Stream.Write(buffer: buffer);
        }
    }
#else
    /// <inheritdoc/>
    public void Write(Byte[] buffer,
                      Int32 offset,
                      Int32 count)
    {
        if (m_Stream is not null)
        {
            m_Stream.Write(buffer: buffer,
                           offset: offset,
                           count: count);
        }
    }
#endif

#if NET5_0_OR_GREATER
    /// <inheritdoc/>
    public ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer,
                                CancellationToken cancellationToken)
    {
        if (m_Stream is null)
        {
            return ValueTask.CompletedTask;
        }
        else
        {
            return m_Stream.WriteAsync(buffer: buffer,
                                       cancellationToken: cancellationToken);
        }
    }
#else
    /// <inheritdoc/>
    public Task WriteAsync(Byte[] buffer,
                           Int32 offset,
                           Int32 count,
                           CancellationToken cancellationToken)
    {
        if (m_Stream is null)
        {
            return Task.CompletedTask;
        }
        else
        {
            return m_Stream.WriteAsync(buffer: buffer,
                                       offset: offset,
                                       count: count,
                                       cancellationToken: cancellationToken);
        }
    }
#endif

    /// <inheritdoc/>
    public void WriteByte(Byte value)
    {
        if (m_Stream is not null)
        {
            m_Stream.WriteByte(value);
        }
    }
}