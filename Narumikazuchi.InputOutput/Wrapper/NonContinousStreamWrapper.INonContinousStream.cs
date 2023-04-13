namespace Narumikazuchi.InputOutput;

public partial struct NonContinousStreamWrapper : INonContinousStream
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
                throw new ObjectDisposedException(nameof(NonContinousStreamWrapper));
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
                throw new ObjectDisposedException(nameof(NonContinousStreamWrapper));
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
                throw new ObjectDisposedException(nameof(NonContinousStreamWrapper));
            }
        }
    }
}