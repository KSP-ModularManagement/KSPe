# KSP API Extensions/L :: Changes

* 2019-0718: 2.1.0.14 (Lisias) for KSP >= 1.2
	+ Adding formal dependency for [Click Through Blocker](https://forum.kerbalspaceprogram.com/index.php?/topic/170747-151-click-through-blocker/) 1.7.2 on KSPe.UI.
	+ New UI helpers:
		- Message Box : Big, centered and modal Window
		- Alert Box with close countdown : smaller, positionable Window.
	+ New Image Utils
		- Universal Texture Loader (works on any Unity) 
* 2019-0718: 2.1.0.13 (Lisias) for KSP >= 1.2 **WITHDRAWN**
	+ Withdrawn due a stupid mistake on compiling against a beta release of an third-party Add'On
* 2019-0524: 2.1.0.10 (Lisias) for KSP >= 1.2
	+ KSPe goes gold after 8 months in development! :)
	+ Adding a proxy to [Click Through Blocker](https://forum.kerbalspaceprogram.com/index.php?/topic/170747-151-click-through-blocker/), what will allow seamless integration with it and new solutions
		- Including selecting the desired one in runtime!
		- Will raise the minimum supported KSP to the installed Click Through Blocker supported version.
* 2019-0503: 2.1.0.9 (Lisias) for KSP >= 1.2 PRE-RELEASE
	+ Adding support for headers when saving ConfigNodes.
	+ Adding local cache support to the *.Solve functions (saving some processing on the client side, by avoiding hitting the Reflection stuff more than one time for file)
