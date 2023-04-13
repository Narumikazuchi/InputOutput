namespace Narumikazuchi.InputOutput;

public partial struct WriteableStreamWrapper : IWriteableStream
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
    public ValueTask WriteAsynchronously(ReadOnlyMemory<Byte> buffer,
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
    public Task WriteAsynchronously(Byte[] buffer,
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