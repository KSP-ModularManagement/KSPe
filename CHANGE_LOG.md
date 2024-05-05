# KSP Enhanced /L :: Change Log

* 2024-0505: 2.5.4.2 (Lisias) for KSP >= 1.2
	+ I **Finally** remembered to activate `richText` on the dialogs boxes! #facePalm
* 2024-0428: 2.5.4.1 (Lisias) for KSP >= 1.2
	+ We have moved!
		- The Official Repository is now on https://github.com/KSP-ModularManagement/KSPe
	+ Fixes a incredibly stupid mistake on `KSPe.Util.SystemTools.Find.ByInterfaceName`
		- Not one of my brightest moments, no doubt...
* 2024-0330: 2.5.4.0 (Lisias) for KSP >= 1.2
	+ More UnityEngine and KSP data types serialization/deserialization support for `ConfigNodeWithSteroids`.
		- Proper support for **writting** nodes implemented.
	+ Some copy constructors for the mentioned data types.
	+ Closes issues:
		- [#65](https://github.com/KSP-ModularManagement/KSPe/issues/65) Instrument the KSPe Install Checker to detect when the user deleted the Add'On Directory and kill yourself from `GameData` .
		- [#56](https://github.com/KSP-ModularManagement/KSPe/issues/56) KSP **should not** be run as Privileged User (Administrator on Windows, root on UNIX) .
* 2024-0107: 2.5.3.7 (Lisias) for KSP >= 1.2
	+ Fixes a regression due a badly executed job on [issue #63](https://github.com/KSP-ModularManagement/KSPe/issues/63)
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
	+ ***Ditched*** because I marvellously borked the packaging of this one. (sigh)
* 2023-0703: 2.5.3.1 (Lisias) for KSP >= 1.2
	+ ***Ditched*** as a new release were made in less than 24 hours. 
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
		- [#50](https://github.com/KSP-ModularManagement/KSPe/issues/50) Rework the Loading Mechanism.
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
	+ ***DITCHED***  -- yes, this is getting repetitive!!!
* 2023-0130: 2.4.3.1 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2023-0125: 2.4.3.0 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2023-0115: 2.4.2.9 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2022-1128: 2.4.2.8 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2022-1126: 2.4.2.7 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2022-1126: 2.4.2.6 (Lisias) for KSP >= 1.2
	+ ***ditched yet again*** -- this is getting repetitive, no?
* 2022-1125: 2.4.2.5 (Lisias) for KSP >= 1.2
	+ ***ditched***
* 2022-1114: 2.4.2.4 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2022-1113: 2.4.2.3 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
* 2022-1111: 2.4.2.2 (Lisias) for KSP >= 1.2
	+ ***WITHDRAWN***
* 2022-0828: 2.4.2.1 (Lisias) for KSP >= 1.2
	+ Implementing a convenient "Number to Text" helper
* 2022-0824: 2.4.2.0 (Lisias) for KSP >= 1.2
	+ Implementing the `Copy*` stuff from `KSPe.IO.File<T>.<things>`
* 2022-0713: 2.4.1.21 (Lisias) for KSP >= 1.2
	+ Solve some performance issues on the KSPe.IO.Directory "unreparsing"
		- As KSP starts to get crowded, the fight for memory starts to get gory, and the damned Unity's garbage collector is crippled. 
	+ Fixing a mess I did on the Fatal Error messages... :P
		- (coding late night now and then haunts us later!)
* 2022-0712: 2.4.1.20 (Lisias) for KSP >= 1.2
	+ **DITCHED** as I released a new version in less that 24 hours
* 2022-0710: 2.4.1.19 (Lisias) for KSP >= 1.2
	+ Fixed a stupidity on the logging system
	+ Adding some niceties to the `ConfigNodeWithSteroids`
* 2022-0627: 2.4.1.18 (Lisias) for KSP >= 1.2
	+ Better error handling on Button registering on the UI.Toolbar
	+ Some additional Button creator helpers
* 2022-0618: 2.4.1.17 (Lisias) for KSP >= 1.2
	+ Fixing a mishap on the deployment.
* 2022-0529: 2.4.1.16 (Lisias) for KSP >= 1.2
	+ And yet another lame mistake fixed. Big thanks to [lion328](https://github.com/lion328) for their efforts on reporting the problem **AND** working around by obtusity!
	+ Closes
		- [#31](https://github.com/KSP-ModularManagement/KSPe/issues/31) 
* 2022-0425: 2.4.1.15 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Fixed a lame mishap handling `Module Manager`
	+ Some small internal enhancements on the Exceptions and Error dialogs
	+ New `KSPe.IO.Path.GetPath` helper
* 2022-0418: 2.4.1.14 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Implements new helpers on `KSPe.Util.SystemTools`
	+ **Finally** Implementing the Compatibility checking tools!
	+ Refactoring: eating my own dog-food on the code, getting rid of redundancy.
	+ Fixes long standing errors on the URL scattered on the repository.
	+ Works around an absolutely and terrible bug on Mono's Process.Process() IDisposable handling.
		- DAMN!!! 
* 2022-0410: 2.4.1.13 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Resurrects `KSPAPIExtensions.PartMessage` on the legacy KSPApiExtensions.
	+ Resurrects `KSPAPIExtensions.ListenerFerramAerospaceResearch` on the legacy KSPApiExtensions.
* 2022-0221: 2.4.1.12 (Lisias) for KSP >= 1.2
	+ Finally² fixing [Issue #27](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/27). 
* 2022-0221: 2.4.1.11 (Lisias) for KSP >= 1.2
	+ ***ditched*** due a lame mistake.
* 2022-0221: 2.4.1.10 (Lisias) for KSP >= 1.2
	+ Finally fixing [Issue #27](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/27). 
* 2022-0220: 2.4.1.9 (Lisias) for KSP >= 1.2
	+ Trying another fix for [Issue #27](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/27). 
* 2022-0216: 2.4.1.8 (Lisias) for KSP >= 1.2
	+ Trying a fix for [Issue #27](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/27). 
* 2021-1204: 2.4.1.7 (Lisias) for KSP >= 1.2
	+ (Correctly this time) handling on AppLauncher for buttons that are not "alive" on the current scene.
		- The AppLauncher returns `null` when the Button is not applicable on the current scene, but the Controller doesn't care about the current scene
		- The easiest way out of the mess is to just ignore the Button on scenes where the AppLauncher returns `null`, and then try to reregister it on the scene change.
	+ Adds `assert` to the Log subsystem
* 2021-1120: 2.4.1.6 (Lisias) for KSP >= 1.2
	+ Maintenance release
		- Fixes a memory leak and a mishap on forking processes handling.
* 2021-1105: 2.4.1.5 (Lisias) for KSP >= 1.2
	+ Minor improvements:
		- Expands the Toobar.Button's icon support to client side callbacks.
			- Convenient way to support "pseudo-states" when the only place where state changes is on a GUI activated by the button.
			- Can be redundant when runtime States are handled by the `State.Status<T>` classes, but can lead to a code easier to understand when the Button's State can be changed by UI **and** by code state.
		- Avoiding cluttering the savegame folder by moving data into an `AddOns` subdirectory on it on add'ons that save info on the savegame and not on the "global" `<ROOT>/PluginData/*`.
* 2021-1104: 2.4.1.4 (Lisias) for KSP >= 1.2
	+ Maintenance release
		- Safer image loading
		- Safer Toolbar button initialisation.
		- Smarter live cycle handling
	+ Minor improvements
		- Some new convenience methods for `ConfigNode` 
		- Better support for KSPe.UI's `GUI` and `GUILayout` facades.
* 2021-1030: 2.4.1.3 (Lisias) for KSP >= 1.2
	+ Handles yet another borderline situation related to symlinks to directories on the sandboxed file system
	+ Adds a (questionable) work around for add'ons that need to know their "local origin" without accessing a file.
	+ Rework issues:
		- [#16](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/16) The Sandboxed File System is borking on symlinks. Again. 
* 2021-1029: 2.4.1.2 (Lisias) for KSP >= 1.2
	+ Refactoring the thing (yep, one day and I already refactored the stunt)
		- getting rid of some conceptually nice ideas that ended up being a tragedy on trial of real life.
			- In special, mixing the Status and the Controller on the same entity rendered some pretty and sleek code - and terribly confusing after some days of being written.
		- Functional Programming is nice to have some fun, but once the code is written and someone else (what including you after some time) need to maintain the thing, things start to be less fun.
	+ Safer handling of Button lists (getHashCode thingy)
	+ Preventing adding a button twice to the same Toolbar
	+ Exposing AppLauncher's GetAnchor (probably a mistake...)
	+ Fixes handling of some internal events.
	+ **Note**
		- The Toolbar API is still unstable, and are being published as R&D on the KSPU series. 
		- Read the [Known Issues](./KNOWN_ISSUES.md) before risking your SAS with this stunt! :)
	+ Closes issues:
		+ [#16](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/16) The Sandboxed File System is borking on symlinks. Again 
		+ [TS#209](https://github.com/KSP-ModularManagement/TweakScale/issues/209) TweakScale not installed on wrong directory
* 2021-1026: 2.4.1.1 (Lisias) for KSP >= 1.2
	+ **DITCHED** due a major bork on the building process 
* 2021-1020: 2.4.1.0 (Lisias) for KSP >= 1.2
	+ Incepts `KSPe.UI.Toolbar`, a facade to allow the use of any Toolbar support (as long a driver is written for it on KSPe, or it supports KSPe natively).
		- Extremely unstable. Being published for R&D on the KSPU series.
		- Read the [Known Issues](./KNOWN_ISSUES.md) before risking your SAS with this stunt! :)
* 2021-1010: 2.4.0.4 (Lisias) for KSP >= 1.2
	+ Small fixes and enhancements on the Globals subsystem.
* 2021-1007: 2.4.0.3 (Lisias) for KSP >= 1.2
	+ Following up on the stupid mistakes, this release fixes one in the latest version's deployment!
* 2021-1005: 2.4.0.2 (Lisias) for KSP >= 1.2
	+ And yet another pretty stupid mistake detected and fixed an handling the sandboxed filesystem.
* 2021-0926: 2.4.0.1 (Lisias) for KSP >= 1.2
	+ Fixes some dumb mistakes on Log and DLL loader that passed through on the last release
	+ Internalises the TweakScale annotations, as I intend to use them on the Companions, and so I need them on the KSPe.Light.
* 2021-0922: 2.4.0.0 (Lisias) for KSP >= 1.2
	+ Implementing missing Use Case on "local" DLL loading.
	+ **Complete** Overhaull of the Assembly Loader Tools.
		- **Breaks users of the feature from previous versions!**
* 2021-0912: 2.3.1.0 (Lisias) for KSP >= 1.2
	+ Fixing a long standing pretty stupid mistake on calculating "abstracted" paths...
	+ Adding "local" DLL loading facilities.
* 2021-0818: 2.3.0.4 (Lisias) for KSP >= 1.2
	+ Adding KSP 1.12.1 and 1.12.2 to the "Database"
* 2021-0626: 2.3.0.3 (Lisias) for KSP >= 1.2
	+ Add KSP 1.12.0 to the "Database"
	+ Promotes to RELEASE
	+ Closes issues:
		- [#15](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/15) Ensure this is working fine on KSP 1.12.x
		- [#14](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/14) The installment check is terribly naive
* 2021-0411: 2.3.0.2 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Reworking a Installment Check
		- Some third parties tools are launching KSP on Windows using a pathname with different case from where it is really installed, and this played havoc with the current check.
	+ Closes issues:
		- [#9](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/9) Throw a FATAL when the PWD is set to a different directory than "Origin"
* 2021-0411: 2.3.0.1 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Resurrecting the KSPe.System idea
		- Adding helpers for Enums, implementing in a 3.5 compatible way some facilities from .Net 4 and newer
	+ Closes issues:
		- [#10](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/10) BUG on calculating paths on Symlinks on Windows
		- [#9](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/9) Throw a FATAL when the PWD is set to a different directory than "Origin"
		- [#8](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/8) NRE on instantiating KSPe's Logger when the class has no Namespace
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
		- [#6](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/6) The Sandboxed File System is borking on symlinks
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
	+ Closes [Issue #6 The Sandboxed File System is borking on symlinks](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/6)
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
		- Read the [Documentation](https://github.com/KSP-ModularManagement/KSPAPIExtensions/blob/mestre/Docs/KSPe.md) for more information. 
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
		- See Issue [#4](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/4)
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
		- See Issue [#3](https://github.com/KSP-ModularManagement/KSPAPIExtensions/issues/3)
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
		- Critical for [ModuleManager](https://github.com/KSP-ModularManagementu/ModuleManager). 
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

