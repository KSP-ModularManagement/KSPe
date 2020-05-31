# KSP API Extensions/L :: Changes

* 2020-0531: 2.1.1.6 (Lisias) for KSP >= 1.2
	+ Refactoring the Log Subsystem:
		- Storming the castle and taking over the whole Unity Logging to KSPe itself
		- See Issue [#4](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/4)
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
* 2020-0104: 2.1.1.1 (Lisias) for KSP >= 1.2
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
