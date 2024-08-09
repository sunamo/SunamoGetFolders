namespace SunamoGetFolders._public.SunamoArgs;

public class GetFilesEveryFolderArgsGetFolders : GetFilesBaseArgsGetFolders
{
    public Action Done;
    public Action DoneOnePercent;

    public Func<string, bool> FilterFoundedFiles;
    public Func<string, bool> FilterFoundedFolders;
    public int getNullIfThereIsMoreThanXFiles = -1;
    public Action<double> InsertPb = null;
    public Action<double> InsertPbTime = null;
    public Action<string> UpdateTbPb = null;
    public bool usePb = false;
    public bool usePbTime = false;
}