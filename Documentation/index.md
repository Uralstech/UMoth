---
_layout: landing
---

# UMoth

Google and AppleID authentication for mobile. 

[![openupm](https://img.shields.io/npm/v/com.uralstech.umoth?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.uralstech.umoth/)
[![openupm](https://img.shields.io/badge/dynamic/json?color=brightgreen&label=downloads&query=%24.downloads&suffix=%2Fmonth&url=https%3A%2F%2Fpackage.openupm.com%2Fdownloads%2Fpoint%2Flast-month%2Fcom.uralstech.umoth)](https://openupm.com/packages/com.uralstech.umoth/)

## Installation

This package was made for Unity 6.0 and above. Built and tested in Unity 6.2.

Since this package contains a native AAR plugin, it depends on the [External Dependency Manager for Unity (EDM4U)](https://github.com/googlesamples/unity-jar-resolver) to resolve native dependencies.
You may already have EDM4U if you use Google or Firebase SDKs in your Unity project. If you do not, the installation steps are available
here: [EDM4U - Getting Started](https://github.com/googlesamples/unity-jar-resolver?tab=readme-ov-file#getting-started)

# [OpenUPM](#tab/openupm)

1. Open project settings
2. Select `Package Manager`
3. Add the OpenUPM package registry:
    - Name: `OpenUPM`
    - URL: `https://package.openupm.com`
    - Scope(s)
        - `com.uralstech`
4. Open the Unity Package Manager window (`Window` -> `Package Manager`)
5. Change the registry from `Unity` to `My Registries`
6. Add the `UMoth` package

# [Unity Package Manager](#tab/upm)

1. Open the Unity Package Manager window (`Window` -> `Package Manager`)
2. Select the `+` icon and `Add package from git URL...`
3. Paste the UPM branch URL and press enter:
    - `https://github.com/Uralstech/UMoth.git#upm`
4. Check the instructions for [`Utils.Loggers`](https://uralstech.github.io/Utils.Loggers) and [`Utils.Singleton`](https://uralstech.github.io/Utils.Singleton) to install the dependencies

# [GitHub Clone](#tab/github)

1. Clone or download the repository from the desired branch (master, preview/unstable)
2. Drag the package folder `UMoth/UMoth/Packages/com.uralstech.umoth` into your Unity project's `Packages` folder
3. Check the instructions for [`Utils.Loggers`](https://uralstech.github.io/Utils.Loggers) and [`Utils.Singleton`](https://uralstech.github.io/Utils.Singleton) to install the dependencies

---

## Preview Versions

Do not use preview versions (i.e. versions that end with "-preview") for production use as they are unstable and untested.

## Documentation

See <https://uralstech.github.io/UMoth/DocSource/QuickStart.html> or `APIReferenceManual.pdf` and `Documentation.pdf` in the package documentation for the reference manual and tutorial.
