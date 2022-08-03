namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="IStream"/> that can be written to.
/// </summary>
public interface IWriteableStream :
    IContainedStream
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
    /// Sets the <see cref="IContainedStream.Length"/> of the current <see cref="IWriteableStream"/>.
    /// </summary>
    /// <param name="length">The desired length of the current <see cref="IWriteableStream"/> in bytes.</param>
    public void SetLength(Int64 length);

    /// <summary>
    /// Writes a sequence of bytes to the current <see cref="IWriteableStream"/> and advances the current <see cref="Position"/>
    /// within this <see cref="IWriteableStream"/> by the number of bytes written.
    /// </summary>
    /// <param name="buffer">A region of memory. This method copies the contents of this region to the current <see cref="IWriteableStream"/>.</param>
    public void Write(ReadOnlySpan<Byte> buffer);

    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current <see cref="IWriteableStream"/>, advances the current <see cref="Position"/>
    /// within this <see cref="IWriteableStream"/> by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="buffer">The region of memory to write data from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    public ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer,
                                CancellationToken cancellationToken);

    /// <summary>
    /// Writes a byte to the current <see cref="Position"/> in the <see cref="IWriteableStream"/> and advances the <see cref="Position"/> 
    /// within the <see cref="IWriteableStream"/> by one byte.
    /// </summary>
    /// <param name="value">The byte to write to the <see cref="IWriteableStream"/>.</param>
    public void WriteByte(Byte value);

    /// <summary>
    /// Gets the current position of the cursor in the <see cref="IWriteableStream"/>.
    /// </summary>
    public new Int64 Position
    {
        get;
        set;
    }
}