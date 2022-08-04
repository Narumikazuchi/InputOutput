﻿namespace Narumikazuchi.InputOutput;

/// <summary>
/// Wraps a <see cref="Stream"/> that can be writen to (<see cref="Stream.CanWrite"/>) into the
/// <see cref="IWriteableStream"/> interface.
/// </summary>
public readonly partial struct WriteableStreamWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WriteableStreamWrapper"/> struct.
    /// </summary>
    public WriteableStreamWrapper()
    {
        m_Stream = Stream.Null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteableStreamWrapper"/> struct.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap.</param>
    /// <exception cref="ArgumentException"/>
    public WriteableStreamWrapper(Stream stream)
    {
        if (!stream.CanWrite)
        {
            throw new ArgumentException("can't write");
        }
        else
        {
            m_Stream = stream;
        }
    }

#pragma warning disable CS1591 // XML Comment
    public static implicit operator WriteableStreamWrapper(Stream stream)
    {
        if (!stream.CanWrite)
        {
            throw new ArgumentException("", nameof(stream));
        }
        else
        {
            return new(stream);
        }
    }
#pragma warning restore
}

// Non-Public
partial struct WriteableStreamWrapper
{
    internal readonly Stream m_Stream = Stream.Null;
}

// IAsyncDisposable
partial struct WriteableStreamWrapper : IAsyncDisposable
{
    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (m_Stream is not null)
        {
            await m_Stream.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}

// IDisposable
partial struct WriteableStreamWrapper : IDisposable
{
    /// <inheritdoc/>
    public void Dispose()
    {
        if (m_Stream is not null)
        {
            m_Stream.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

// IWriteableStream
partial struct WriteableStreamWrapper : IWriteableStream
{
    /// <inheritdoc/>
    public void Close() =>
        this.Dispose();

    /// <inheritdoc/>
    public void Flush()
    {
        if (m_Stream is not null)
        {
            m_Stream.Flush();
        }
    }

    /// <inheritdoc/>
    public async ValueTask FlushAsync()
    {
        if (m_Stream is not null)
        {
            await m_Stream.FlushAsync();
        }
    }

    /// <inheritdoc/>
    public void Write(ReadOnlySpan<Byte> buffer)
    {
        if (m_Stream is not null)
        {
            m_Stream.Write(buffer: buffer);
        }
    }

    /// <inheritdoc/>
    public ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer,
                                CancellationToken cancellationToken)
    {
        if (m_Stream is null)
        {
            return ValueTask.CompletedTask;
        }
        else
        {
            return m_Stream.WriteAsync(buffer: buffer,
                                       cancellationToken: cancellationToken);
        }
    }

    /// <inheritdoc/>
    public void WriteByte(Byte value)
    {
        if (m_Stream is not null)
        {
            m_Stream.WriteByte(value);
        }
    }

    /// <inheritdoc/>
    public void SetLength(Int64 length)
    {
        if (m_Stream is not null)
        {
            m_Stream.SetLength(length);
        }
    }

    /// <inheritdoc/>
    public Int64 Length
    {
        get
        {
            if (m_Stream is null)
            {
                return 0;
            }
            else if (m_Stream.CanSeek)
            {
                return m_Stream.Length;
            }
            else
            {
                throw new ObjectDisposedException(null);
            }
        }
    }

    /// <inheritdoc/>
    public Int64 Position
    {
        get
        {
            if (m_Stream is null)
            {
                return 0;
            }
            else if (m_Stream.CanSeek)
            {
                return m_Stream.Position;
            }
            else
            {
                throw new ObjectDisposedException(null);
            }
        }
        set
        {
            if (m_Stream is not null &&
                m_Stream.CanSeek)
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                else
                {
                    m_Stream.Position = value;
                }
            }
            else if (m_Stream is not null)
            {
                throw new ObjectDisposedException(null);
            }
        }
    }
}