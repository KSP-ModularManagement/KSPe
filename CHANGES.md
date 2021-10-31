# KSP API Extensions/L :: Changes

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
