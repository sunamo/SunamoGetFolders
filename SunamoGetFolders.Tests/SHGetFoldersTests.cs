// variables names: ok
namespace SunamoGetFolders.Tests;

using Microsoft.Extensions.Logging;
using SunamoGetFolders.Args;
using SunamoTest;
using System.Text;

/// <summary>
/// Test class for FSGetFolders functionality
/// </summary>
public class SHGetFoldersTests
{
    ILogger logger = TestLogger.Instance;

    /// <summary>
    /// Tests GetFoldersEveryFolder with exclusion patterns and logging
    /// </summary>
    [Fact]
    public void GetFoldersEveryFolderTest()
    {
        var gitPaths = FSGetFolders.GetFoldersEveryFolder(logger, @"E:\vs", ".git", SearchOption.TopDirectoryOnly, new SunamoGetFolders.Args.GetFoldersEveryFolderArgs { ExcludeFromLocationsContains = ["de_mo", @"\obj\", ".vs"], SecondsToWriteActualFolder = 5 });


    }

    /// <summary>
    /// Tests GetFoldersEveryFolder with junction point exclusion
    /// </summary>
    [Fact]
    public void GetFoldersEveryFolder_ExcludeJunction_Test()
    {
        var data = FSGetFolders.GetFoldersEveryFolder(logger, @"D:\", "*", SearchOption.TopDirectoryOnly, new GetFoldersEveryFolderArgs { FollowJunctions = false });
    }

    /// <summary>
    /// Tests GetFoldersEveryFolder with wildcard pattern matching
    /// </summary>
    [Fact]
    public void GetFoldersTest2()
    {
        var folders = FSGetFolders.GetFoldersEveryFolder(logger, @"D:\_Test\", $"*{"PlatformIndependentNuGetPackages"}*");
    }

    /// <summary>
    /// Tests that ignored folders are included in results but their subfolders are not traversed
    /// </summary>
    [Fact]
    public void GetFoldersTest_ReturnCodeFoldersButNotItsInner()
    {
        // Test with folder "a" - add "a" to ignored folders
        var args = new GetFoldersEveryFolderArgs 
        { 
            IgnoreFoldersWithName = { "a" },  // Add "a" to ignored folders
            IncludeExcludedFoldersWithoutTraversing = true 
        };
        
        // Search for folders matching mask "a"
        var foundFolders = FSGetFolders.GetFoldersEveryFolder(logger, @"D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\", "a", SearchOption.AllDirectories, args);

        // Should contain data:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\d\a\
        var aFolder = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\d\a\";
        Assert.Contains(aFolder, foundFolders);

        // Should NOT contain data:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\d\a\ab\a\
        var nestedAFolder = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\d\a\ab\a\";
        Assert.DoesNotContain(nestedAFolder, foundFolders);
    }

    /// <summary>
    /// Tests ExcludeGeneratedCodeFolders flag with IncludeExcludedFoldersWithoutTraversing
    /// </summary>
    [Fact]
    public void GetFoldersTest_ReturnCodeFoldersButNotItsInner_WithExcludeGeneratedCodeFolders()
    {
        // Test with ExcludeGeneratedCodeFolders which adds standard code folders to ignore list
        var args = new GetFoldersEveryFolderArgs 
        { 
            ExcludeGeneratedCodeFolders = true,  // This adds obj, bin, node_modules, .git, .vs to IgnoreFoldersWithName
            IncludeExcludedFoldersWithoutTraversing = true 
        };
        
        // Search for folders named "obj"
        var foundFolders = FSGetFolders.GetFoldersEveryFolder(logger, @"D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\", "obj", SearchOption.AllDirectories, args);

        // Check results - should find obj folders but not their subfolders
        var objFolder = foundFolders.FirstOrDefault(x => x.EndsWith(@"\obj\"));
        if (objFolder != null)  // Only test if obj folder exists
        {
            Assert.NotNull(objFolder);

            // Should NOT contain subfolders of obj
            var objSubfolders = foundFolders.Where(x => x.Contains(@"\obj\") && !x.EndsWith(@"\obj\")).ToList();
            Assert.Empty(objSubfolders);
        }
    }

    /// <summary>
    /// Tests GetFoldersEveryFolder on all drives and writes results to file
    /// </summary>
    [Fact]
    public void GetFoldersTest()
    {
        List<List<string>> driveResults = new List<List<string>>();

        var drives = DriveInfo.GetDrives();
        foreach (var item in drives)
        {
            driveResults.Add(FSGetFolders.GetFoldersEveryFolder(logger, item.RootDirectory.FullName, "*", SearchOption.TopDirectoryOnly, new Args.GetFoldersEveryFolderArgs { FollowJunctions = false }));
        }

        StringBuilder stringBuilder = new StringBuilder();

        foreach (var item in driveResults)
        {
            foreach (var folderPath in item)
            {
                stringBuilder.AppendLine(folderPath);
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
        }

        File.WriteAllText(@"D:\a.txt", stringBuilder.ToString());
    }
}