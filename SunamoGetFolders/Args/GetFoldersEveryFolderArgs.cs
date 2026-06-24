namespace SunamoGetFolders.Args;

public class GetFoldersEveryFolderArgs
{
    public bool TrimA1AndLeadingBs { get; set; } = false;

    public Func<string, bool>? IsJunctionPoint { get; set; } = null;

    public bool FollowJunctions { get; set; } = false;

    public int SecondsToWriteActualFolder { get; set; } = -1;

    public bool TrimExt { get; set; } = false;

    public bool ByDateOfLastModifiedAsc { get; set; } = false;

    public bool DontIncludeNewest { get; set; } = false;

    public List<string> ExcludeFromLocationsContains { get; set; } = new();

    public Action<List<string>>? ExcludeWithMethod { get; set; } = null;

    public Func<string, DateTime?>? LastModifiedFromFn { get; set; }

    public bool ThrowEx { get; set; } = false;

    public bool UseMascFromExtension { get; set; } = false;

    public bool Wildcard { get; set; } = false;

    public bool WriteToDebugEveryLoadedFolder { get; set; } = false;

    public List<string> IgnoreFoldersWithName { get; set; } = new();

    // When true, excluded folders themselves will be included in results but their subfolders won't be traversed
    public bool IncludeExcludedFoldersWithoutTraversing { get; set; } = false;

    private readonly List<string> codeFolders = ["obj", "bin", "node_modules", ".git", ".vs"];

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
