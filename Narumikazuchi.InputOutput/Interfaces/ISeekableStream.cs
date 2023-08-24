namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="IStream"/> which supports seeking.
/// </summary>
public interface ISeekableStream :
    INonContinousStream
{
    /// <summary>
    /// Sets the <see cref="INonContinousStream.Position"/> in the current <see cref="ISeekableStream"/> depending
    /// on the <paramref name="offset"/> and <paramref name="origin"/>.
    /// </summary>
    /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
    /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new <see cref="INonContinousStream.Position"/>.</param>
    /// <returns>The new <see cref="INonContinousStream.Position"/> within the current <see cref="ISeekableStream"/>.</returns>
    public Int64 Seek(Int64 offset,
                      SeekOrigin origin);
}