# KSP API Extensions/L :: Changes

* 2021-0818: 2.3.0.4 (Lisias) for KSP >= 1.2
	+ Adding KSP 1.12.1 and 1.12.2 to the "Database"
* 2021-0626: 2.3.0.3 (Lisias) for KSP >= 1.2
	+ Add KSP 1.12.0 to the "Database"
	+ Promotes to RELEASE
	+ Closes issues:
		- [#15](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/15) Ensure this is working fine on KSP 1.12.x
		- [#14](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/14) The installment check is terribly naive
* 2021-0411: 2.3.0.2 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Reworking a Installment Check
		- Some third parties tools are launching KSP on Windows using a pathname with different case from where it is really installed, and this played havoc with the current check.
	+ Closes issues:
		- [#9](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/9) Throw a FATAL when the PWD is set to a different directory than "Origin"
* 2021-0411: 2.3.0.1 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Resurrecting the KSPe.System idea
		- Adding helpers for Enums, implementing in a 3.5 compatible way some facilities from .Net 4 and newer
	+ Closes issues:
		- [#10](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/10) BUG on calculating paths on Symlinks on Windows
		- [#9](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/9) Throw a FATAL when the PWD is set to a different directory than "Origin"
		- [#8](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/8) NRE on instantiating KSPe's Logger when the class has no Namespace
* 2021-0325: 2.3.0.0 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Extending the Abstracted File System for Savegames
	+ Added a XInput Abstraction Layer (currently short circuited to dry run)
	+ Added KSP 1.11.2 to the "Database"
