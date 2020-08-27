# KSP API Extensions /L

## KSPe - KSP enhanced

Offer standardised, KSP version abstracted features, utilities and helpers to help the fellow KSP Add'On Author to seamless support part, present and (hopefully) future versions of KSP without needing even a recompile.

Manual is Work in Progress on the moment.

### Global/Local Configuration

Allows to set up some KSPe parameters on an Add'On by Add'On basis.

Current supported Parameters:

* **DebugMode** : boolean default `False`
	+ A flag to allow activating or suppressing diagnostic features at runtime. 
	+ Its use and appliance are entirely at the client add'on author discretion.
	+ To do not be confused with the DEBUG compiling mode, it's completely unrelated.
* Log : structure - Holding Logging parameters
	+ **Level** : int default `3`
		- Defines the logging level to be written on the KSP.log.
		- Acceptable values:
			- 0 = OFF : Suppress all log messages. Only "forced" will be issued. Unwise to use.
			- 1 = ERROR : Only errors & exceptions log entries are issued.
			- 2 = WARNING : Allows warnings log entries too.
			- 3 = INFO : Allows informative log entries too.
			- 4 = DETAIL : Allows detailed log entries too.
			- 5 = TRACE : Allows tracing log entries too. Will probably flood the KSP.log when supported by the Add'On!
		- The use if these logging levels is entirely at the client add'on author's discretion.
	+ **ThreadSafe** : boolean default `False`
		- Activates the thread safe logging routines, allowing log messages to be issued from any concurrent thread.

These configurations should be placed on a file called `KSPe.cfg` on the `PluginData` subdirectory on the `KSP root` directory (i.e., as a "brother" of `GameData`). The file format should be as the example:

```
KSPe
{
	DebugMode = False
	Log
	{
		ThreadSafe = False
		LogLevel = 5
	}
	LOCAL
	{
		ModuleManager
		{
			DebugMode = False
			Log
			{
				ThreadSafe = False
				LogLevel = 4
			}
		}
		TweakScale
		{
			DebugMode = False
			Log
			{
				ThreadSafe = False
				LogLevel = 3
			}
		}
	}
}
```
The add'on node's name must be the C#'s namespace of its code, **not** the name the Author used to publish the thing - besides being, usually, the same.


### Module Manager KSP version detection

Allows patches to detect using `:NEEDS` the KSP Major version in use using the following tags:

* KSP-1.1
* KSP-1.2
* KSP-1.3
* KSP-1.4
* KSP-1.5
* KSP-1.6
* KSP-1.7
* KSP-1.8
* KSP-1.9
* KSP-1.10

The tags are added to every previous KSP until the current one, so you can add features cumulatively instead of explicitly supporting each version individually.

You can use the `!` operator do restrict versions. For example, `:NEEDS[KSP-1.3,!KSP-1.5]` will restrict the patch to KSP versions between 1.3.0 and 1.4.5. Restrictions on the KSP Minor version are possible, but at this time is not known if it is really needed.

Besides not being as convenient and straight forwarded as MM [Issue #116](https://github.com/sarbian/ModuleManager/issues/116), it does the trick **now** while MM is way behind schedule on it.

And, of course, **IT WORKS WITH "OFFICIAL" Module Manager** - including older versions downto [4.0.2](https://github.com/net-lisias-ksp/ModuleManager/commit/c43268f5d4ece8ef2f5c5c55537cd12db081c6cf). :)

Of course, you may want to use [Module Manager /L Experimental](https://github.com/net-lisias-ksp/ModuleManager/releases) in order to get full MM current features down to KSP 1.2.2 (the older supported by now). Yes, it uses KSPe. :)

### Module Manager Patch Cache detection

KSPe detects when the MM cache had to be rebuilt, and so your Add'On can choose to do things only then new patches are applied (as Sanity Checks!).

Use the boolean `KSPe.Util.ModuleManagerTools.IsLoadedFromCache` to check if MM had loaded the patches from the cache (`true`) or if it was rebuilt from scratch (`false`). Note that buggy patches will prevent MM to create the cache at all.

When using MM /L Experimental the cache detection is accurate. Other MM versions have the timestamp from the cache file checked, and it's assumed loading from cache if the cache file is older than 1 hour.

