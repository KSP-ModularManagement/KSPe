# KSP Enhanced /L :: Changes

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
		- [#37](https://github.com/net-lisias-ksp/KSPe/issues/37) Dont get screwed by `System.IO.Directory.GetCurrentDirecrtory()` again. 
	+ Closes issues:
		- [#44](https://github.com/net-lisias-ksp/KSPe/issues/44) `KSPe.Util.SystemTools.Assembly.Loader<T>()` to cope with TweakScale Companion directory structure.
		- [#40](https://github.com/net-lisias-ksp/KSPe/issues/40) Bug on `ConfigNodeWithSteroids.HasNode`
		- [#37](https://github.com/net-lisias-ksp/KSPe/issues/37) Dont get screwed by `System.IO.Directory.GetCurrentDirecrtory()` again. 
		- [#18](https://github.com/net-lisias-ksp/KSPe/issues/18) Implement a Installment check for (yet) more bordelines use cases
* 2023-0212: 2.5.0.0 (Lisias) for KSP >= 1.2
	+ ***DITCHED***
