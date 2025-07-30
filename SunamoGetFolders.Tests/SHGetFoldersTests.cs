namespace SunamoGetFolders.Tests;

using Microsoft.Extensions.Logging;
using SunamoGetFolders.Args;
using SunamoTest;
using System.Text;

public class SHGetFoldersTests
{
    ILogger logger = TestLogger.Instance;

    [Fact]
    public void GetFoldersEveryFolderTest()
    {
        var gitPaths = FSGetFolders.GetFoldersEveryFolder(logger, @"E:\vs", ".git", SearchOption.TopDirectoryOnly, new SunamoGetFolders.Args.GetFoldersEveryFolderArgs { excludeFromLocationsCOntains = ["de_mo", @"\obj\", ".vs"], SecondsToWriteActualFolder = 5 });


    }

    [Fact]
    public void GetFoldersEveryFolder_ExcludeJunction_Test()
    {
        var d = FSGetFolders.GetFoldersEveryFolder(logger, @"D:\", "*", SearchOption.TopDirectoryOnly, new GetFoldersEveryFolderArgs { followJunctions = false });
    }

    [Fact]
    public void GetFoldersTest2()
    {
        var f = FSGetFolders.GetFoldersEveryFolder(logger, @"D:\_Test\", $"*{"PlatformIndependentNuGetPackages"}*");
    }

    [Fact]
    public void GetFoldersTest()
    {
        List<List<string>> r = new List<List<string>>();

        var d = DriveInfo.GetDrives();
        foreach (var item in d)
        {
            r.Add(FSGetFolders.GetFoldersEveryFolder(logger, item.RootDirectory.FullName, "*", SearchOption.TopDirectoryOnly, new Args.GetFoldersEveryFolderArgs { followJunctions = false }));
        }

        StringBuilder sb = new StringBuilder();

        foreach (var item in r)
        {
            foreach (var item2 in item)
            {
                sb.AppendLine(item2);
            }
            sb.AppendLine();
            sb.AppendLine();
        }

        File.WriteAllText(@"D:\a.txt", sb.ToString());
    }
}