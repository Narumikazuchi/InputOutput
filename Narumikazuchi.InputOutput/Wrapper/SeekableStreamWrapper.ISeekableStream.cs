namespace Narumikazuchi.InputOutput;

public partial struct SeekableStreamWrapper : ISeekableStream
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
    public Int64 Seek(Int64 offset,
                      SeekOrigin origin)
    {
        if (m_Stream is null)
        {
            return 0;
        }
        else
        {
            return m_Stream.Seek(offset: offset,
                                 origin: origin);
        }
    }

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
                throw new ObjectDisposedException(nameof(SeekableStreamWrapper));
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
                throw new ObjectDisposedException(nameof(SeekableStreamWrapper));
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
                throw new ObjectDisposedException(nameof(SeekableStreamWrapper));
            }
        }
    }
}