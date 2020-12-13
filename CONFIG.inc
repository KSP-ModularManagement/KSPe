source ../CONFIG.inc

PACKAGE=KSPAPIExtensions
TARGETDIR="000_$PACKAGE"
TARGETBINDIR="$TARGETDIR/Plugins"
PROJECTSDIR="$PACKAGE/"

DLLS="KSPe.UI KSPAPIExtensions CSharpEmulator"
PD_DLLS="KSPe.KSP.11 KSPe.KSP.12 KSPe.KSP.13 KSPe.KSP.14 KSPe.KSP.15 KSPe.KSP.16 KSPe.KSP.17 KSPe.KSP.18 KSPe.KSP.19 KSPe.KSP.110 KSPe.UI.12 KSPe.UI.14 Unity.5 Unity.2017 Unity.2019"
GD_DLLS="KSPe"
GD_PRIORITY="000"

PROJECT_BRANCH=`git branch | grep \* | cut -d ' ' -f2`
if [ "$PROJECT_BRANCH" == "mestre" ] ; then
	# Release mode
	PROJECT_STATE=""
else
	# Note: Add a leading dash when this value is present!
	PROJECT_STATE=-PreRelease
fi

VERSION=$( cat $PACKAGE.version | tr '\n' ' ' | sed -n -E 's/^.*?"VERSION\":\{.*"MAJOR":([0-9]+?),.*?"MINOR":([0-9]+?),.*?"PATCH":([0-9]+?),.*?"BUILD":([0-9]+?).*?\}.*$/\1.\2.\3.\4/p' )
