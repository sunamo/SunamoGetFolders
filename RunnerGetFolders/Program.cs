using SunamoGetFolders.Tests;

namespace RunnerGetFolders;

internal class Program
{
    static void Main()
    {
        SHGetFoldersTests t = new SHGetFoldersTests();
        //t.GetFoldersTest2();
        t.GetFoldersEveryFolder_ExcludeJunction_Test();

    }
}
