namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="IStream"/> that has a known length.
/// </summary>
public interface INonContinousStream :
    IStream
{
    /// <summary>
    /// Gets the amount of bytes in the <see cref="INonContinousStream"/>.
    /// </summary>
    public Int64 Length { get; }

    /// <summary>
    /// Gets the current position of the cursor in the <see cref="INonContinousStream"/>.
    /// </summary>
    public Int64 Position
    {
        get;
        set;
    }
}