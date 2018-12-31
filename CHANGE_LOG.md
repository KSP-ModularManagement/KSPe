# KSP API Extensions/L :: Change Log

* 2018-1231: 2.1.0.6 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Better Listing Files support.
	+ Some more syntactic sugar
		- Adding generics syntax to ConfigNode's GetValue, making easier to port code from XML (PluginConfiguration) to CFG (ConfigNode) and vice versa
	+ Small refactoring on the class hierarchy
		- Some classes were deprecated, but will be maintained until the version 3
		- No impact to the user base, I'm the only client for this stunt! :D
	+ Removed Max KSP version check.
	+ Fixed a glitch that prevented debug messages from being suppressed.
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
