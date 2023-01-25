# KSP API Extensions/L :: Changes

* 2023-0125: 2.4.3.0 (Lisias) for KSP >= 1.2
	+ Refactoring.
		- Deprecates `KSPe.util.SystemTools.Type.Finder`
		- Substituted by:
			- `KSPe.util.SystemTools.Type.Exists.*`
			- `KSPe.util.SystemTools.Type.Find.*`
	+ Reworks the Installment Checks due issue #44.
	+ Closes issues:
		- [#44](https://github.com/net-lisias-ksp/KSPe/issues/44) `KSPe.Util.SystemTools.Assembly.Loader<T>()` to cope with TweakScale Companion directory structure.
