namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="IStream"/> that has a known length.
/// </summary>
public interface IContainedStream :
    IStream
{
    /// <summary>
    /// Gets the amount of bytes in the <see cref="IContainedStream"/>.
    /// </summary>
    public Int64 Length { get; }

    /// <summary>
    /// Gets the current position of the cursor in the <see cref="IContainedStream"/>.
    /// </summary>
    public Int64 Position { get; }
}