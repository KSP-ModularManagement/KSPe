# KSP API Extensions/L :: Known Issues

* There's a nasty bug on Unity's Mono runtime that once an DLL fails to load with an Exception, all the subsequent Reflection calls fails miserably.
	+ You can detect the problem by searching for ADDON BINDER errors preceded by an Exception.
		- If not Exception is found, the problem is not triggered and you and ignore the message.
	+ Currently, the only solution is to keep your KSP installment free of ADDON BINDER fatal errors.
* KSPe.UI doesn't loads on KSP <= 1.3.1 due an idiotic mistake on handling dependencies. Delete it from `000_KSPAPIExtensions/Plugins` as a temporary measure.
* Legacy KSP API Extensions (by Swamp-Ig) are still untested.
	+ Revamping the code for legacy Add'On support is currently WiP.

- - - 

* RiP : Research In Progress
* WiP : Work in Progress
