namespace SunamoGetFolders._sunamo.SunamoFileSystem;

internal class FSND
{

    

    internal static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1) return nazevPP.ToUpper();

        var sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }
}