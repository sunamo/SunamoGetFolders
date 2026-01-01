namespace SunamoGetFolders.Args;

/// <summary>
/// Arguments for configuring folder retrieval operations
/// </summary>
public class GetFoldersEveryFolderArgs
{
    /// <summary>
    /// Gets or sets whether to trim the base folder path and leading backslashes from results
    /// </summary>
    public bool _trimA1AndLeadingBs { get; set; } = false;

    /// <summary>
    /// Gets or sets the predicate function to determine if a path is a junction point
    /// </summary>
    public Func<string, bool>? dIsJunctionPoint { get; set; } = null;

    /// <summary>
    /// Gets or sets whether to follow junction points during folder traversal
    /// </summary>
    public bool followJunctions { get; set; } = false;

    /// <summary>
    /// Gets or sets the interval in seconds for logging progress (-1 disables progress logging)
    /// </summary>
    public int SecondsToWriteActualFolder { get; set; } = -1;

    /// <summary>
    /// Gets or sets whether to trim file extensions from results
    /// </summary>
    public bool _trimExt { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to sort results by last modified date in ascending order
    /// </summary>
    public bool byDateOfLastModifiedAsc { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to exclude the newest item from results
    /// </summary>
    public bool dontIncludeNewest { get; set; } = false;

    /// <summary>
    /// Gets or sets the list of path substrings to exclude from results
    /// </summary>
    public List<string> excludeFromLocationsCOntains { get; set; } = new();

    /// <summary>
    /// Gets or sets the custom method for excluding items from results
    /// </summary>
    public Action<List<string>>? excludeWithMethod { get; set; } = null;

    /// <summary>
    /// Gets or sets the function to get last modified date from a file path
    /// </summary>
    public Func<string, DateTime?>? LastModifiedFromFn { get; set; }

    /// <summary>
    /// Gets or sets whether to throw exceptions on errors
    /// </summary>
    public bool throwEx { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to use search pattern from file extension
    /// </summary>
    public bool useMascFromExtension { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to use wildcard matching for exclusions
    /// </summary>
    public bool wildcard { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to write debug output for every loaded folder
    /// </summary>
    public bool writeToDebugEveryLoadedFolder { get; set; } = false;

    /// <summary>
    /// Gets or sets the list of folder names to ignore during traversal
    /// </summary>
    public List<string> IgnoreFoldersWithName { get; set; } = new();

    /// <summary>
    /// When true, excluded folders themselves will be included in results but their subfolders won't be traversed
    /// </summary>
    public bool IncludeExcludedFoldersWithoutTraversing { get; set; } = false;

    private readonly List<string> codeFolders = ["obj", "bin", "node_modules", ".git", ".vs"];

    /// <summary>
    /// Gets or sets whether to exclude common generated code folders (obj, bin, node_modules, .git, .vs)
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