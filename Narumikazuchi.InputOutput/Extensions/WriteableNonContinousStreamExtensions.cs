namespace Narumikazuchi.InputOutput;

#if NET5_0_OR_GREATER
/// <summary>
/// Contains extensions for <see cref="IWriteableNonContinousStream"/> objects, to reduce the clutter of methods
/// that need to be implemented, yet still give the consumer more options to use a reduced signature.
/// </summary>
public static class WriteableNonContinousStreamExtensions
{
    /// <summary>
    /// Writes a sequence of bytes to the current <see cref="IWriteableNonContinousStream"/> and advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableNonContinousStream"/> by the number of bytes written.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current <see cref="IWriteableNonContinousStream"/>.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current <see cref="IWriteableNonContinousStream"/>.</param>
    /// <param name="count">The number of bytes to be written to the current <see cref="IWriteableNonContinousStream"/>.</param>
    public static void Write(this IWriteableNonContinousStream stream,
                             [DisallowNull] Byte[] buffer,
                             Int32 offset,
                             Int32 count) =>
        stream.Write(buffer: buffer.AsSpan()[offset..(offset + count)]);

    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current <see cref="IWriteableNonContinousStream"/>, advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableNonContinousStream"/> by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write data from.</param>
    public static ValueTask WriteAsync(this IWriteableNonContinousStream stream,
                                       ReadOnlyMemory<Byte> buffer) =>
        stream.WriteAsync(buffer: buffer,
                          cancellationToken: CancellationToken.None);
    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current <see cref="IWriteableNonContinousStream"/>, advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableNonContinousStream"/> by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write data from.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current <see cref="IWriteableNonContinousStream"/>.</param>
    /// <param name="count">The number of bytes to be written to the current <see cref="IWriteableNonContinousStream"/>.</param>
    public static ValueTask WriteAsync(this IWriteableNonContinousStream stream,
                                       [DisallowNull] Byte[] buffer,
                                       Int32 offset,
                                       Int32 count) =>
        stream.WriteAsync(buffer: buffer.AsMemory()[offset..(offset + count)],
                          cancellationToken: CancellationToken.None);
    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current <see cref="IWriteableNonContinousStream"/>, advances the current <see cref="INonContinousStream.Position"/>
    /// within this <see cref="IWriteableNonContinousStream"/> by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write data from.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current <see cref="IWriteableNonContinousStream"/>.</param>
    /// <param name="count">The number of bytes to be written to the current <see cref="IWriteableNonContinousStream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    public static ValueTask WriteAsync(this IWriteableNonContinousStream stream,
                                       [DisallowNull] Byte[] buffer,
                                       Int32 offset,
                                       Int32 count,
                                       CancellationToken cancellationToken) =>
        stream.WriteAsync(buffer: buffer.AsMemory()[offset..(offset + count)],
                          cancellationToken: cancellationToken);
}
#endif