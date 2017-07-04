# UnityAndroidPermissions
Starting with Android Marshmallow (Android 6), Google introduced Runtime permissions system where the user is asked to grant a permission in runtime rather than doing that during installation of the app.
However, Unity for Android is not supporting it out of the box because:
-	the corresponding Android API requires an Activity, when Unity can run without it. All non-Activity Unity applications are not supported for that reason
-	the plugins may add a dangerous permission and not have the code to handle it correctly, thus causing the whole app to crash
This is the reason why Unity prompts the user for all the permissions on startup. This behavior is the safest and most compatible.

However, Google requires the runtime permission system to be implemented to get featured on Google Play. To let the user implement it (and take the responsibility), the Unity's dialog on startup can be suppressed by adding "unityplayer.SkipPermissionsDialog"="true" metadata tag to application or activity section of the Android Manifest.

This plugin is one of the Android runtime permissions for Unity implementations. It provides the API to check the status of a permission, request a set of permissions and get a callback with the result.

## API

`AndroidPermissionsManager` is the class which provides you the following methods:
-	`bool IsPermissionGranted(string permissionName)` to check the status of a permission
-	`void RequestPermission(string[] permissionNames, AndroidPermissionCallback callback)` to query for an array of permissions. Pass `AndroidPermissionCallback` object or an object of derived class with your own callback implementation. `void AndroidPermissionCallback.OnPermissionGranted(string permissionName)` is called when a permission is granted, `void OnPermissionDenied(string permissionName)` - when a permission is denied.

## Usage
0.	Should work with Unity 5.3+. Please report an issue if it does not for you
1.	Add the plugin to your project. You need the manifest, the AAR and the C# script (Assets/Plugins/Android/AndroidManifest.xml, Assets/Plugins/Android/unityandroidpermissions.aar and Assets/AndroidPermissionsManager.cs)
2.	Please pay attention to the manifest - you may want to use the one provided here or, if you have your own base manifest, please make sure to add "unityplayer.SkipPermissionsDialog"="true" metadata tag to application or activity section
3.	Use the C# API in your scripts to check the permission status and request it if necessary, right before you actually need this permission
4.	Enjoy

## How to Build
Use Android Studio to build the AAR from the source in src/ directory.

## See Also
Please refer to Google documentation for more details: https://developer.android.com/training/permissions/requesting.html
