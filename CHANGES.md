# KSP API Extensions/L :: Changes

* 2021-0925: 2.4.0.1 (Lisias) for KSP >= 1.2
	+ Major Overhaul on the KSPe.UI subsystem.
		- Very nice things are possible now.
	+ Small Overhaul on the Logging subsystem.
		- Got rid of lots of some deprecated methods
		- Since we are going to a new Minor numbering, I let the Logging API broke - as I already broke the Assembly one.
	+ Proper handling of fatal errors on KSPe initialisation.
	+ Implementing missing Use Case on "local" DLL loading.
	+ **Complete** Overhaull of the Assembly Loader Tools.
		- **Breaks users of the feature from previous versions!**
		- The new DLL `KSPe.UI.dev.dll` is the one you should link against while compiling. KSPe will link the appropriated one on loading time for you later.
			- The filename is irrelevant, the AssemblyName is what matters, so you can rename it to `KSPe.UI.dll` on your building machine to prevent reconfiguring your building tools. 
* 2021-0922: 2.4.0.0 (Lisias) for KSP >= 1.2
	+ **Ditched** due a major bork on the new API definition. 
