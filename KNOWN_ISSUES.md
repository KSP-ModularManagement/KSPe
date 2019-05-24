# KSP API Extensions/L :: Known Issues

* There's a nasty bug on Unity's Mono runtime that once an DLL fails to load with an Exception, all the subsequent Reflection calls fails miserabily.
	+ You can detect the problem by searching for ADDON BINDER errors preceded by an Exception.
		- If not Exception is found, the problem is not triggered and you and ignore the message.
	+ Currently, the only solution is to keep your KSP installment free of ADDON BINDER fatal errors.
* Legacy KSP API Extensions (by Swamp-Ig) are still untested.
	+ Revamping the code for legacy Add'On support is currently WiP.

- - - 

* RiP : Research In Progress
* WiP : Work in Progress
