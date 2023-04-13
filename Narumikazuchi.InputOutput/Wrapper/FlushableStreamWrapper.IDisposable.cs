namespace Narumikazuchi.InputOutput;

public partial struct FlushableStreamWrapper : IDisposable
{
    /// <inheritdoc/>
    public void Dispose()
    {
        m_Stream.Dispose();
    }
}