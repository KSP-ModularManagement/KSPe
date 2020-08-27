# KSP API Extensions/L :: Known Issues

* The Thread Safe Logging should be considered unreliable at this moment.
	+ It doesn't works on all supported KSP versions. (1.5 is known to halt on it) 
* There's a new feature on Logging exceptions, pinpoint the class, method and line number where the Exception were handled. But it's badly implemented at the moment
	- I failed to realise that I need to find the Exception emitter on the stack, as I'm usually deploying a Facade for the Logs to avoid coupling the code too much on KSPe, and then I can just assume in which position of the stack the emitter is. #facePalm
	- Fixing this will be WiP soon. 
* There's a nasty bug on Unity's Mono runtime that once an DLL fails to load with an Exception, all the subsequent Reflection calls fails miserably.
	+ You can detect the problem by searching for ADDON BINDER errors preceded by an Exception.
		- If no Exception is found, the problem is not triggered and you can ignore the message.
	+ Currently, the only solution is to keep your KSP instalment free of ADDON BINDER fatal errors.
* Legacy KSP API Extensions (by Swamp-Ig) are still untested.
	+ Revamping the code for legacy Add'On support is currently WiP.

- - - 

* RiP : Research In Progress
* WiP : Work in Progress
