# KSP API Extensions/L :: Changes

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
