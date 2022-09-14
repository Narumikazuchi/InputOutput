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

    internal readonly Stream m_Stream = Stream.Null;
}

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
    public void CopyTo<TStream>([DisallowNull] TStream destination,
                                Int32 bufferSize)
        where TStream : IWriteableStream
    {
        ArgumentNullException.ThrowIfNull(destination);

        if (m_Stream is not null)
        {
            Byte[] buffer = new Byte[bufferSize];
            Int32 read = m_Stream.Read(buffer: buffer);
            while (read != 0)
            {
                destination.Write(buffer.AsSpan()[..read]);
                read = m_Stream.Read(buffer: buffer);
            }
        }
    }

    /// <inheritdoc/>
    public ValueTask CopyToAsync<TStream>([DisallowNull] TStream destination,
                                          Int32 bufferSize,
                                          CancellationToken cancellationToken)
        where TStream : IWriteableStream
    {
        ArgumentNullException.ThrowIfNull(destination);

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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public Boolean ReadByte([NotNullWhen(true)] out Byte? value)
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