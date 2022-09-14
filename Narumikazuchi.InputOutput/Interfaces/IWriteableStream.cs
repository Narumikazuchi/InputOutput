namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="IStream"/> that can be written to.
/// </summary>
public interface IWriteableStream :
    IStream
{
    /// <summary>
    /// Clears all buffers for this <see cref="IWriteableStream"/> and causes any buffered data to be written to the underlying device.
    /// </summary>
    public void Flush();

    /// <summary>
    /// Asynchronously clears all buffers for this <see cref="IWriteableStream"/> and causes any buffered data to be written to the underlying device.
    /// </summary>
    public ValueTask FlushAsync();

    /// <summary>
    /// Writes a sequence of bytes to the current <see cref="IWriteableStream"/> and advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableStream"/> by the number of bytes written.
    /// </summary>
    /// <param name="buffer">A region of memory. This method copies the contents of this region to the current <see cref="IWriteableStream"/>.</param>
    public void Write(ReadOnlySpan<Byte> buffer);

    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current <see cref="IWriteableStream"/>, advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableStream"/> by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="buffer">The region of memory to write data from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    public ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer,
                                CancellationToken cancellationToken);

    /// <summary>
    /// Writes a byte to <see cref="IWriteableStream"/> and  if the <see cref="IWriteableStream"/> implements
    /// <see cref="INonContinousStream"/>, then advances the <see cref="INonContinousStream.Position"/> 
    /// within the <see cref="IWriteableStream"/> by one byte.
    /// </summary>
    /// <param name="value">The byte to write to the <see cref="IWriteableStream"/>.</param>
    public void WriteByte(Byte value);
}