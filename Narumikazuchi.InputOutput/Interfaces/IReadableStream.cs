namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="IStream"/> that can be read from.
/// </summary>
public interface IReadableStream :
    IStream
{
    /// <summary>
    /// Reads the bytes from the current <see cref="IReadableStream"/> and writes them
    /// to the destination <typeparamref name="TStream"/>.
    /// </summary>
    /// <remarks>
    /// Be aware that if the <see cref="IReadableStream"/> implements <see cref="INonContinousStream"/>, then
    /// the copying starts at the <see cref="INonContinousStream.Position"/>
    /// of the cursor and continues until the end of the <see cref="IReadableStream"/>.
    /// This also means that the cursor will be at the end of the <see cref="IReadableStream"/>,
    /// once the operation finishes.
    /// </remarks>
    /// <param name="destination">The <see cref="IWriteableStream"/> to write to.</param>
    /// <param name="bufferSize">The size of the individual chunks that should be copied at once.</param>
    /// <exception cref="ArgumentNullException"/>
    public void CopyTo<TStream>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        TStream destination,
        Int32 bufferSize)
            where TStream : IWriteableStream;

    /// <summary>
    /// Reads the bytes from the current <see cref="IReadableStream"/> and writes them
    /// to the destination <typeparamref name="TStream"/> asynchronosly.
    /// </summary>
    /// <remarks>
    /// Be aware that if the <see cref="IReadableStream"/> implements <see cref="INonContinousStream"/>, then
    /// the copying starts at the <see cref="INonContinousStream.Position"/>
    /// of the cursor and continues until the end of the <see cref="IReadableStream"/>.
    /// This also means that the cursor will be at the end of the <see cref="IReadableStream"/>,
    /// once the operation finishes.
    /// </remarks>
    /// <param name="destination">The <see cref="IWriteableStream"/> to write to.</param>
    /// <param name="bufferSize">The size of the individual chunks that should be copied at once.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET5_0_OR_GREATER
    public ValueTask CopyToAsynchronously<TStream>([DisallowNull] TStream destination,
                                                   Int32 bufferSize,
                                                   CancellationToken cancellationToken)
        where TStream : IWriteableStream;
#else
    public Task CopyToAsynchronously<TStream>(TStream destination,
                                              Int32 bufferSize,
                                              CancellationToken cancellationToken)
        where TStream : IWriteableStream;
#endif

#if NET5_0_OR_GREATER
    /// <summary>
    /// Reads a sequence of bytes from the current <see cref="IReadableStream"/> and if the <see cref="IReadableStream"/>
    /// implements <see cref="INonContinousStream"/>,then advances the <see cref="INonContinousStream.Position"/>
    /// within the <see cref="IReadableStream"/> by the number of bytes read.
    /// </summary>
    /// <param name="buffer">
    /// A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current source.
    /// </param>
    /// <returns>
    /// The total number of bytes read into the <paramref name="buffer"/>. This can be less than the number of bytes allocated in the 
    /// <paramref name="buffer"/> if that many bytes are not currently available, or zero (0) if the end of the <see cref="IReadableStream"/> has been reached.
    /// </returns>
    public Int32 Read(Span<Byte> buffer);
#else
    /// <summary>
    /// Reads a sequence of bytes from the current <see cref="IReadableStream"/> and if the <see cref="IReadableStream"/>
    /// implements <see cref="INonContinousStream"/>,then advances the <see cref="INonContinousStream.Position"/>
    /// within the <see cref="IReadableStream"/> by the number of bytes read.
    /// </summary>
    /// <param name="buffer">
    /// A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current source.
    /// </param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current <see cref="IReadableStream"/>.</param>
    /// <param name="count">The maximum number of bytes to be read from the current <see cref="IReadableStream"/>.</param>
    /// <returns>
    /// The total number of bytes read into the <paramref name="buffer"/>. This can be less than the number of bytes allocated in the 
    /// <paramref name="buffer"/> if that many bytes are not currently available, or zero (0) if the end of the <see cref="IReadableStream"/> has been reached.
    /// </returns>
    public Int32 Read(Byte[] buffer,
                      Int32 offset,
                      Int32 count);
#endif

#if NET5_0_OR_GREATER
    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current <see cref="IReadableStream"/> and if the <see cref="IReadableStream"/>
    /// implements <see cref="INonContinousStream"/>, then advances the <see cref="INonContinousStream.Position"/> 
    /// within the <see cref="IReadableStream"/> by the number of bytes read. Monitors cancellation requests.
    /// </summary>
    /// <param name="buffer">The region of memory to write the data into.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    public ValueTask<Int32> ReadAsynchronously(Memory<Byte> buffer,
                                               CancellationToken cancellationToken);
#else
    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current <see cref="IReadableStream"/> and if the <see cref="IReadableStream"/>
    /// implements <see cref="INonContinousStream"/>, then advances the <see cref="INonContinousStream.Position"/> 
    /// within the <see cref="IReadableStream"/> by the number of bytes read. Monitors cancellation requests.
    /// </summary>
    /// <param name="buffer">The region of memory to write the data into.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current <see cref="IReadableStream"/>.</param>
    /// <param name="count">The maximum number of bytes to be read from the current <see cref="IReadableStream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    public Task<Int32> ReadAsynchronously(Byte[] buffer,
                                          Int32 offset,
                                          Int32 count,
                                          CancellationToken cancellationToken);
#endif

    /// <summary>
    /// Reads a byte from the <see cref="IReadableStream"/> and if the <see cref="IReadableStream"/> implements 
    /// <see cref="INonContinousStream"/>, then advances the <see cref="INonContinousStream.Position"/> within the stream by one byte.
    /// </summary>
    /// <param name="byte">The next unsigned byte in the <see cref="IReadableStream"/>.</param>
    /// <returns><see langword="true"/> if there is a next byte; otherwise, <see langword="false"/>.</returns>
    public Boolean ReadByte(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [NotNullWhen(true)]
#endif
        out Byte? @byte);
}