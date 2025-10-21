// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoGetFolders._sunamo.SunamoCollectionsChangeContent;

internal class CAChangeContent
{
    private static void RemoveNullOrEmpty(ChangeContentArgsGetFolders a, List<string> files_in)
    {
        if (a != null)
        {
            if (a.removeNull) files_in.Remove(null);
            if (a.removeEmpty)
                for (var i = files_in.Count - 1; i >= 0; i--)
                    if (files_in[i].Trim() == string.Empty)
                        files_in.RemoveAt(i);
        }
    }

    /// <summary>
    ///     Direct edit
    ///     If not every element fullfil pattern, is good to remove null (or values returned if cant be changed) from result
    ///     Poslední číslo je počet parametrů jež se předávají do delegátu
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    internal static List<string> ChangeContent0(ChangeContentArgsGetFolders a, List<string> files_in,
        Func<string, string> func)
    {
        for (var i = 0; i < files_in.Count; i++) files_in[i] = func.Invoke(files_in[i]);
        RemoveNullOrEmpty(a, files_in);
        return files_in;
    }

    #region Vem obojí

    #endregion

    #region ChangeContent for easy copy


    #endregion
}