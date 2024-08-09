namespace SunamoGetFolders._public.SunamoArgs;

public class GetFilesBaseArgsGetFolders
{
    public bool _trimA1AndLeadingBs = false;
    public Func<string, bool> dIsJunctionPoint = null;
    public bool followJunctions = false;
}