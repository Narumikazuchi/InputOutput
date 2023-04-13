namespace Narumikazuchi.InputOutput;

public partial struct WriteableNonContinousStreamWrapper : IWriteableNonContinousStream
{
    /// <inheritdoc/>
    public void Close()
    {
        m_Stream.Close();
        this.Dispose();
    }

#if NET5_0_OR_GREATER
    /// <inheritdoc/>
    public void Write(ReadOnlySpan<Byte> buffer)
    {
        m_Stream.Write(buffer: buffer);
    }
#else
    /// <inheritdoc/>
    public void Write(Byte[] buffer,
                      Int32 offset,
                      Int32 count)
    {
        m_Stream.Write(buffer: buffer,
                       offset: offset,
                       count: count);
    }
#endif

#if NET5_0_OR_GREATER
    /// <inheritdoc/>
    public ValueTask WriteAsynchronously(ReadOnlyMemory<Byte> buffer,
                                         CancellationToken cancellationToken)
    {
        return m_Stream.WriteAsynchronously(buffer: buffer,
                                            cancellationToken: cancellationToken);
    }
#else
    /// <inheritdoc/>
    public Task WriteAsynchronously(Byte[] buffer,
                                    Int32 offset,
                                    Int32 count,
                                    CancellationToken cancellationToken)
    {
        return m_Stream.WriteAsynchronously(buffer: buffer,
                                            offset: offset,
                                            count: count,
                                            cancellationToken: cancellationToken);
    }
#endif

    /// <inheritdoc/>
    public void WriteByte(Byte value)
    {
        m_Stream.WriteByte(value);
    }

    /// <inheritdoc/>
    public void SetLength(Int64 length)
    {
        m_Stream.UnderlyingStream.SetLength(length);
    }

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
                throw new ObjectDisposedException(nameof(WriteableNonContinousStreamWrapper));
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
                throw new ObjectDisposedException(nameof(WriteableNonContinousStreamWrapper));
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
                throw new ObjectDisposedException(nameof(WriteableNonContinousStreamWrapper));
            }
        }
    }
}