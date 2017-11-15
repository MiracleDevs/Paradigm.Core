[![Build Status](https://travis-ci.org/MiracleDevs/Paradigm.Core.svg?branch=master)](https://travis-ci.org/MiracleDevs/Paradigm.Core)


| Library | Nuget | Install
|-|-|-|
| Assemblies | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Core.Assemblies/) | `Install-Package Paradigm.Core.Assemblies`
| Dependency Injection | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Core.DependencyInjection/) | `Install-Package Paradigm.Core.DependencyInjection`
| Extensions | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Core.Extensions/)| `Install-Package Paradigm.Core.Extensions`
| Logging | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Core.Logging/)| `Install-Package Paradigm.Core.Logging`
| Mapping | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Core.Mapping/)| `Install-Package Paradigm.Core.Mapping`



# Paradigm.Core
Set of core libraries used by the paradigm framework in .net core.

Change log
---

Version `2.0.3`
- Added new member configuration method to mapping project to allow to setup the constructor of a mapping configuration.
  Without this ability, domain entities can not be created with a DI container.
- Added new tests for the mapping library.
- Updated nuget dependencies.


Version `2.0.2`
- Improved loggers code.
- Added three new static methods to `FileLogging` to create typed loggers:
  - `FileLogging.CreateCsv`
  - `FileLogging.CreateJson`
  - `FileLogging.CreateXml`

  Take in mind that at least for now, Json and Xml files won't be closed correctly,
  due to the logger not knowing when will be the last writing, and to prevent costly
  file write/read operations. So it  will depend on you to close it before using them.
- Added tests for all type of loggers.
- Added new methods to `CombineLogging` to spread changes among its loggers.
- Updated nuget dependencies.


Version `2.0.1`
- Updated nuget dependencies.
- Added new logging types:
  - ConsoleLogging: Allows to set the foreground and background colors.
  - CombineLogging: Allows to combine multiple loggers.


Version `2.0.0`
- Updated .net core from version 1 to version 2.


Version `1.0.1`
- Updated nuget package configuration.
- Fixed wrong project version to 1.6.


Version `1.0.0`
- Uploaded first version of the Paradigm Core.