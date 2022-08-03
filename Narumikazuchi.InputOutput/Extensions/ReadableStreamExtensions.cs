namespace Narumikazuchi.InputOutput;

/// <summary>
/// Contains extensions for <see cref="IReadableStream"/> objects, to reduce the clutter of methods
/// that need to be implemented, yet still give the consumer more options to use a reduced signature.
/// </summary>
public static class ReadableStreamExtensions
{
    /// <summary>
    /// Reads the bytes from the current <see cref="IReadableStream"/> and writes them
    /// to the destination <typeparamref name="TStream"/>.
    /// </summary>
    /// <remarks>
    /// Be aware that the copying starts at the <see cref="IContainedStream.Position"/>
    /// of the cursor and continues until the end of the <see cref="IReadableStream"/>.
    /// This also means that the cursor will be at the end of the <see cref="IReadableStream"/>,
    /// once the operation finishes.
    /// </remarks>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="destination">The <see cref="IWriteableStream"/> to write to.</param>
    /// <exception cref="ArgumentNullException"/>
    public static void CopyTo<TStream>(this IReadableStream stream,
                                       [DisallowNull] TStream destination)
        where TStream : IWriteableStream =>
            stream.CopyTo(destination: destination,
                          bufferSize: DEFAULT_BUFFER_SIZE);

    /// <summary>
    /// Reads the bytes from the current <see cref="IReadableStream"/> and writes them
    /// to the destination <typeparamref name="TStream"/> asynchronosly.
    /// </summary>
    /// <remarks>
    /// Be aware that the copying starts at the <see cref="IContainedStream.Position"/>
    /// of the cursor and continues until the end of the <see cref="IReadableStream"/>.
    /// This also means that the cursor will be at the end of the <see cref="IReadableStream"/>,
    /// once the operation finishes.
    /// </remarks>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="destination">The <see cref="IWriteableStream"/> to write to.</param>
    /// <exception cref="ArgumentNullException"/>
    public static ValueTask CopyToAsync<TStream>(this IReadableStream stream,
                                                [DisallowNull] TStream destination)
        where TStream : IWriteableStream =>
            stream.CopyToAsync(destination: destination,
                               bufferSize: DEFAULT_BUFFER_SIZE,
                               cancellationToken: CancellationToken.None);
    /// <summary>
    /// Reads the bytes from the current <see cref="IReadableStream"/> and writes them
    /// to the destination <typeparamref name="TStream"/> asynchronosly.
    /// </summary>
    /// <remarks>
    /// Be aware that the copying starts at the <see cref="IContainedStream.Position"/>
    /// of the cursor and continues until the end of the <see cref="IReadableStream"/>.
    /// This also means that the cursor will be at the end of the <see cref="IReadableStream"/>,
    /// once the operation finishes.
    /// </remarks>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="destination">The <see cref="IWriteableStream"/> to write to.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <exception cref="ArgumentNullException"/>
    public static ValueTask CopyToAsync<TStream>(this IReadableStream stream,
                                                [DisallowNull] TStream destination,
                                                CancellationToken cancellationToken)
        where TStream : IWriteableStream =>
            stream.CopyToAsync(destination: destination,
                               bufferSize: DEFAULT_BUFFER_SIZE,
                               cancellationToken: cancellationToken);
    /// <summary>
    /// Reads the bytes from the current <see cref="IReadableStream"/> and writes them
    /// to the destination <typeparamref name="TStream"/> asynchronosly.
    /// </summary>
    /// <remarks>
    /// Be aware that the copying starts at the <see cref="IContainedStream.Position"/>
    /// of the cursor and continues until the end of the <see cref="IReadableStream"/>.
    /// This also means that the cursor will be at the end of the <see cref="IReadableStream"/>,
    /// once the operation finishes.
    /// </remarks>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="destination">The <see cref="IWriteableStream"/> to write to.</param>
    /// <param name="bufferSize">The size of the individual chunks that should be copied at once.</param>
    /// <exception cref="ArgumentNullException"/>
    public static ValueTask CopyToAsync<TStream>(this IReadableStream stream,
                                                [DisallowNull] TStream destination,
                                                Int32 bufferSize)
        where TStream : IWriteableStream =>
            stream.CopyToAsync(destination: destination,
                               bufferSize: bufferSize,
                               cancellationToken: CancellationToken.None);

    /// <summary>
    /// Reads a sequence of bytes from the current <see cref="IReadableStream"/> and advances the <see cref="IContainedStream.Position"/>
    /// within the <see cref="IReadableStream"/> by the number of bytes read.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">
    /// An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and 
    /// (offset + count - 1) replaced by the bytes read from the current source.
    /// </param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current <see cref="IReadableStream"/>.</param>
    /// <param name="count">The maximum number of bytes to be read from the current <see cref="IReadableStream"/>.</param>
    /// <returns>
    /// The total number of bytes read into the <paramref name="buffer"/>. This can be less than the number of bytes allocated in the 
    /// <paramref name="buffer"/> if that many bytes are not currently available, or zero (0) if the end of the <see cref="IReadableStream"/> has been reached.
    /// </returns>
    public static Int32 Read(this IReadableStream stream,
                             [DisallowNull] Byte[] buffer,
                             Int32 offset,
                             Int32 count) =>
        stream.Read(buffer: buffer.AsSpan()[offset..(offset + count)]);

    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current <see cref="IReadableStream"/>, advances the <see cref="IContainedStream.Position"/> 
    /// within the <see cref="IReadableStream"/> by the number of bytes read, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write the data into.</param>
    public static ValueTask<Int32> ReadAsync(this IReadableStream stream,
                                             Memory<Byte> buffer) =>
        stream.ReadAsync(buffer: buffer,
                         cancellationToken: CancellationToken.None);
    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current <see cref="IReadableStream"/>, advances the <see cref="IContainedStream.Position"/> 
    /// within the <see cref="IReadableStream"/> by the number of bytes read, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write the data into.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current <see cref="IReadableStream"/>.</param>
    /// <param name="count">The maximum number of bytes to be read from the current <see cref="IReadableStream"/>.</param>
    public static ValueTask<Int32> ReadAsync(this IReadableStream stream,
                                             [DisallowNull] Byte[] buffer,
                                             Int32 offset,
                                             Int32 count) =>
        stream.ReadAsync(buffer: buffer.AsMemory()[offset..(offset + count)],
                         cancellationToken: CancellationToken.None);
    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current <see cref="IReadableStream"/>, advances the <see cref="IContainedStream.Position"/> 
    /// within the <see cref="IReadableStream"/> by the number of bytes read, and monitors cancellation requests.
    /// </summary>
    /// <param name="stream">The stream that will execute the method.</param>
    /// <param name="buffer">The region of memory to write the data into.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current <see cref="IReadableStream"/>.</param>
    /// <param name="count">The maximum number of bytes to be read from the current <see cref="IReadableStream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    public static ValueTask<Int32> ReadAsync(this IReadableStream stream,
                                             [DisallowNull] Byte[] buffer,
                                             Int32 offset,
                                             Int32 count,
                                             CancellationToken cancellationToken) =>
        stream.ReadAsync(buffer: buffer.AsMemory()[offset..(offset + count)],
                         cancellationToken: cancellationToken);

    private const Int32 DEFAULT_BUFFER_SIZE = 81920;
}