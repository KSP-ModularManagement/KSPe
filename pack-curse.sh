#!/usr/bin/env bash

echo "CurseForge packaging is not needed anymore, this thing is being redistributed in 'Modular Management' there."
exit 0

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'
source ./CONFIG.inc

clean() {
	rm -fR $FILE
	if [ ! -d Archive ] ; then
		mkdir Archive
	fi
}

pwd=$(pwd)
FILE=${pwd}/Archive/$PACKAGE-$VERSION${PROJECT_STATE}-CurseForge.zip
echo $FILE
clean
cd GameData

zip -r $FILE ./000_$PACKAGE/* -x ".*"
zip -d $FILE "__MACOSX/*" "**/.DS_Store"
cd $pwd
