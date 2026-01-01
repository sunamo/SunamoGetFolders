namespace SunamoGetFolders._sunamo.SunamoArgs;

/// <summary>
/// Arguments for configuring content change operations
/// </summary>
internal class ChangeContentArgsGetFolders
{
    /// <summary>
    /// Gets or sets whether to remove empty strings from the result
    /// </summary>
    internal bool removeEmpty { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to remove null values from the result
    /// </summary>
    internal bool removeNull { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to switch the first and second arguments
    /// </summary>
    internal bool switchFirstAndSecondArg { get; set; } = false;
}