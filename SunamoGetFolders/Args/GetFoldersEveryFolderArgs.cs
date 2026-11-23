namespace SunamoGetFolders.Args;

/// <summary>
/// Represents the get folders every folder args type.
/// </summary>
public class GetFoldersEveryFolderArgs
{
    /// <summary>
    /// Gets or sets the trim a 1 and leading bs.
    /// </summary>
    public bool TrimA1AndLeadingBs { get; set; } = false;

    /// <summary>
    /// Gets or sets the is junction point.
    /// </summary>
    public Func<string, bool>? IsJunctionPoint { get; set; } = null;

    /// <summary>
    /// Gets or sets the follow junctions.
    /// </summary>
    public bool FollowJunctions { get; set; } = false;

    /// <summary>
    /// Gets or sets the seconds to write actual folder.
    /// </summary>
    public int SecondsToWriteActualFolder { get; set; } = -1;

    /// <summary>
    /// Gets or sets the trim ext.
    /// </summary>
    public bool TrimExt { get; set; } = false;

    /// <summary>
    /// Gets or sets the by date of last modified asc.
    /// </summary>
    public bool ByDateOfLastModifiedAsc { get; set; } = false;

    /// <summary>
    /// Gets or sets the dont include newest.
    /// </summary>
    public bool DontIncludeNewest { get; set; } = false;

    /// <summary>
    /// Gets or sets the exclude from locations contains.
    /// </summary>
    public List<string> ExcludeFromLocationsContains { get; set; } = new();

    /// <summary>
    /// Gets or sets the exclude with method.
    /// </summary>
    public Action<List<string>>? ExcludeWithMethod { get; set; } = null;

    /// <summary>
    /// Gets or sets the last modified from fn.
    /// </summary>
    public Func<string, DateTime?>? LastModifiedFromFn { get; set; }

    /// <summary>
    /// Gets or sets the throw ex.
    /// </summary>
    public bool ThrowEx { get; set; } = false;

    /// <summary>
    /// Gets or sets the use masc from extension.
    /// </summary>
    public bool UseMascFromExtension { get; set; } = false;

    /// <summary>
    /// Gets or sets the wildcard.
    /// </summary>
    public bool Wildcard { get; set; } = false;

    /// <summary>
    /// Gets or sets the write to debug every loaded folder.
    /// </summary>
    public bool WriteToDebugEveryLoadedFolder { get; set; } = false;

    /// <summary>
    /// Gets or sets the ignore folders with name.
    /// </summary>
    public List<string> IgnoreFoldersWithName { get; set; } = new();

    // When true, excluded folders themselves will be included in results but their subfolders won't be traversed
    /// <summary>
    /// Gets or sets the include excluded folders without traversing.
    /// </summary>
    public bool IncludeExcludedFoldersWithoutTraversing { get; set; } = false;

    private readonly List<string> codeFolders = ["obj", "bin", "node_modules", ".git", ".vs"];

    /// <summary>
    /// The exclude generated code folders.
    /// </summary>
    public bool ExcludeGeneratedCodeFolders
    {
        set
        {
            if (value)
            {
                foreach (var item in codeFolders)
                {
                    if (!IgnoreFoldersWithName.Contains(item))
                    {
                        IgnoreFoldersWithName.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in codeFolders)
                {
                    IgnoreFoldersWithName.Remove(item);
                }
            }
        }
    }
}
