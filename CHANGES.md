# KSP API Extensions/L :: Changes

* 2021-1204: 2.4.1.7 (Lisias) for KSP >= 1.2
	+ (Correctly this time) handling on AppLauncher for buttons that are not "alive" on the current scene.
		- The AppLauncher returns `null` when the Button is not applicable on the current scene, but the Controller doesn't care about the current scene
		- The easiest way out of the mess is to just ignore the Button on scenes where the AppLauncher returns `null`, and then try to reregister it on the scene change.
	+ Adds `assert` to the Log subsystem.
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
		- [#16](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/16) The Sandboxed File System is borking on symlinks. Again. 
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
		+ [#16](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/16) The Sandboxed File System is borking on symlinks. Again 
		+ [TS#209](https://github.com/net-lisias-ksp/TweakScale/issues/209) TweakScale not installed on wrong directory
* 2021-1020: 2.4.1.0 (Lisias) for KSP >= 1.2
	+ Incepts `KSPe.UI.Toolbar`, a facade to allow the use of any Toolbar support (as long a driver is written for it on KSPe, or it supports KSPe nativelly).
		- Extremely unstable. Being published for R&D on the KSPU series.
		- Read the [Known Issues](./KNOWN_ISSUES.md) before risking your SAS with this stunt! :)
