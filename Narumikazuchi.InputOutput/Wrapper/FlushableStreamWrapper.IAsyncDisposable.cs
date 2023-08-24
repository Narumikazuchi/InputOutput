#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
namespace Narumikazuchi.InputOutput;

public partial struct FlushableStreamWrapper : IAsyncDisposable
{
    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        return m_Stream.DisposeAsync();
    }
}
#endif