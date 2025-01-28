namespace SunamoGetFolders._sunamo.SunamoCollections;

internal class CA
{


    internal static void InitFillWith<T>(List<T> datas, int pocet, T initWith)
    {
        for (var i = 0; i < pocet; i++) datas.Add(initWith);
    }


    internal static void RemoveWhichContains(List<string> files1, string item, bool wildcard,
        Func<string, string, bool> WildcardIsMatch)
    {
        if (wildcard)
        {
            //item = SH.WrapWith(item, '*');
            for (var i = files1.Count - 1; i >= 0; i--)
                if (WildcardIsMatch(files1[i], item))
                    files1.RemoveAt(i);
        }
        else
        {
            for (var i = files1.Count - 1; i >= 0; i--)
                if (files1[i].Contains(item))
                    files1.RemoveAt(i);
        }
    }

    internal static void RemoveWhichContainsList(List<string> files, List<string> list, bool wildcard,
        Func<string, string, bool> WildcardIsMatch = null)
    {
        foreach (var item in list) RemoveWhichContains(files, item, wildcard, WildcardIsMatch);
    }
}