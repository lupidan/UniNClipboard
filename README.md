# UniNClipboard
UniNClipboard is a minimalistic plugin for Unity 3D that abstracts the implementation of a basic Clipboard/Pasteboard for each platform. It allows to check the contents of the Clipboard, write content into it and, in some platforms, observe changes on it.

| Platform     | Strings            | Observe changes     |
|----------    |----------------    |-----------------    |
| Android      | Read and write     | TODO                |
| iOS          | Read and write     | TODO                |
| OSX          | Read and write     | TODO                |
| Windows   | TODO              | TODO                |
| Linux     | TODO               | TODO                |

# Installation
(WIP) ~~Just import the provided .unitypackage file~~

# Plugin Details
### Android
On Android, to keep it simple, we use the **JNI** implementation provided by Unity. This means no `.aar` library or Android Studio project is provided or required.
### iOS
On iOS, a implementation file called `UniNClipboardHelper.mm` is provided containing all the required implementation.
### Mac OSX
For Mac OSX, a compiled `UniNClipboard.bundle` is provided. If you would like to change or expand the implementation, the Xcode project is located in `OSX/UniNClipboard.xcodeproj`.
**To build a new bundle file**, just press Cmd+B or go to `Product` > `Build`. If the build is successful, the new `UniNClipboard.bundle` will be automatically copied into the example Unity project.

### Windows
TODO?

### Linux
TODO?


# TODO
TODO/Ideas:

- [x] iOS Basic implementation (Only strings)
- [x] Android Basic implementation (Only strings)
- [ ] UnityPackage generation
- [ ] Compatibility for older Unity Versions (no assemblies)
- [x ] OSX Basic implementation (Only strings)
- [ ] Windows Basic implementation (Only strings)
- [ ] Linux Basic implementation (Only strings)
- [ ] Add other types for iOS (Data? URL? HTML?)...
- [ ] Add other types for Android (Data? URL? HTML?)...
- [ ] Add other types for OSX (Data? URL? HTML?)...
- [ ] Add other types for Windows (Data? URL? HTML?)...
- [ ] Add other types for Linux (Data? URL? HTML?)...
