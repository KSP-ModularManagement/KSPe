# KSP API Extensions/L :: Changes

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
* 2020-0825: 2.2.1.2 (Lisias) for KSP >= 1.2
	+ **WITHDRAWN** due a silly mistake.
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
