source ../CONFIG.inc

PACKAGE=KSPAPIExtensions
TARGETDIR="000_$PACKAGE"
TARGETBINDIR="$TARGETDIR/Plugins"
PROJECTSDIR="$PACKAGE/"

DLLS="KSPe.HMI KSPe.System KSPAPIExtensions"
PD_DLLS="KSPe.KSP.11 KSPe.KSP.12 KSPe.KSP.13 KSPe.KSP.14 KSPe.KSP.15 KSPe.KSP.16 KSPe.KSP.17 KSPe.KSP.18 KSPe.KSP.19 KSPe.KSP.110 KSPe.KSP.111 KSPe.KSP.112 KSPe.Unity.5 KSPe.Unity.2017 KSPe.Unity.2019 KSPe.UI.dev KSPe.UI.Loader"
declare -A PD_SUB_RULES=( ["KSPe.UI.12"]="./12x/KSPe.UI" ["KSPe.UI.14"]="./14x/KSPe.UI" ["KSPe.UI.18"]="./18x/KSPe.UI" )
PD_SUB_DLLS=`{ for K in "${!PD_SUB_RULES[@]}"; do echo $(basename ${PD_SUB_RULES[$K]}); done } | sort -u | tr '\n' ' '`
PD_SUB_DIRS=`{ for K in "${!PD_SUB_RULES[@]}"; do echo $(dirname ${PD_SUB_RULES[$K]}); done } | sort -u | tr '\n' ' '`
GD_DLLS="KSPe"
GD_PRIORITY="000"
LIB_DLLS="KSPe KSPe.UI.dev ${DLLS}"

PROJECT_BRANCH=`git branch | grep \* | cut -d ' ' -f2`
if [ "$PROJECT_BRANCH" == "mestre" ] ; then
	# Release mode
	PROJECT_STATE=""
else
	# Note: Add a leading dash when this value is present!
	PROJECT_STATE=""
fi

VERSION=$( cat $PACKAGE.version | tr '\n' ' ' | sed -n -E 's/^.*?"VERSION\":\{.*"MAJOR":([0-9]+?),.*?"MINOR":([0-9]+?),.*?"PATCH":([0-9]+?),.*?"BUILD":([0-9]+?).*?\}.*$/\1.\2.\3.\4/p' )
