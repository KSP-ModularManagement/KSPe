# KSP API Extensions/L :: Change Log

* 2021-0818: 2.3.0.4 (Lisias) for KSP >= 1.2
	+ Adding KSP 1.12.1 and 1.12.2 to the "Database"
* 2021-0626: 2.3.0.3 (Lisias) for KSP >= 1.2
	+ Add KSP 1.12.0 to the "Database"
	+ Promotes to RELEASE
	+ Closes issues:
		- [#15](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/15) Ensure this is working fine on KSP 1.12.x
		- [#14](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/14) The installment check is terribly naive
* 2021-0411: 2.3.0.2 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Reworking a Installment Check
		- Some third parties tools are launching KSP on Windows using a pathname with different case from where it is really installed, and this played havoc with the current check.
	+ Closes issues:
		- [#9](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/9) Throw a FATAL when the PWD is set to a different directory than "Origin"
* 2021-0411: 2.3.0.1 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Resurrecting the KSPe.System idea
		- Adding helpers for Enums, implementing in a 3.5 compatible way some facilities from .Net 4 and newer
	+ Closes issues:
		- [#10](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/10) BUG on calculating paths on Symlinks on Windows
		- [#9](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/9) Throw a FATAL when the PWD is set to a different directory than "Origin"
		- [#8](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/8) NRE on instantiating KSPe's Logger when the class has no Namespace
* 2021-0325: 2.3.0.0 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Extending the Abstracted File System for Savegames
	+ Added a XInput Abstraction Layer (currently short circuited to dry run)
	+ Added KSP 1.11.2 to the "Database"
* 2021-0323: 2.2.3.1 (Lisias) for KSP >= 1.2
	+ ***Withdrawn*** due a faulty implementation on a new feature.
* 2021-0227: 2.2.3.0 (Lisias) for KSP >= 1.2
	+ New Logging on file support
		- `FileChainUnityLogger<T>`
	+ Bug fixes and small enhancements
		- Side DLLs loading improvements
			- Multiple KSP and Unity support overhaul
		- Normalising the trailing PathSeparator on Path functions that returns a directory
			- Also fixes a long standing bug on the Install Error Dialog 
		- Fixing less than optimum decisions on the threading log support
		- Avoiding Linq on hot code
		- More sensible `KSPe.cfg` default settings. 
* 2021-0208: 2.2.2.3 (Lisias) for KSP >= 1.2
	+ Some pretty stupid omission fixed on handling the installment checks
	+ Closes (again):
		- [#6](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/6) The Sandboxed File System is borking on symlinks
* 2021-0104: 2.2.2.2 (Lisias) for KSP >= 1.2
	+ Nicer tools to handle KSP versions
* 2020-1220: 2.2.2.1 (Lisias) for KSP >= 1.2
	+ Adding formal support for KSP 1.11.0
	+ Adding specialised support for Unity versions (5, 2017 & 2019 at this time)
		- This removes a lot of duplicated code from the KSP's specialised support 
	+ Incepting a new helper, `C# Emulator`, to backport to Mono 3.5 classes and helpers from 4.x, easing backporting for Add'Ons. 
		- Minimalistic at this time, but this will grow with time. 
* 2020-1219: 2.2.2.0 (Lisias) for KSP >= 1.2
	+ ***Withdrawn*** due an idiotic mistake on the Instalment check... =/
* 2020-1025: 2.2.1.7 (Lisias) for KSP >= 1.2
	+ Lame mishap on handling Installment Check Exceptions was detected and fixed.
* 2020-1011: 2.2.1.6 (Lisias) for KSP >= 1.2
	+ Abstracts the Unity's Screen Capture feature.
		- You call KSPe.Util.Image.Capture and you will have your screenshot not matter the Unity version you are running! #hurray! :)
	+ Implements a check against duplicated DLLs on the Instalment Checks facilities. 
	+ Closes [Issue #6 The Sandboxed File System is borking on symlinks](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/6)
		- Some heavy refactoring involved.
	+ Fixes an old bug still lingering on the ConfigNode facilities.
* 2020-1004: 2.2.1.5 (Lisias) for KSP >= 1.2
	+ **Finally** implementing the Commit/Rollback stunt for the `WriteableConfigNode` descendants.
		- Now you can just `configNode.Rollback()` to revert all the uncommitted changes if anything bad happen.
		- `Save` implies `Commit` (auto commit style), but you can use `configNode.Commit()` sporadically to make sure part of the job will be persisted if it is aborted.
	+ Some helpers to *deep*, *shallow* or *flat* copy ConfigNodes.
	+ Some hackish helpers for linking files on GameData into the GameDatabase.
		- Ugly as hell, but it solved the problem. 
	+ **IMPORTANT**
		- Completely remove the folder `000_KSPAPIExtensions` if you didn't did that recently.
		- A rogue DLL leaked on the 2.2.1.3 release, and **IT MUST BE REMOVED** 
* 2020-1001: 2.2.1.4 (Lisias) for KSP >= 1.2
	+ Some pretty lame mistakes fixed.
	+ **IMPORTANT**
		- Completely remove the folder `000_KSPAPIExtensions`
		- A rogue DLL leaked on the last release, and **IT MUST BE REMOVED** 
* 2020-0901: 2.2.1.3 (Lisias) for KSP >= 1.2
	+ New Global/Local configuration support for KSPe features.
		- Read the [Documentation](https://github.com/net-lisias-ksp/KSPAPIExtensions/blob/mestre/Docs/KSPe.md) for more information. 
* 2020-0825: 2.2.1.1 (Lisias) for KSP >= 1.2
	+ New ModuleManager Tools are now available
	+ On MacOS and Linux, symlinks are now useable inside GameData
		- KSPe "undoes" the nasty symlinks resolving imposed by the Mono runtime. **Transparently**.
		- Not available on Windows.
	+ Thread Safe Unity Logging
		- On by default for every `KSPe.Util.Log` client
			- It will delay the message on the KSP.log for at least one frame.
			- Refer to the source code about how to disable it for your add'on if this is not desirable.
		- Does not interfere with normal `UnityEngine.Debug` Logging.
	+ Falling back to KSP.UI.12 if the dependencies for KSP.UI on KSP >= 1.4 are not met.
* 2020-0822: 2.2.1.0 (Lisias) for KSP >= 1.2
	+ **Withdrawn** to an unhappy decision made on the API.
* 2020-0729: 2.2.0.2 (Lisias) for KSP >= 1.2
	+ updating fixing the known KSP Versions 'database'
* 2020-0725: 2.2.0.1 (Lisias) for KSP >= 1.2
	+ updating fixing the known KSP Versions 'database'
	+ KSP Abstraction Layer Kick Off!
		- Specialised and transparent KSP UI support 
		- Specialised and transparent KSP specific version features
			- Including a Quick&Dirty MM patching support.
		- And made right this time. =/
	+ Bumping up minor version to allow Add'Ons to alert when running on older KSPe versions without these features. 
	+ Now (current) Module Manager /L Experimental can run on KSP 1.2 and 1.3 without even a recompile! (hopefully! :D)
* 2020-0724: 2.2.0.0 (Lisias) for KSP >= 1.2
	* *WITHDRAWN*
* 2020-0531: 2.1.1.6 (Lisias) for KSP >= 1.2
	+ Refactoring the Log Subsystem:
		- Storming the castle and taking over the whole Unity Logging to KSPe itself
		- See Issue [#4](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/4)
		- A **huge** THANK YOU to [hendrack](https://forum.kerbalspaceprogram.com/index.php?/profile/142754-hendrack/) for the report and the priceless help on the diagnosing process!
* 2020-0428: 2.1.1.5 (Lisias) for KSP >= 1.2
	+ Updated KSP Version "database" to 1.9.0 and 1.9.1
	+ Added KSP Version facilites. 
* 2020-0128: 2.1.1.4 (Lisias) for KSP >= 1.2
	+ Regression on solving Assets path fixed.
* 2020-0116: 2.1.1.3 (Lisias) for KSP >= 1.2
	+ New helpers to allow clients to find their way on the abstract file system without being hard tied to the default hierarchies.
		- Nothing to be heavily used, but it can helps on some specific, niche situations.
		- A not so abstract mechanism so allow some abstraction without relying on the unrealiable C# Reflection
	+ A heavy refactoring was applied on critical sections to allow proper implementation of the present and futue features.
* 2020-0107: 2.1.1.2 (Lisias) for KSP >= 1.2
	+ Fixing yet another stupid mistake on handling resource names from GameDB on Windows Machines.
	+ New Features
		- Expanded support for seamless subdirectories definitions when naming filenames (on the File<> replacement for System.IO.File).
* 2020-0105: 2.1.1.1 (Lisias) for KSP >= 1.2
	+ Fixed stupid mistake on handling pathnames on fetching Textures on the old API.
* 2020-0101: 2.1.1.0 (Lisias) for KSP >= 1.2
	+ New Features 
		- Added support for seamless subdirectories definitions when naming filenames
			- No more System.IO.Path.Combine !
		- More versatile Textures Loading
		- New public Facade for the abstract file system features
	+ Bug fixes
		- Better TEMP files handling
		- GameDB Asset solver now solves correctly
* 2019-1018: 2.1.0.17 (Lisias) for KSP >= 1.2
	+ New Installment facilities:
		- Checking on compatible Unity version(s) 
	+ Fixing a stupid mistake on handling pathnames that rendered this thing unusable on Windows!
* 2019-1005: 2.1.0.16 (Lisias) for KSP >= 1.2
	+ Preemptive update to support Unity 2019.2 (KSP 1.8) 
	+ Known issue:
		- KSPe.UI doesn't loads on KSP <= 1.3.1 . Delete it from `000_KSPAPIExtensions/Plugins` as a temporary measure.  
		- See Issue [#3](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/3)
* 2019-0728: 2.1.0.15 (Lisias) for KSP >= 1.2
	+ Fixing last minute mishaps:
		- More robust Unity version identification and handling
		- Better coping with current practices on KSPe.IO.File.Assets
	+ Known issue:
		- KSPe.UI doesn't loads on KSP <= 1.3.1 due an idiotic mistake on handling dependencies. Delete it from `000_KSPAPIExtensions/Plugins` as a temporary measure.  
* 2019-0727: 2.1.0.14 (Lisias) for KSP >= 1.2 REISSUE
	+ Adding formal dependency for [Click Through Blocker](https://forum.kerbalspaceprogram.com/index.php?/topic/170747-151-click-through-blocker/) 1.7.2 on KSPe.UI.
	+ New UI helpers:
		- Message Box : Big, centered and modal Window
		- Alert Box with close countdown : smaller, positionable Window.
	+ New Image Utils
		- Universal Texture Loader (works on any Unity) 
* 2019-0718: 2.1.0.13 (Lisias) for KSP >= 1.2 **WITHDRAWN**
	+ Withdrawn due a stupid mistake on compiling against a beta release of an third-party Add'On
* 2019-0524: 2.1.0.10 (Lisias) for KSP >= 1.2
	+ KSPe goes gold after 8 months in development! :)
	+ Adding a proxy to [Click Through Blocker](https://forum.kerbalspaceprogram.com/index.php?/topic/170747-151-click-through-blocker/), what will allow seamless integration with it and new solutions
		- Including selecting the desired one in runtime!
		- Will raise the minimum supported KSP to the installed Click Through Blocker supported version.
* 2019-0503: 2.1.0.9 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Adding support for headers when saving ConfigNodes.
	+ Adding local cache support to the *.Solve functions (saving some processing on the client side, by avoiding hitting the Reflection stuff more than one time for file)
* 2019-0125: 2.1.0.8 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Preventing concurrent processes from playing havoc on the KSP/Unity logging.
		- No more entries being written in the middle of the previous ones! :) 	
	+ Better Listing Files support.
	+ Some more syntactic sugar
		- Adding generics syntax to ConfigNode's GetValue, making easier to port code from XML (PluginConfiguration) to CFG (ConfigNode) and vice versa
	+ Small refactoring on the class hierarchy
		- Some classes were deprecated, but will be maintained until the version 3
		- No impact to the user base, I'm the only client for this stunt! :D
	+ Removed Max KSP version check.
	+ Fixed a glitch that prevented debug messages from being suppressed.
* 2019-0101: 2.1.0.7 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ **DITCHED** (again²! =P)
* 2018-1231: 2.1.0.6 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ **DITCHED** (again! =P)
* 2018-1228: 2.1.0.5 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ **DITCHED**
* 2018-1206: 2.1.0.4 (Lisias) for {1.2 <= KSP <= 1.5.1} PRE-RELEASE
	+ Implementing Helper Classes for Assets, Local and Temp files
	+ Some minor code sanitizing and normalizing to promote maintainability
	+ Adding install instructinos to the package, as suggested by [scottadges](https://forum.kerbalspaceprogram.com/index.php?/profile/174580-scottadges/) [on forum](https://forum.kerbalspaceprogram.com/index.php?/topic/50911-13-kerbal-joint-reinforcement-v333-72417/&do=findComment&comment=3499125)
* 2018-1206: 2.1.0.3 (Lisias) for {1.2 <= KSP <= 1.5.1} PRE-RELEASE
	+ **DITCHED**
* 2018-1202: 2.1.0.2 (Lisias) for KSP 1.2+; 1.3+; 1.4+; 1.5+ PRE-RELEASE
	+ Changing the Logging default to INFO. 
	+ Stating compatibility with KSP since version 1.2
		- Tested on 1.2.2 and above. No flaws. 
* 2018-1024: 2.1.0.1 (Lisias) for KSP 1.4 PRE-RELEASE
	+ Implementing sandboxed File<type> decorators:
		- [Asset|Data|Local|Temp].ReadAll*()
	+ Fixed bug on KSPe.IO.File<type>.Asset pathname solving algorithm.
* 2018-1024: 2.1.0.0 (Lisias) for KSP 1.4 PRE-RELEASE
	+ **DITCHED**
* 2018-1010: 2.0.0.2 (Lisias) for KSP 1.4
	+ Dumb mistake on the PluginConfig.Save method.
		- Critical for [ModuleManager](https://github.com/net-lisias-kspu/ModuleManager). 
* 2018-1008: 2.0.0.1 (Lisias) for KSP 1.4
	+ Logging Helpers (`KSPe.Util.Log`)
		- Currently, only `UnitEngine.Debug.Log*` as target. 
	+ This is a proper release.
		- All plugins with KSPe 2.0 dependencies will be satisfied by this version.
* 2018-0930: 2.0 (Lisias) for KSP 1.4 PRE-RELEASE
	+ Recompile of legacy code to KSP 1.4 series
	+ Added `KSPe` namespace/API.
	+ Helpers for Reading/Writing ConfigNode and other data files on standardized and specialized directory hierarchy:
		- KSPe.IO.PluginConfiguration
		- KSPe.KspConfig
		- KSPe.PluginConfig 
