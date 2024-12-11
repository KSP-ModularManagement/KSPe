# KSP Enhanced /L :: Changes

* 2024-1211: 2.5.5.0 (Lisias) for KSP >= 1.2
	+ Some serious (long due) refactorings on `IO.Hierarchy.*GAMEDATA*`
		+ Adds `KSPe.IO.SaveGameMonitor` - the only reliable way, from this point, to known when a savegame is ready to be used from `KSPe` point of view.
			- TL;DR: KSP only populates `HighLogic.fetch.GameSaveFolder` some time after `HighLogic.CurrentGame` is available. `GameEvents.onGameStatePostLoad` is called before that too, rendering us high and dry if we need to know the directory KSP is using for the savegame when our Assemblies are `Start`ed immediately after the savegame is loaded.
			- This Monitor will, well, monitor the `GameSaveFolder` and will call all its listeners as soon as this data is available.
	+ Adds a check agains having Add'Ons installed inside an inner `GameData` inside the canonical `GameData`.
	+ Closes issues:
		- [#74](https://github.com/KSP-ModularManagement/KSPe/issues/74) The helper `KSPe.IO.Hierarchy.SAVE.Solve()` is botched!
		- [#71](https://github.com/KSP-ModularManagement/KSPe/issues/71) Add a Check for the presence of a `GameData` inside the `GameData`.
