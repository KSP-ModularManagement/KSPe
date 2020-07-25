# KSP API Extensions /L

## KSPe - KSP enhanced

Offer standardised, KSP version abstracted features, utilities and helpers to help the fellow KSP Add'On Author to seamless support part, present and (hopefully) future versions of KSP without needing even a recompile.

Manual is Work in Progress on the moment.

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
