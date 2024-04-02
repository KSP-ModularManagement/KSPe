# KSP Enhanced /L :: Changes

* 2024-0330: 2.5.4.0 (Lisias) for KSP >= 1.2
	+ More UnityEngine and KSP data types serialization/deserialization support for `ConfigNodeWithSteroids`.
		- Proper support for **writting** nodes implemented.
	+ Some copy constructors for the mentioned data types.
	+ Closes issues:
		- [#65](https://github.com/KSP-ModularManagement/KSPe/issues/65) Instrument the KSPe Install Checker to detect when the user deleted the Add'On Directory and kill yourself from `GameData` .
		- [#56](https://github.com/KSP-ModularManagement/KSPe/issues/56) KSP **should not** be run as Privileged User (Administrator on Windows, root on UNIX) .
* 2024-0213: 2.5.3.8 (Lisias) for KSP >= 1.2
	+ More friendly approach on handling issues when CKAN is present.
* 2024-0107: 2.5.3.7 (Lisias) for KSP >= 1.2
	+ Fixes a regression due a badly executed job on [issue #63](https://github.com/KSP-ModularManagement/KSPe/issues/63).	 
	+ Reworks issues:
		- [#63](https://github.com/KSP-ModularManagement/KSPe/issues/63) KSPe.External.AddOnVersionChecker method `LoadFrom(string)` royally screwed the `LoadFrom(string, string =null)` .
* 2024-0106: 2.5.3.6 (Lisias) for KSP >= 1.2
	+ ***DITCHED*** as I issued a new release less than 24 hours after releasing this.
* 2023-1118: 2.5.3.5 (Lisias) for KSP >= 1.2
	+ Deactivating the Elevated Privileges checking, as there's something fishy on the C# Libraries that handle it (as usual)
	+ Related issues:
		- [#64](https://github.com/KSP-ModularManagement/KSPe/issues/64) Error on KSP launch .
* 2023-1116: 2.5.3.4 (Lisias) for KSP >= 1.2
	+ Warns user if he is running KSP as a privileged user - seriously, guys, don't do it!
	+ More robust `ApplicationRootPath` calculation.
	+ Removes `KSPApiExtensions` from the main codebase, as this thing is not being properly maintained and it's there just to keep old things running while I don't update them.
	+ Fixes a very stupid decision on creating a new helper on `KSPe.External.AddOnVersionChecker`.
	+ Closes issues:
		- [#63](https://github.com/KSP-ModularManagement/KSPe/issues/63) KSPe.External.AddOnVersionChecker method `LoadFrom(string)` royally screwed the `LoadFrom(string, string =null)` .
		- [#60](https://github.com/KSP-ModularManagement/KSPe/issues/60) Remove KSPAPIExtensions from this repository, moving it ao another one as I did with TinyJSON .
		- [#58](https://github.com/KSP-ModularManagement/KSPe/issues/58) get_dataPath is not allowed to be called from a MonoBehaviour constructor .
		- [#54](https://github.com/KSP-ModularManagement/KSPe/issues/54) KSP **should not** be run as Privileged User (Administrator on Windows, root on UNIX) .
	+ Related issues:
		- [TS#318](https://github.com/TweakScale/TweakScale/issues/318) TweakScale cannot check the presens of TweakScaleCompanion .
		- [TS#304](https://github.com/TweakScale/TweakScale/discussions/304) Possible fix for the Tweakscale Hierarchy bug .
* 2023-0704: 2.5.3.3 (Lisias) for KSP >= 1.2
	+ More sensible updating mechanism, coping with how Windows handles DLL files once they are loaded.
	+ Fixes some mishaps on some Standard Dialogs.
	+ Fixes a bug on the KSPe's Install Checker/Update Tool
		- **You need to manually remove the older `<KSP-ROOT>/GameData/000_KSPe.dll` file if existent!** 
* 2023-0703: 2.5.3.2 (Lisias) for KSP >= 1.2
	+ ***Dithed*** because I marvellously borked the packaging of this one. (sigh)
* 2023-0703: 2.5.3.1 (Lisias) for KSP >= 1.2
	+ ***Dithed*** as a new release were made in less than 24 hours. 
* 2023-0629: 2.5.3.0 (Lisias) for KSP >= 1.2
	+ New Standard Error Handler for the lazy developer! ;) 
		- With new Standard Error/Alert/Warning Dialogs too!
	+ Refactorings on the `Exception` hierarchy, making easier to create standard handlers
	+ Small enhancements:
		- Stack dump method on the Log
		- Allowing rich text on the KSPe.UI's Message Boxes
		- Allowing the Message Boxes to be scalable by User Settings
	+ Small fixes:
		- Adding names to the loaded textures (for better debuggability)
		- Throwing an error if trying to load the same Assembly again.
		- Stupid mistakes galore fixed
	+ Related issues:
		- [TSCF#6](https://github.com/TweakScale/Companion_Frameworks/issues?q=is%3Aissue+is%3Aclosed) Crash to desktop with version 2023.03.28.3
	+ Closes issues:
		- [#54](https://github.com/KSP-ModularManagement/KSPe/issues/54) `KSPe.InstallChecker` is not being able to show its Modals .
		- [#51](https://github.com/KSP-ModularManagement/KSPe/issues/51) Prevent KSPe from initialising itself twice.
* 2023-0514: 2.5.2.3 (Lisias) for KSP >= 1.2
	+ Fixes a design flaw on the `KSPe.InstallChecker`'s update mechanism that wasn't being able to tell the user to restart KSP due an updated `000_KSPe.dll`
		- This fix is mandatory for CurseForge users, otherwise future KSPe updates will be troublesome for them.
		- Everybody else should not be affected. 
	+ Fixes another flaw on the thing, working around the KSP >= 1.8 `Assembly Loader/Resolver` major screw up.
	+ Closes issues:
		- [#54](https://github.com/KSP-ModularManagement/KSPe/issues/50) Rework the Loading Mechanism.
* 2023-0513: 2.5.2.2 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2023-0513: 2.5.2.1 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2023-0321: 2.5.2.0 (Lisias) for KSP >= 1.2
	+ Moving CkanTools to a external library (KSPe.External) where 3rd party support will be implemented, avoiding cluttering the main Assembly with non core business stuff
	+ Incepting support for KSP-AVC version files.
	+ Preventing idiots like me from trying to find a `Type` by using itself as parameter on `KSPe.Util.SystemTools.Type.Find.By` :P
* 2023-0317: 2.5.1.1 (Lisias) for KSP >= 1.2
	+ Adding a library to easily handle JSONs (Tiny-JSON) into the toolset.
	+ Incepting tools to handle installers.
	+ A few helpers for `KSP.UI`
* 2023-0316: 2.5.1.0 (Lisias) for KSP >= 1.2
	+ ***DITCHED*** due an incredibly lame mistake!
* 2023-0313: 2.5.0.1 (Lisias) for KSP >= 1.2
	+ Rebranding the thing into `KSPe Enhanced /L`, finally decoupling from the `KSPApiExtensions` heritage, now delegated as a secondary (and optional) feature.
	+ Removes deprecating artefacts marked for removal on 2.5 series
	+ Makes the thing deliverable on CurseForge **#HURRAY!!**
	+ Makes the thing deliverable on CKAN also.
		- `KSPe.InstallChecker` should not be installed under CKAN controlled KSP installments
	+ **This is a deal breaker release!!**
		- Add'Ons compiled against 2.4.x and 2.3.x relying on the removed artefacts **will break**. 
	+ Refactoring (should bad been done in the last release!)
		- Deprecates `KSPe.util.SystemTools.Assembly.Finder`
		- Substituted by:
			- `KSPe.util.SystemTools.Assembly.Exists.*`
			- `KSPe.util.SystemTools.Assenbly.Find.*`
	+ Refactoring.	 
		- Deprecates `KSPe.util.SystemTools.Type.Finder`
		- Substituted by:
			- `KSPe.util.SystemTools.Type.Exists.*`
			- `KSPe.util.SystemTools.Type.Find.*`
	+ Reworks the Installment Checks due issue #44.
	+ Bug fixes.
	+ Implements yet some more missing Use Cases from 2.4.2.4, when AppRoot replaced Origin - but I let code relying on `pwd` pass trough.
	+ Implements `KSPe.IO.Directory.*` calls.
	+ Implements some missing Use Cases from 2.4.2.4, when AppRoot replaced Origin - but I let code relying on `pwd` pass trough.
		- As well new use cases that I never dreamed about...
	+ Fixing a regression on 2.4.2.3 about the filesystem sandbox that affected SteamDeck and Steam on Linux. Power MacOS users also affected. 
	+ Switches the internal pathname resolving from `Origin()` to the new `AppRoot()`, making possible to client add'ons to kinda of keep working even if the `pwd` is set to a different path than `Origin()` - what will still break KSP anyway, but whatever. It's not by job to prevent people from shooting their feet if this is what they really wanna do.
		+ KSPe current behaviour will not change, it still yells if `pwd` is different from `Origin()`.
		+ In fact, KSPe now also checks if `AppRoot()` is different from `Origin()` the same, yelling when the check fails.
		+ Paths will be normalised the same, preventing clients from "jail breaking" the KSP's file system hierarchy.
	+ Reworks:
		- [#37](https://github.com/KSP-ModularManagement/KSPe/issues/37) Dont get screwed by `System.IO.Directory.GetCurrentDirecrtory()` again. 
	+ Closes issues:
		- [#44](https://github.com/KSP-ModularManagement/KSPe/issues/44) `KSPe.Util.SystemTools.Assembly.Loader<T>()` to cope with TweakScale Companion directory structure.
		- [#40](https://github.com/KSP-ModularManagement/KSPe/issues/40) Bug on `ConfigNodeWithSteroids.HasNode`
		- [#37](https://github.com/KSP-ModularManagement/KSPe/issues/37) Dont get screwed by `System.IO.Directory.GetCurrentDirecrtory()` again. 
		- [#18](https://github.com/KSP-ModularManagement/KSPe/issues/18) Implement a Installment check for (yet) more bordelines use cases
* 2023-0212: 2.5.0.0 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
