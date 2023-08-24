namespace Narumikazuchi.InputOutput;

public partial struct TimeoutStreamWrapper : ITimeoutStream
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
    public Int32 ReadTimeout
    {
        get
        {
            if (m_Stream is null)
            {
                return -1;
            }
            else
            {
                return m_Stream.ReadTimeout;
            }
        }
        set
        {
            if (m_Stream is not null)
            {
                m_Stream.ReadTimeout = value;
            }
        }
    }

    /// <inheritdoc/>
    public Int32 WriteTimeout
    {
        get
        {
            if (m_Stream is null)
            {
                return -1;
            }
            else
            {
                return m_Stream.WriteTimeout;
            }
        }
        set
        {
            if (m_Stream is not null)
            {
                m_Stream.WriteTimeout = value;
            }
        }
    }
}