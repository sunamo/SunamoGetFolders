namespace SunamoGetFolders.Args;

public class GetFoldersEveryFolderArgs //: GetFilesArgsGetFolders
{
    public bool _trimA1AndLeadingBs = false;
    public Func<string, bool> dIsJunctionPoint = null;
    public bool followJunctions = false;

    public int SecondsToWriteActualFolder { get; set; } = -1;


    public bool _trimExt = false;
    public bool byDateOfLastModifiedAsc = false;
    public bool dontIncludeNewest = false;
    public List<string> excludeFromLocationsCOntains = new();


    public Action<List<string>> excludeWithMethod = null;
    public Func<string, DateTime?> LastModifiedFromFn;
    public bool throwEx = false;


    public bool useMascFromExtension = false;
    public bool wildcard = false;


    // nevím k čemu to je ale zdá se nesmysl, ověřovat můžu přes excludeFromLocationsCOntains != null
    //public bool excludeFromLocationsCOntainsBool = false;
    public bool writeToDebugEveryLoadedFolder = false;


    public List<string> IgnoreFoldersWithName { get; set; } = new();

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


    public GetFoldersEveryFolderArgs()
    {
    }
}