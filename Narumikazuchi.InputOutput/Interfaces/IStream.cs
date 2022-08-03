namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents a basic stream of data in the form of <see cref="Byte"/>[].
/// </summary>
public interface IStream :
    IAsyncDisposable,
    IDisposable
{
    /// <summary>
    /// Closes the <see cref="IStream"/> and ensures that any managed or unmanaged resources will be released.
    /// </summary>
    public void Close();
}