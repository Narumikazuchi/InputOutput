namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="IStream"/> that can be flushed.
/// </summary>
public interface IFlushableStream : 
    IWriteableStream
{
    /// <summary>
    /// Clears all buffers for this <see cref="IWriteableStream"/> and causes any buffered data to be written to the underlying device.
    /// </summary>
    public void Flush();

    /// <summary>
    /// Asynchronously clears all buffers for this <see cref="IWriteableStream"/> and causes any buffered data to be written to the underlying device.
    /// </summary>
    public Task FlushAsynchronously();
}