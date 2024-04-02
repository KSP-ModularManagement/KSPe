# KSP Enhanced /L

New extensions and utilities for Kerbal Space Program by Lisias.


## Installation Instructions

To install, place the GameData folder inside your Kerbal Space Program folder. Optionally, you can also do the same for the PluginData (be careful to do not overwrite your custom settings):

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
	+ Delete `<KSP_ROOT>/GameData/000_KSPe.dll`
	+ Delete `<KSP_ROOT>/GameData/001_KSPe.dll`
	+ Delete `<KSP_ROOT>/GameData/000_KSPe`
	+ Delete `<KSP_ROOT>/GameData/000_KSPAPIExtensions`
		- If this thing still exists on your `GameData`! 
* Extract the package's `GameData` folder into your KSP's root:
	+ \<PACKAGE>/GameData --> \<KSP_ROOT>/GameData
* Extract the package's `PluginData` folder (if available) into your KSP's root, taking precautions to do not overwrite your custom settings if this is not what you want to.
	+ \<PACKAGE>/PluginData --> \<KSP_ROOT>/PluginData
	+ You can safely ignore this step if you already had installed it previously and didn't deleted your custom configurable files.
* Install the dependencies, if needed.
	+ See below. 

The following file layout must be present after installation:

```
<KSP_ROOT>
	[GameData]
		[000_KSPe]
			[Plugins]
				...
			CHANGE_LOG.md
			LICENSE
			LICENSE.3rdParties
			LICENSE.KSPAPIExtensions
			LICENSE.KSPe.GPL-2_0
			LICENSE.KSPe.SKL-1_0
			NOTICE
			README.md
			KSPe.version
		000_KSPe.dll
		001_KSPe.dll
		ModuleManager.dll
		...
	KSP.log
	PastDatabase.cfg
	...
```

### Dependencies

* For KSP >= 1.4
	+ [Click Through Blocker](https://forum.kerbalspaceprogram.com/index.php?/topic/170747-151-click-through-blocker/) **Optional**
		- Please note than CTB on KSP >= 1.8 also needs [Toolbar Control](https://forum.kerbalspaceprogram.com/index.php?/topic/169509-19x-toolbar-controller-for-modders/) installed! 
	+ You need to localise and install the best available release for the KSP version you are running.
		- Don't install the latest version for KSP 1.10 on KSP 1.7, **it will not work**.
* There're no dependencies for [1.2 <= KSP <= 1.3.1]

### Suggested Tools

* Module Manager Watch Dog
	+ Checks if Module Manager is correctly installed on your rig, preventing you from running older or improperly installed ones.
	+ Download it [here](https://github.com/KSP-ModularManagement/ModuleManagerWatchDog/releases). 
