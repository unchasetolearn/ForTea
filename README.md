[![official JetBrains project](http://jb.gg/badges/official-flat-square.svg)](https://confluence.jetbrains.com/display/ALL/JetBrains+on+GitHub)

![ForTea Logo](https://raw.github.com/MrJul/ForTea/master/Logo/ForTea%2032x32.png "ForTea Logo") ForTea
====

About
----
ForTea is a plugin for [Rider](https://www.jetbrains.com/rider/) and [ReSharper](https://www.jetbrains.com/resharper/) which adds intelligent support for editing T4 files (`*.tt`, `*.ttinclude`, `*.t4`).
The Rider plugin also can automatically execute and preprocess T4 files.

Installation
----
#### Rider:
Starting with `Rider 2019.3`, this plugin will be installed out of the box.  
If you build the plugin manually, it can be installed using  
_Settings > Plugins > (Gear icon) > Install plugin from disk_

#### ReSharper:
Visual Studio 2010, 2012, 2013, 2015, 2017 and 2019 are supported.  
To use the latest version of the plugin, the latest stable ReSharper must be installed (older releases are still available for some older versions of ReSharper).  
This plugin is not published. If you want to use it, you'll first need to build it. See BUILD.md for details.  
After you've built the plugin, use Extension Manager from the ReSharper menu.

Features
----
 - Execution of classical T4 templates
 - Generation of C# code (aka template preprocessing)
 - Full intelligent support in T4 directives
 - Full intelligent support in C# in in-line blocks: find usages, refactorings, context actions, etc.
 - Extensive intelligent support for includes
 - Support for adding assembly and import directives through quick fixes
 - Lots of other IDE features: file structure, extend selection, refactorings, etc

License
----
Licensed under [Apache License 2.0](LICENSE)
This plugin is a successor to [the original plugin by MrJul](https://github.com/MrJul/ForTea), to whom we are very thankful.

Building plugin
----
Build instructions can be found [here](BUILD.md).  

Things to know about this ReSharper plugin
----
As a ReSharper plugin, ForTea doesn't provide any Visual Studio service,
meaning there are some limitations.
Among those, syntax highlighting is fully handled by ReSharper rather than Visual Studio:
to get coloring for identifiers, you must enable _Color identifiers_ in _ReSharper Options > Code Inspection > Settings_.
Plus, there is no code outlining support yet.

Visual Basic T4 files aren't supported yet.
Custom T4 directives aren't supported yet.