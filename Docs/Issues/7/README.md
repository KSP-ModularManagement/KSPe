There's an nasty issue when running KSPe's logging on Unity 5 and Unity 2017 (2019 appears to be imunne to the problem).

Apparently, while handling OnAwake unity events, the Stack's deepness is terribly reduced - leading to a blunt crash on MacOS (Windows and Linux status is unknown at this point) on heavy weighted  code on this handler.

Disk I/O may or may not be aggravating the problem - I **never** had faced this before since KSPe inception, but I never overloaded the OnAwake event with disk I/O neither. Take this information with a grain of salt.

This problem was detected while refactoring [FMRS /L Unofficial](https://github.com/net-lisias-kspu/FMRS/commit/730f5976e70eba5a608c72b42251c6fe3bf5c88b), and the only known work around at this moment is just to avoid KSPe logging on the OnAwake events. Due the unusual complexity of FMRS, it was decided to just deactivate KSPe logging on the Release configuration (allowing it to be reactivated with conditional compiling for Unity 2019, where the problem doesn't happens - so I can further develop the thing on newer KSPs).

**THIS IS NOT A BUG ON KSPe**. It's a bug on Unity, but since I need to tackle down these things, I'm registering the problem on KSPe.

### IMPORTANT

KSPe uses locks to prevent racing conditions while logging multithreaded code. **The lock** is not involved on the problem for sure, deactivating the lock on a test bed leaded to no improvement on the situation. At worst, the lock mechanism would delay the OnAwake a tiny little bit, and perhaps this could trigger some bug on Unity - and so, the real problem is not (lack of) Stack deepness, and I may had misdiagnosed the cause.

In a way or another, it's another Unity nasty issue to have to cope.

### Evidences

* [CrashDump-Unity5.txt](./CrashDump-Unity5.txt)
* [CrashDump-Unity2017-141.txt](./CrashDump-Unity2017-141.txt)
* [CrashDump-Unity2017-173.txt](./CrashDump-Unity2017-173.txt)

No crash dumps on KSP >= 1.8 because the damned thing works there.


- - - 

This was published originally on [KSPe's Issue 7](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/7), and it's being reproduced here for safety.
