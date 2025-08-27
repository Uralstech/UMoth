#!/bin/bash

SCHEME="UMoth"
OUTPUT="build"
XCFRAMEWORK_NAME="UMoth.xcframework"
FRAMEWORK_NAME="UMoth.framework"

xcodebuild archive \
  -scheme "$SCHEME" \
  -configuration Release \
  -destination "generic/platform=iOS" \
  -archivePath "$OUTPUT/ios_devices.xcarchive" \
  SKIP_INSTALL=NO BUILD_LIBRARY_FOR_DISTRIBUTION=YES

xcodebuild archive \
  -scheme "$SCHEME" \
  -configuration Release \
  -destination "generic/platform=iOS Simulator" \
  -archivePath "$OUTPUT/ios_simulator.xcarchive" \
  SKIP_INSTALL=NO BUILD_LIBRARY_FOR_DISTRIBUTION=YES

xcodebuild -create-xcframework \
  -archive "$OUTPUT/ios_devices.xcarchive" -framework $FRAMEWORK_NAME \
  -archive "$OUTPUT/ios_simulator.xcarchive" -framework $FRAMEWORK_NAME \
  -output "$OUTPUT/$XCFRAMEWORK_NAME"
