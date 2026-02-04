namespace SunamoGetFolders._public.SunamoArgs;

/// <summary>
/// Arguments for getting files from every folder
/// </summary>
public class GetFilesEveryFolderArgsGetFolders
{
    /// <summary>
    /// Gets or sets the action to execute when done
    /// </summary>
    public Action? Done { get; set; }

    /// <summary>
    /// Gets or sets the action to execute when one percent is done
    /// </summary>
    public Action? DoneOnePercent { get; set; }

    /// <summary>
    /// Gets or sets the filter function for found files
    /// </summary>
    public Func<string, bool>? FilterFoundedFiles { get; set; }

    /// <summary>
    /// Gets or sets the filter function for found folders
    /// </summary>
    public Func<string, bool>? FilterFoundedFolders { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of files before returning null (-1 for unlimited)
    /// </summary>
    public int GetNullIfThereIsMoreThanXFiles { get; set; } = -1;

    /// <summary>
    /// Gets or sets the action to update progress bar value
    /// </summary>
    public Action<double>? InsertPb { get; set; } = null;

    /// <summary>
    /// Gets or sets the action to update progress bar time
    /// </summary>
    public Action<double>? InsertPbTime { get; set; } = null;

    /// <summary>
    /// Gets or sets the action to update progress bar text
    /// </summary>
    public Action<string>? UpdateTbPb { get; set; } = null;

    /// <summary>
    /// Gets or sets whether to use progress bar
    /// </summary>
    public bool UsePb { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to use progress bar time tracking
    /// </summary>
    public bool UsePbTime { get; set; } = false;
}