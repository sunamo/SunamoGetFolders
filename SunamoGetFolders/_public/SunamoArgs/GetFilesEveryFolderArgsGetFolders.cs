namespace SunamoGetFolders._public.SunamoArgs;

public class GetFilesEveryFolderArgsGetFolders
{
    public Action? Done { get; set; }

    public Action? DoneOnePercent { get; set; }

    public Func<string, bool>? FilterFoundedFiles { get; set; }

    public Func<string, bool>? FilterFoundedFolders { get; set; }

    public int GetNullIfThereIsMoreThanXFiles { get; set; } = -1;

    public Action<double>? InsertPb { get; set; } = null;

    public Action<double>? InsertPbTime { get; set; } = null;

    public Action<string>? UpdateTbPb { get; set; } = null;

    public bool UsePb { get; set; } = false;

    public bool UsePbTime { get; set; } = false;
}
