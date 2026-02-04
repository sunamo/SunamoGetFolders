namespace SunamoGetFolders._sunamo.SunamoCollectionsChangeContent;

internal class CAChangeContent
{
    /// <summary>
    /// Removes null or empty strings from the list based on the arguments
    /// </summary>
    /// <param name="args">Arguments specifying which items to remove</param>
    /// <param name="list">The list to modify</param>
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

    /// <summary>
    /// Changes content of each element in the list using the provided function
    /// Direct edit - modifies the list in place
    /// If not every element fulfills pattern, it is good to remove null (or values returned if can't be changed) from result
    /// </summary>
    /// <param name="args">Optional arguments for removing null/empty values</param>
    /// <param name="list">The list to modify</param>
    /// <param name="transformFunc">Function to transform each element</param>
    /// <returns>The modified list</returns>
    internal static List<string> ChangeContent0(ChangeContentArgsGetFolders? args, List<string> list,
        Func<string, string> transformFunc)
    {
        for (var i = 0; i < list.Count; i++) list[i] = transformFunc.Invoke(list[i]);
        RemoveNullOrEmpty(args, list);
        return list;
    }
}