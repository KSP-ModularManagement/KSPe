source ../CONFIG.inc

PACKAGE=KSPAPIExtensions
TARGETDIR="000_$PACKAGE"
TARGETBINDIR="$TARGETDIR/Plugins"
PROJECTSDIR="$PACKAGE/"

DLLS="KSPe.UI KSPAPIExtensions"
PD_DLLS="KSP.11 KSP.12 KSP.13 KSP.14 KSP.15 KSP.16 KSP.17 KSP.18 KSP.19 KSP.110 KSP.UI.12 KSP.UI.14"
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
