namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="INonContinousStream"/> that can be written to.
/// </summary>
public interface IWriteableNonContinousStream :
    IWriteableStream
{
    /// <summary>
    /// Sets the <see cref="INonContinousStream.Length"/> of the current <see cref="IWriteableStream"/>.
    /// </summary>
    /// <param name="length">The desired length of the current <see cref="IWriteableStream"/> in bytes.</param>
    public void SetLength(Int64 length);
}