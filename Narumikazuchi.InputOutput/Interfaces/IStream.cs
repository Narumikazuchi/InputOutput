namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents a basic stream of data in the form of <see cref="Byte"/>[].
/// </summary>
public interface IStream :
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    IAsyncDisposable,
#endif
    IDisposable
{
    /// <summary>
    /// Closes the <see cref="IStream"/> and ensures that any managed or unmanaged resources will be released.
    /// </summary>
    public void Close();
}