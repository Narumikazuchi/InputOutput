namespace Narumikazuchi.InputOutput;

public partial struct TimeoutStreamWrapper : IDisposable
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