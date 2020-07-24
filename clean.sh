#!/usr/bin/env bash

source ./CONFIG.inc

clean() {
	local DLL=$1.dll

	rm -f "./GameData/$DLL"
	rm -f "./GameData/$TARGETBINDIR/$DLL"
	rm -f "./GameData/$TARGETBINDIR/PluginData/$DLL"
	rm -f "$LIB/$DLL"
	rm -f "${KSP_DEV}/GameData/$DLL"
	rm -f "${KSP_DEV}/GameData/$TARGETBINDIR/PluginData/$DLL"
}

VERSIONFILE=$PACKAGE.version

rm -fR "./bin"
rm -fR "./obj"
rm -f "./GameData/$TARGETDIR/$VERSIONFILE"
rm -f "./GameData/$TARGETDIR/CHANGE_LOG.md"
rm -f "./GameData/$TARGETDIR/README.md"
rm -f "./GameData/$TARGETDIR/LICENSE*"
for dll in $DLLS 000_KSPe ; do
    clean $dll
done
