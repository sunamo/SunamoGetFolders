namespace SunamoGetFolders._sunamo.SunamoCollections;

internal class CA
{
    internal static void RemoveWhichContains(List<string> list, string searchPattern, bool isUsingWildcard,
        Func<string, string, bool>? wildcardIsMatch)
    {
        if (isUsingWildcard && wildcardIsMatch != null)
        {
            for (var i = list.Count - 1; i >= 0; i--)
                if (wildcardIsMatch(list[i], searchPattern))
                    list.RemoveAt(i);
        }
        else
        {
            for (var i = list.Count - 1; i >= 0; i--)
                if (list[i].Contains(searchPattern))
                    list.RemoveAt(i);
        }
    }

    internal static void RemoveWhichContainsList(List<string> list, List<string> searchPatternList, bool isUsingWildcard,
        Func<string, string, bool>? wildcardIsMatch = null)
    {
        foreach (var item in searchPatternList) RemoveWhichContains(list, item, isUsingWildcard, wildcardIsMatch);
    }
}
