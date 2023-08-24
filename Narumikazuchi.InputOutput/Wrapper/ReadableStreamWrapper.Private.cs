namespace Narumikazuchi.InputOutput;

public partial struct ReadableStreamWrapper
{
#if NET5_0_OR_GREATER
    private async ValueTask InternalCopyToAsynchronously<TStream>(TStream destination,
                                                                  Int32 bufferSize,
                                                                  CancellationToken cancellationToken)
        where TStream : IWriteableStream
    {
        Byte[] buffer = new Byte[bufferSize];
        Int32 read = await m_Stream.ReadAsync(buffer: buffer,
                                              cancellationToken: cancellationToken);
        while (read != 0)
        {
            await destination.WriteAsynchronously(buffer: buffer.AsMemory()[..read],
                                                  cancellationToken: cancellationToken);
            read = await m_Stream.ReadAsync(buffer: buffer,
                                            cancellationToken: cancellationToken);
        }
    }
#else
    private async Task InternalCopyToAsynchronously<TStream>(TStream destination,
                                                             Int32 bufferSize,
                                                             CancellationToken cancellationToken)
        where TStream : IWriteableStream
    {
        Byte[] buffer = new Byte[bufferSize];
        Int32 read = await m_Stream.ReadAsync(buffer: buffer,
                                              offset: 0,
                                              count: bufferSize,
                                              cancellationToken: cancellationToken);
        while (read != 0)
        {
            await destination.WriteAsynchronously(buffer: buffer,
                                                  offset: 0,
                                                  count: read,
                                                  cancellationToken: cancellationToken);
            read = await m_Stream.ReadAsync(buffer: buffer,
                                            offset: 0,
                                            count: bufferSize,
                                            cancellationToken: cancellationToken);
        }
    }
#endif

    internal readonly Stream m_Stream = Stream.Null;
}