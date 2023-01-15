# KSP API Extensions/L :: Known Issues

* The Toolbar support incepted on 2.4.1.0 is still embryonic.
	+ Using it on your own add'ons is risky at this moment.
	+ The API is still being worked out, as services to use [Blizzy Toolbar /L](https://github.com/net-lisias-kspu/ksp_toolbar) or emulate [ToolBar Control](https://forum.kerbalspaceprogram.com/index.php?/topic/169509-112x-toolbar-controller-for-modders/) are still RiP.
	+ There's a bug on the Button's life cycle when it is created on the Main Menu.
		- A message like **""[LOG hh:mm:ss.sss] [KSPe.UI] DETAIL: It's embarrasing, but somehow KSPe.UI.Toolbar.State.Control.Update got a NullReferenceException with message Object reference not set to an instance of an object. It's probably am error on handling the \<namespace>_Button's life cycle.""** will appears in the log if you are using DETAIL log level.
		- Other than this log entry, no collateral effects were detected.
		- Yeah, I will fix this. Eventually™. 
* There's a unhandled borderline situation on detecting symlinks
	+ On MS lingo, a `ReparsePoint` is a pathname that contains special metadata instead of a file, and this metadata can be Junction, a MountPoint, a SymLink, etc. It's pretty different from UNICES where these things are completely different entities.
	+ When this mishap happens (as putting the KSP on a MountPoint), the code will misunderstand is as a SymLink and will try to resolve it on UNIX using `readlink`, and then an `Win32Exception` error logged (the exception itself is pretty deceptive too!). The good news is that no collateral effects were detected, as the code fails gracefully.
	+ Handling correctly this mess is currently WiP, as I don't know how to proceed (yet) on MacOS and Linux, but it will be fixed Soon™.
* The new 2.4 series breaks binary compatibility with the 2.3!!
	+ I failed on implementing the new features I needed without breaking some legacy API that were meant to be trashed on the next major release, so I trashed them now and called a day. However, this left me between a rock and a hard place: I had to decide to keep binary compatibility or source code compatibility to work around a huge screw up of mine on the Logging mechanism.
	+ I decided to to the Right Thing™ (what ended up being somewhat painful on the short run) in order to keep things sane and avoid exporting shitty code into the MainStream (on the KSPe.Light series).
	+ So this release breaks a lot of the KSPU add'ons - you may want to wait a week while I update them before trying this release. There's nothing really useful on it right now, unless you plan to use KSPe in your own projects - something that is only going to be really feasible from the 2.4 and newer.
* The Thread Safe Logging should be considered unreliable at this moment.
	+ It doesn't works on all supported KSP versions. (1.5 is known to halt on it)
	+ And it's plain useless at this moment on KSP >= 1.8
* There's a new feature on Logging exceptions, pinpoint the class, method and line number where the Exception were handled. But it's badly implemented at the moment
	- I failed to realise that I need to find the Exception emitter on the stack, as I'm usually deploying a Facade for the Logs to avoid coupling the code too much on KSPe, and then I can just assume in which position of the stack the emitter is. #facePalm
	- Fixing this will be WiP eventually... 
* There's a nasty bug on KSP's `Assembly Loader/Resolver` that once an DLL fails to load with an Exception, all the subsequent Reflection calls fails miserably.
	+ You can detect the problem by searching for ADDON BINDER errors preceded by an Exception.
		- If no Exception is found, the problem is not triggered and you can ignore the message.
	+ Currently, the only solution is to keep your KSP instalment free of ADDON BINDER fatal errors.
* Legacy KSP API Extensions (by Swamp-Ig) are still untested.
	+ Revamping the code for legacy Add'On support is currently RiP.

- - - 

* RiP : Research In Progress
* WiP : Work in Progress
