#!/usr/bin/env bash

#Remove app bundle (its already in appcenter conf, and prevents apk distribution)
CSPROJ_PATH="$APPCENTER_SOURCE_DIRECTORY/Qr19.Android/Qr19.Android.csproj"
sed -i "" 's/<AndroidPackageFormat>aab<\/AndroidPackageFormat>//' $CSPROJ_PATH

#Increment version code automatically for google play store
#Note: No need to check "increment version code" in appcenter's build config
MANIFEST_PATH="$APPCENTER_SOURCE_DIRECTORY/Qr19.Android/Properties/AndroidManifest.xml"
VERSION_CODE_SHIFT=0000000000
VERSION_CODE=$((VERSION_CODE_SHIFT + APPCENTER_BUILD_ID))
sed -i "" 's/android:versionCode="[^"]*"/android:versionCode="'$VERSION_CODE'"/' $MANIFEST_PATH
