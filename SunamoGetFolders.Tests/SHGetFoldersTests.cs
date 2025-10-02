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
    public void GetFoldersTest_ReturnCodeFoldersButNotItsInner()
    {
        // Test with folder "a" - add "a" to ignored folders
        var args = new GetFoldersEveryFolderArgs 
        { 
            IgnoreFoldersWithName = { "a" },  // Add "a" to ignored folders
            IncludeExcludedFoldersWithoutTraversing = true 
        };
        
        // Search for folders matching mask "a"
        var f = FSGetFolders.GetFoldersEveryFolder(logger, @"D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\", "a", SearchOption.AllDirectories, args);
        
        // Should contain D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\d\a\
        var aFolder = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\d\a\";
        Assert.Contains(aFolder, f);
        
        // Should NOT contain D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\d\a\ab\a\
        var nestedAFolder = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\d\a\ab\a\";
        Assert.DoesNotContain(nestedAFolder, f);
    }
    
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
        var f = FSGetFolders.GetFoldersEveryFolder(logger, @"D:\_Test\PlatformIndependentNuGetPackages\SunamoGetFolders\", "obj", SearchOption.AllDirectories, args);
        
        // Check results - should find obj folders but not their subfolders
        var objFolder = f.FirstOrDefault(x => x.EndsWith(@"\obj\"));
        if (objFolder != null)  // Only test if obj folder exists
        {
            Assert.NotNull(objFolder);
            
            // Should NOT contain subfolders of obj
            var objSubfolders = f.Where(x => x.Contains(@"\obj\") && !x.EndsWith(@"\obj\")).ToList();
            Assert.Empty(objSubfolders);
        }
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