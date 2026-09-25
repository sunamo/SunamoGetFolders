namespace SunamoGetFolders._sunamo.SunamoCollectionsChangeContent;

internal class CAChangeContent
{
    private static void RemoveNullOrEmpty(ChangeContentArgsGetFolders? args, List<string> list)
    {
        if (args != null)
        {
            if (args.RemoveNull) list.Remove(null!);
            if (args.RemoveEmpty)
                for (var i = list.Count - 1; i >= 0; i--)
                    if (list[i].Trim() == string.Empty)
                        list.RemoveAt(i);
        }
    }

    internal static List<string> ChangeContent0(ChangeContentArgsGetFolders? args, List<string> list,
        Func<string, string> transformFunc)
    {
        for (var i = 0; i < list.Count; i++) list[i] = transformFunc.Invoke(list[i]);
        RemoveNullOrEmpty(args, list);
        return list;
    }
}
