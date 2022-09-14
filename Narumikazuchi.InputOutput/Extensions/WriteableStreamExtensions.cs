namespace Narumikazuchi.InputOutput;

/// <summary>
/// Contains extensions for <see cref="IWriteableStream"/> objects, to reduce the clutter of methods
/// that need to be implemented, yet still give the consumer more options to use a reduced signature.
/// </summary>
public static class WriteableStreamExtensions
{
    /// <summary>
    /// Writes a sequence of bytes to the current <see cref="IWriteableStream"/> and advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableStream"/> by the number of bytes written.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current <see cref="IWriteableStream"/>.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current <see cref="IWriteableStream"/>.</param>
    /// <param name="count">The number of bytes to be written to the current <see cref="IWriteableStream"/>.</param>
    public static void Write(this IWriteableStream stream,
                             [DisallowNull] Byte[] buffer,
                             Int32 offset,
                             Int32 count) =>
        stream.Write(buffer: buffer.AsSpan()[offset..(offset + count)]);

    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current <see cref="IWriteableStream"/>, advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableStream"/> by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write data from.</param>
    public static ValueTask WriteAsync(this IWriteableStream stream,
                                       ReadOnlyMemory<Byte> buffer) =>
        stream.WriteAsync(buffer: buffer,
                          cancellationToken: CancellationToken.None);
    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current <see cref="IWriteableStream"/>, advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableStream"/> by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write data from.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current <see cref="IWriteableStream"/>.</param>
    /// <param name="count">The number of bytes to be written to the current <see cref="IWriteableStream"/>.</param>
    public static ValueTask WriteAsync(this IWriteableStream stream,
                                       [DisallowNull] Byte[] buffer,
                                       Int32 offset,
                                       Int32 count) =>
        stream.WriteAsync(buffer: buffer.AsMemory()[offset..(offset + count)],
                          cancellationToken: CancellationToken.None);
    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current <see cref="IWriteableStream"/>, advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableStream"/> by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write data from.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current <see cref="IWriteableStream"/>.</param>
    /// <param name="count">The number of bytes to be written to the current <see cref="IWriteableStream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    public static ValueTask WriteAsync(this IWriteableStream stream,
                                       [DisallowNull] Byte[] buffer,
                                       Int32 offset,
                                       Int32 count,
                                       CancellationToken cancellationToken) =>
        stream.WriteAsync(buffer: buffer.AsMemory()[offset..(offset + count)],
                          cancellationToken: cancellationToken);
}