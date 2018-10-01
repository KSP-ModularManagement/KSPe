#!/usr/bin/env bash

source ./CONFIG.inc

clean() {
	local DLL=$1

	rm -f "./bin/Release/$DLL.dll"
	rm -f "./GameData/$TARGETBINDIR/$DLL"
	rm -f "./bin/Debug/$DLL.dll"
}

VERSIONFILE=$PACKAGE.version

rm -f "./GameData/$TARGETDIR/$VERSIONFILE"
rm -f "./GameData/$TARGETDIR/CHANGE_LOG.md"
rm -f "./GameData/$TARGETDIR/README.md"
rm -f "./GameData/$TARGETDIR/*.LICENSE"
for dll in KSPAPIExtensions KSPe; do
    clean $dll
done
