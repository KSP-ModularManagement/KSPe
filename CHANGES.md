# KSP API Extensions/L :: Changes

* 2022-1126: 2.4.2.7 (Lisias) for KSP >= 1.2
	+ Implements `KSPe.IO.Directory.*` calls.
	+ Implements some missing Use Cases from 2.4.2.4, when AppRoot replaced Origin - but I let code relying on `pwd` pass trough.
		- As well new use cases that I never dreamed about...
	+ Closes issue (again):
		- [#37](https://github.com/net-lisias-ksp/KSPe/issues/37) Dont get screwed by `System.IO.Directory.GetCurrentDirecrtory()` again. 
* 2022-1114: 2.4.2.4 (Lisias) for KSP >= 1.2
	+ Fixing a regression on 2.4.2.3 about the filesystem sandbox that affected SteamDeck and Steam on Linux. Power MacOS users also affected. 
	+ Switches the internal pathname resolving from `Origin()` to the new `AppRoot()`, making possible to client add'ons to kinda of keep working even if the `pwd` is set to a different path than `Origin()` - what will still break KSP anyway, but whatever. It's not by job to prevent people from shooting their feet if this is what they really wanna do.
		+ KSPe current behaviour will not change, it still yells if `pwd` is different from `Origin()`.
		+ In fact, KSPe now also checks if `AppRoot()` is different from `Origin()` the same, yelling when the check fails.
		+ Paths will be normalised the same, preventing clients from "jail breaking" the KSP's file system hierarchy.
	+ Makes some error messages easier to understand, as well fixes some pathnames to be useable on Windows. Thanks, [@Hebarusan](https://github.com/HebaruSan)!
	+ Implementing a convenient "Number to Text" helper
* 2022-0824: 2.4.2.0 (Lisias) for KSP >= 1.2
	+ Implementing the `Copy*` stuff from `KSPe.IO.File<T>.<things>`
