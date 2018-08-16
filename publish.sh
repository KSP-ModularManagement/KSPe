#!/usr/bin/env bash

source ./CONFIG.inc

VERSIONFILE=$PACKAGE.version

scp -i $SSH_ID ./GameData/net.lisias.ksp/$VERSIONFILE $SITE:/$TARGETPATH