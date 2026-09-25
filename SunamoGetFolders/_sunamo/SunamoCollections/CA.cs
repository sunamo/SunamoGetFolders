namespace SunamoGetFolders._sunamo.SunamoCollections;

internal class CA
{
    /// <summary>
    /// Removes items from the list that contain the specified pattern
    /// </summary>
    /// <param name="list">The list to modify</param>
    /// <param name="searchPattern">The pattern to search for</param>
    /// <param name="isUsingWildcard">Whether to use wildcard matching</param>
    /// <param name="wildcardIsMatch">Optional wildcard matching function</param>
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

    /// <summary>
    /// Removes items from the list that contain any pattern from the pattern list
    /// </summary>
    /// <param name="list">The list to modify</param>
    /// <param name="searchPatternList">List of patterns to search for</param>
    /// <param name="isUsingWildcard">Whether to use wildcard matching</param>
    /// <param name="wildcardIsMatch">Optional wildcard matching function</param>
    internal static void RemoveWhichContainsList(List<string> list, List<string> searchPatternList, bool isUsingWildcard,
        Func<string, string, bool>? wildcardIsMatch = null)
    {
        foreach (var item in searchPatternList) RemoveWhichContains(list, item, isUsingWildcard, wildcardIsMatch);
    }
}