# KSP Enhanced /L :: Changes

* 2024-1117: 2.5.4.5 (Lisias) for KSP >= 1.2
	+ Fixed dumb mistake while solving paths from `IO.Hierarchy.GAMEDATA` 
* 2024-0831: 2.5.4.4 (Lisias) for KSP >= 1.2
	+ Somewhat better error messages, in a (futile, probably) attempt to prevent this [kind of crap](https://www.reddit.com/r/KerbalAcademy/comments/1ejaf9b/houstonerror_contradiction/).
* 2024-0803: 2.5.4.3 (Lisias) for KSP >= 1.2
	+ Fix a stupid mistake on handling `Regex` on Windows pathnames.
		- Embarrassing... :(
* 2024-0505: 2.5.4.2 (Lisias) for KSP >= 1.2
	+ I **Finally** remembered to activate `richText` on the dialogs boxes! #facePalm
* 2024-0428: 2.5.4.1 (Lisias) for KSP >= 1.2
	+ We have moved!
		- The Official Repository is now on https://github.com/KSP-ModularManagement/KSPe
	+ Fixes a incredibly stupid mistake on `KSPe.Util.SystemTools.Find.ByInterfaceName`
		- Not one of my brightest moments, no doubt...
* 2024-0330: 2.5.4.0 (Lisias) for KSP >= 1.2
	+ More UnityEngine and KSP data types serialization/deserialization support for `ConfigNodeWithSteroids`.
		- Proper support for **writting** nodes implemented.
	+ Some copy constructors for the mentioned data types.
	+ Closes issues:
		- [#65](https://github.com/KSP-ModularManagement/KSPe/issues/65) Instrument the KSPe Install Checker to detect when the user deleted the Add'On Directory and kill yourself from `GameData` .
		- [#56](https://github.com/KSP-ModularManagement/KSPe/issues/56) KSP **should not** be run as Privileged User (Administrator on Windows, root on UNIX) .
