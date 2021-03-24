# KSP API Extensions/L :: Changes

* 2021-0323: 2.2.3.1 (Lisias) for KSP >= 1.2
	+ Extending the Abstracted File System for Savegames
	+ Added KSP 1.11.2 to the "Database"
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
