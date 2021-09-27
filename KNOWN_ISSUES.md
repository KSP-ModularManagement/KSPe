# KSP API Extensions/L :: Known Issues

* The new 2.4 series breaks binary compatibility with the 2.3!!
	+ I failed on implementing the new features I needed without breaking some legacy API that were meant to be trashed on the next major release, so I trashed them now and called a day. However, this left me between a rock and a hard place: I had to decide to keep binary compatibility or source code compatibility to work around a huge screw up of mine on the Logging mechanism.
	+ I decided to to the Right Thing™ (what ended up being somewhat painful on the short run) in order to keep things sane and avoid exporting shitty code into the MainStream (on the KSPe.Light series).
	+ So this release breaks a lot of the KSPU add'ons - you may want to wait a week while I update them before trying this release. There's nothing really useful on it right now, unless you plan to use KSPe in your own projects - something that is only going to be really feasible from the 2.4 and newer.
	+ TweakScale BETA also needs to be updated, but the current 2.0.0.36 release may work with this stunt - but I'm not going to test it. [I will only support the 2.0.0.37 Beta](https://github.com/net-lisias-ksp/TweakScale/releases/tag/PRERELEASE%2F2.5.0.37) and newer.
	+ My [personal fork of Module Manager](https://github.com/net-lisias-ksp/ModuleManager/releases/) also needs to be updated, so, really, wait until I update the Add'Ons you are using before updating this one.
* The Thread Safe Logging should be considered unreliable at this moment.
	+ It doesn't works on all supported KSP versions. (1.5 is known to halt on it)
	+ And it's plain useless at this moment on KSP >= 1.8
* There's a new feature on Logging exceptions, pinpoint the class, method and line number where the Exception were handled. But it's badly implemented at the moment
	- I failed to realise that I need to find the Exception emitter on the stack, as I'm usually deploying a Facade for the Logs to avoid coupling the code too much on KSPe, and then I can just assume in which position of the stack the emitter is. #facePalm
	- Fixing this will be WiP eventually... 
* There's a nasty bug on Unity's Mono runtime that once an DLL fails to load with an Exception, all the subsequent Reflection calls fails miserably.
	+ You can detect the problem by searching for ADDON BINDER errors preceded by an Exception.
		- If no Exception is found, the problem is not triggered and you can ignore the message.
	+ Currently, the only solution is to keep your KSP instalment free of ADDON BINDER fatal errors.
* Legacy KSP API Extensions (by Swamp-Ig) are still untested.
	+ Revamping the code for legacy Add'On support is currently RiP.

- - - 

* RiP : Research In Progress
* WiP : Work in Progress
