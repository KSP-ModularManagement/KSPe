# KSP API Extensions/L :: Change Log

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
* 2015-0625: 1.7.5 (Swamp-Ig) for KSP 1.0.4
	+ Recompile for KSP 1.0.4
* 2015-0502: 1.7.4 (Swamp-Ig) for KSP 1.0
	+ Compatible with KSP 1.0.2
* 2015-0427: 1.7.3 (Swamp-Ig) for KSP 1.0
	+ Fixed for 1.0, thanks to @taniwha-qf  (as usual)
* 2014-1216: 1.7.2 (Swamp-Ig) for KSP 0.90
	+ Recompiled for KSP 0.90
* 2014-1009: 1.7.1 (Swamp-Ig) for KSP 0.25
	+ Compiled for KSP 0.25
* 2014-0802: 1.7.0 (Swamp-Ig) for KSP 0.24.2
	+ Fix multiple version handling.
	+ Note that for this to work properly, all mods using KSPAPIExtensions need to be updated. After that, they should be free to drift again.
* 2014-0725: 1.6.3 (Swamp-Ig) for KSP 0.24.2
	+ Update to KSP 0.24.2
* 2014-0725: 1.6.2 (Swamp-Ig) for KSP 0.24.1
	+ Update checker for .24.1
* 2014-0724: 1.6.1 (Swamp-Ig) for KSP 0.24
	+ More fixes from taniwha:
		- Avoid NRE in OutputLocksModified
		- Avoid a divide-by-zero
		- Avoid recursive value change events.
* 2014-0720: 1.6.0 (Swamp-Ig) for KSP 0.24
	+ fix by taniwha to part resource tweaker code for .24
	+ update CompatibilityChecker for .24 and to latest code from Majiir
* 2014-0612: 1.5.1 (Swamp-Ig) for KSP 0.23.5
	+ Bugfix #6. Needed for some things to work with Alt-Click in VAB.
* 2014-0607: 1.5.0 (Swamp-Ig) for KSP 0.23.5
	+ Added ConfigNodeUtils - adds some extra methods to ConfigNodes, and ParseUtils for parsing various types.
	+ Thanks for stupid_chris for the submission.
* 2014-0601: 1.4.5 (Swamp-Ig) for KSP 0.23.5
	+ Small improvement to RegisterOnUpdateEditor
* 2014-0521: 1.4.4 (Swamp-Ig) for KSP 0.23.5
	+ Bugfixes and improvements
* 2014-0518: 1.4.3 (Swamp-Ig) for KSP 0.23.5
	+ New messages for root part adding / removing
	+ FormatMass in MathUtils
* 2014-0517: 1.4.2 (Swamp-Ig) for KSP 0.23.5
	+ Fixed some issues with multiple copies of the same dll being present.
* 2014-0514: 1.4.1 (Swamp-Ig) for KSP 0.23.5
	+ Multiple loads of the exact same KSPAddon when several copies of the same version dll present
* 2014-0503: 1.4.0 (Swamp-Ig) for KSP 0.23.5
	+ A number of updates
* 2014-0428: 1.3.4 (Swamp-Ig) for KSP 0.23.5
	+ Released with [ProceduralParts v0.9.4](https://github.com/Swamp-Ig/ProceduralParts/releases/tag/v0.9.4)
* 2014-0421: 1.3.1 (Swamp-Ig) for KSP 0.23.5
	+ Released with v0.9.2 of ProceduralParts.
