# KSP API Extensions/L :: Changes

* 2021-0208: 2.2.2.3 (Lisias) for KSP >= 1.2
	+ Some pretty stupid omission fixed on handling the installment checks
	+ Closes (again):
		- [#6](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/6) The Sandboxed File System is borking on symlinks
* 2021-0104: 2.2.2.2 (Lisias) for KSP >= 1.2
	+ Nicer tools to handle KSP versions
* 2020-1220: 2.2.2.1 (Lisias) for KSP >= 1.2
	+ Adding formal support for KSP 1.11.0
	+ Adding specialised support for Unity versions (5, 2017 & 2019 at this time)
		- This removes a lot of duplicated code from the KSP's specialised support 
	+ Incepting a new helper, `C# Emulator`, to backport to Mono 3.5 classes and helpers from 4.x, easing backporting for Add'Ons. 
		- Minimalistic at this time, but this will grow with time. 
* 2020-1219: 2.2.2.0 (Lisias) for KSP >= 1.2
	+ ***Withdrawn*** due an idiotic mistake on the Instalment check... =/
