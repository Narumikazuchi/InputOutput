namespace Narumikazuchi.InputOutput;

public partial struct WriteableNonContinousStreamWrapper : IDisposable
{
    /// <inheritdoc/>
    public void Dispose()
    {
        m_Stream.Dispose();
    }
}