namespace Narumikazuchi.InputOutput;

public partial struct ReadableStreamWrapper : IReadableStream
{
    /// <inheritdoc/>
    public void Close()
    {
        if (m_Stream is not null)
        {
            m_Stream.Close();
        }

        this.Dispose();
    }

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
    public ValueTask CopyToAsynchronously<TStream>([DisallowNull] TStream destination,
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
                return this.InternalCopyToAsynchronously(destination: destination,
                                                bufferSize: bufferSize,
                                                cancellationToken: cancellationToken);
            }
        }
    }
#else
    public Task CopyToAsynchronously<TStream>(TStream destination,
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
                return this.InternalCopyToAsynchronously(destination: destination,
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
    public ValueTask<Int32> ReadAsynchronously(Memory<Byte> buffer,
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
    public Task<Int32> ReadAsynchronously(Byte[] buffer,
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