namespace Narumikazuchi.InputOutput;

/// <summary>
/// Represents an <see cref="IStream"/> that can timeout.
/// </summary>
public interface ITimeoutStream :
    IStream
{
    /// <summary>
    /// Gets or sets a value, in milliseconds, that determines how long the <see cref="ITimeoutStream"/> will attempt to read before timing out.
    /// </summary>
    public Int32 ReadTimeout
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets a value, in milliseconds, that determines how long the <see cref="ITimeoutStream"/> will attempt to write before timing out.
    /// </summary>
    public Int32 WriteTimeout
    {
        get;
        set;
    }
}