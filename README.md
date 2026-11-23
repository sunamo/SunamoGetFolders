# SunamoGetFolders

## Short description

Knihovna pro získávání složek s automatickým zachycením výjimek a následným zpracováním výsledku. Podporuje rekurzivní procházení a filtrování. Součástí je Runner a testy.


Retrieving folders with automatic exception catching and further processing after

## Overview

SunamoGetFolders is part of the Sunamo package ecosystem, providing modular, platform-independent utilities for .NET development.

## Main Components

### Key Classes

- **GetFoldersEveryFolderArgs**
- **GetFilesArgsGetFolders**
- **GetFilesBaseArgsGetFolders**
- **GetFilesEveryFolderArgsGetFolders**

### Key Methods

- `GetFoldersEveryFolder()`
- `GetFoldersEveryFolderWhichContainsFiles()`

## Installation

```bash
dotnet add package SunamoGetFolders
```

## Dependencies

- **Microsoft.Extensions.Logging.Abstractions** (v9.0.3)
- **System.Net.Http** (v4.3.4)
- **System.Text.Encodings.Web** (v9.0.3)
- **System.Text.RegularExpressions** (v4.3.1)
- **WildcardMatch** (v1.0.7)

## Package Information

- **Package Name**: SunamoGetFolders
- **Version**: 25.6.7.1
- **Target Framework**: net9.0
- **Category**: Platform-Independent NuGet Package
- **Source Files**: 18

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.

<!-- sunamo:test-coverage:start -->
## Test coverage

| Metric | Value |
|---|---|
| Method | static |
| Test projects | 1 |
| Public types referenced by tests | 2/3 (66.7 %) |
| Public methods referenced by tests | 1/2 (50.0 %) |
| Line coverage | n/a |
| Branch coverage | n/a |
| Generated (UTC) | 2026-11-22 21:24 |

<!-- sunamo:test-coverage:data {"package":"SunamoGetFolders","container":"pinp","testProjects":1,"publicTypes":3,"typesReferencedByTests":2,"publicMethods":2,"methodsReferencedByTests":1,"lineRate":null,"branchRate":null,"method":"static","generatedUtc":"2026-11-22 21:24"} -->
<!-- sunamo:test-coverage:end -->
