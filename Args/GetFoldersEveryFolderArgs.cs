namespace SunamoGetFolders.Args;

public class GetFoldersEveryFolderArgs : GetFilesArgsGetFolders
{
    /// <summary>
    ///     Auto call WithEndSlash
    /// </summary>
    public bool _trimA1AndLeadingBs;
    public SearchOption SearchOption { get; set; } = SearchOption.AllDirectories;

    /// <summary>
    /// Hodí se při velkých složkách např. E:\vs
    /// Je zbytečné vypisovat miliony složek pro každou ale co x sekund aby bylo vidět že se něco děje
    /// </summary>


    public List<string> excludeFromLocationsCOntains = null;

    // nevím k čemu to je ale zdá se nesmysl, ověřovat můžu přes excludeFromLocationsCOntains != null
    //public bool excludeFromLocationsCOntainsBool = false;
    public bool writeToDebugEveryLoadedFolder = false;

    public GetFoldersEveryFolderArgs(GetFilesEveryFolderArgsGetFolders e)
    {
        _trimA1AndLeadingBs = e._trimA1AndLeadingBs;
        followJunctions = e.followJunctions;
        dIsJunctionPoint = e.dIsJunctionPoint;
    }

    public GetFoldersEveryFolderArgs()
    {
    }
}