namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can be read from (<see cref="Stream.CanRead"/>) into the
/// <see cref="IReadableStream"/> interface.
/// </summary>
public readonly partial struct ReadableStreamWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReadableStreamWrapper"/> struct.
    /// </summary>
    public ReadableStreamWrapper()
    {
        m_Stream = Stream.Null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadableStreamWrapper"/> struct.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <exception cref="ArgumentException"/>
    public ReadableStreamWrapper(Stream stream)
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
    public static implicit operator ReadableStreamWrapper(Stream stream)
    {
        if (!stream.CanRead)
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
partial struct ReadableStreamWrapper
{
#if NET5_0_OR_GREATER
    private async ValueTask InternalCopyToAsync<TStream>(TStream destination,
                                                         Int32 bufferSize,
                                                         CancellationToken cancellationToken)
        where TStream : IWriteableStream
    {
        Byte[] buffer = new Byte[bufferSize];
        Int32 read = await m_Stream.ReadAsync(buffer: buffer,
                                              cancellationToken: cancellationToken);
        while (read != 0)
        {
            await destination.WriteAsync(buffer: buffer.AsMemory()[..read],
                                         cancellationToken: cancellationToken);
            read = await m_Stream.ReadAsync(buffer: buffer,
                                            cancellationToken: cancellationToken);
        }
    }
#else
    private async Task InternalCopyToAsync<TStream>(TStream destination,
                                                    Int32 bufferSize,
                                                    CancellationToken cancellationToken)
        where TStream : IWriteableStream
    {
        Byte[] buffer = new Byte[bufferSize];
        Int32 read = await m_Stream.ReadAsync(buffer: buffer,
                                              offset: 0,
                                              count: bufferSize,
                                              cancellationToken: cancellationToken);
        while (read != 0)
        {
            await destination.WriteAsync(buffer: buffer,
                                         offset: 0,
                                         count: read,
                                         cancellationToken: cancellationToken);
            read = await m_Stream.ReadAsync(buffer: buffer,
                                            offset: 0,
                                            count: bufferSize,
                                            cancellationToken: cancellationToken);
        }
    }
#endif

    internal readonly Stream m_Stream = Stream.Null;
}

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
// IAsyncDisposable
partial struct ReadableStreamWrapper : IAsyncDisposable
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
partial struct ReadableStreamWrapper : IDisposable
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

// IReadableStream
partial struct ReadableStreamWrapper : IReadableStream
{
    /// <inheritdoc/>
    public void Close() =>
        this.Dispose();

    /// <inheritdoc/>
    public void CopyTo<TStream>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        TStream destination,
        Int32 bufferSize)
        where TStream : IWriteableStream
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(destination);
#else
        if (destination is null)
        {
            throw new ArgumentNullException(nameof(destination));
        }
#endif

        if (m_Stream is not null)
        {
            Byte[] buffer = new Byte[bufferSize];
            Int32 read = m_Stream.Read(buffer: buffer,
                                       offset: 0,
                                       count: bufferSize);
            while (read != 0)
            {
                destination.Write(buffer: buffer,
                                  offset: 0,
                                  count: read);
                read = m_Stream.Read(buffer: buffer,
                                     offset: 0,
                                     count: bufferSize);
            }
        }
    }

    /// <inheritdoc/>
#if NET5_0_OR_GREATER
    public ValueTask CopyToAsync<TStream>([DisallowNull] TStream destination,
                                          Int32 bufferSize,
                                          CancellationToken cancellationToken)
        where TStream : IWriteableStream
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(destination);
#else
        if (destination is null)
        {
            throw new ArgumentNullException(nameof(destination));
        }
#endif

        if (m_Stream is null)
        {
            return ValueTask.CompletedTask;
        }
        else
        {
            if (m_Stream is MemoryStream memoryStream &&
                destination is WriteableStreamWrapper wrapper &&
                wrapper.m_Stream is MemoryStream destinationStream)
            {
                ReadOnlySpan<Byte> buffer = memoryStream.ToArray();

                destinationStream.Write(buffer);
                return ValueTask.CompletedTask;
            }
            else
            {
                return this.InternalCopyToAsync(destination: destination,
                                                bufferSize: bufferSize,
                                                cancellationToken: cancellationToken);
            }
        }
    }
#else
    public Task CopyToAsync<TStream>(TStream destination,
                                     Int32 bufferSize,
                                     CancellationToken cancellationToken)
        where TStream : IWriteableStream
    {
        if (destination is null)
        {
            throw new ArgumentNullException(nameof(destination));
        }

        if (m_Stream is null)
        {
            return Task.CompletedTask;
        }
        else
        {
            if (m_Stream is MemoryStream memoryStream &&
                destination is WriteableStreamWrapper wrapper &&
                wrapper.m_Stream is MemoryStream destinationStream)
            {
                Byte[] buffer = memoryStream.ToArray();

                destinationStream.Write(buffer: buffer,
                                        offset: 0,
                                        count: buffer.Length);
                return Task.CompletedTask;
            }
            else
            {
                return this.InternalCopyToAsync(destination: destination,
                                                bufferSize: bufferSize,
                                                cancellationToken: cancellationToken);
            }
        }
    }
#endif

    /// <inheritdoc/>
#if NET5_0_OR_GREATER
    public Int32 Read(Span<Byte> buffer)
    {
        if (m_Stream is null)
        {
            return 0;
        }
        else
        {
            return m_Stream.Read(buffer);
        }
    }
#else
    public Int32 Read(Byte[] buffer,
                      Int32 offset,
                      Int32 count)
    {
        if (m_Stream is null)
        {
            return 0;
        }
        else
        {
            return m_Stream.Read(buffer: buffer,
                                 offset: offset,
                                 count: count);
        }
    }
#endif

    /// <inheritdoc/>
#if NET5_0_OR_GREATER
    public ValueTask<Int32> ReadAsync(Memory<Byte> buffer,
                                      CancellationToken cancellationToken)
    {
        if (m_Stream is null)
        {
            return ValueTask.FromResult(0);
        }
        else
        {
            return m_Stream.ReadAsync(buffer: buffer,
                                      cancellationToken: cancellationToken);
        }
    }
#else
    public Task<Int32> ReadAsync(Byte[] buffer,
                                 Int32 offset,
                                 Int32 count,
                                 CancellationToken cancellationToken)
    {
        if (m_Stream is null)
        {
            return Task.FromResult(0);
        }
        else
        {
            return m_Stream.ReadAsync(buffer: buffer,
                                      offset: offset,
                                      count: count,
                                      cancellationToken: cancellationToken);
        }
    }
#endif

    /// <inheritdoc/>
    public Boolean ReadByte(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [NotNullWhen(true)]
#endif
        out Byte? value)
    {
        if (m_Stream is null)
        {
            value = default;
            return false;
        }
        else
        {
            Int32 result = m_Stream.ReadByte();
            if (result != -1)
            {
                value = (Byte)result;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }
    }
}