namespace SunamoGetFolders._public.SunamoArgs;

public class GetFilesArgsGetFolders : GetFilesBaseArgsGetFolders
{
    public int SecondsToWriteActualFolder { get; set; } = -1;

    public bool _trimA1AndLeadingBs = false;

    public bool _trimExt = false;
    public bool byDateOfLastModifiedAsc = false;
    public bool dontIncludeNewest = false;
    public List<string> excludeFromLocationsCOntains = new();


    public Action<List<string>> excludeWithMethod = null;
    public Func<string, DateTime?> LastModifiedFromFn;
    public bool throwEx = false;


    public bool useMascFromExtension = false;
    public bool wildcard = false;
}