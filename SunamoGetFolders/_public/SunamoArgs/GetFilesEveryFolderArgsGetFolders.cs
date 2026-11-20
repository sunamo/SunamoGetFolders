namespace SunamoGetFolders._public.SunamoArgs;

/// <summary>
/// Represents the get files every folder args get folders type.
/// </summary>
public class GetFilesEveryFolderArgsGetFolders
{
    /// <summary>
    /// Gets or sets the done.
    /// </summary>
    public Action? Done { get; set; }

    /// <summary>
    /// Gets or sets the done one percent.
    /// </summary>
    public Action? DoneOnePercent { get; set; }

    /// <summary>
    /// Gets or sets the filter founded files.
    /// </summary>
    public Func<string, bool>? FilterFoundedFiles { get; set; }

    /// <summary>
    /// Gets or sets the filter founded folders.
    /// </summary>
    public Func<string, bool>? FilterFoundedFolders { get; set; }

    /// <summary>
    /// Gets or sets the get null if there is more than x files.
    /// </summary>
    public int GetNullIfThereIsMoreThanXFiles { get; set; } = -1;

    /// <summary>
    /// Gets or sets the insert pb.
    /// </summary>
    public Action<double>? InsertPb { get; set; } = null;

    /// <summary>
    /// Gets or sets the insert pb time.
    /// </summary>
    public Action<double>? InsertPbTime { get; set; } = null;

    /// <summary>
    /// Gets or sets the update tb pb.
    /// </summary>
    public Action<string>? UpdateTbPb { get; set; } = null;

    /// <summary>
    /// Gets or sets the use pb.
    /// </summary>
    public bool UsePb { get; set; } = false;

    /// <summary>
    /// Gets or sets the use pb time.
    /// </summary>
    public bool UsePbTime { get; set; } = false;
}
