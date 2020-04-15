#!/usr/bin/env bash

#Increment version code automatically
#Note: No need to check "increment version code" in appcenter's build config
MANIFEST_PATH="$APPCENTER_SOURCE_DIRECTORY/Qr19.iOS/info.plist"
VERSION_CODE_SHIFT=1700000000
VERSION_CODE=$((VERSION_CODE_SHIFT + APPCENTER_BUILD_ID))
plutil -replace CFBundleVersion -string "$VERSION_CODE" $MANIFEST_PATH
