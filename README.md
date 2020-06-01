# Setup
### Installing Unity

Basic instructions by Oculus on how to install Unity and set it up for Quest development:
[How to Setup Unity to develop for Oculus Quest](https://developer.oculus.com/documentation/unity/book-unity-gsg/)

After cloning the repo add it in the Unity Hub.
Use one of the following versions to start editing the project:
  - 2019.2.8f1 or newer
  - Version 2019.3 or newer not fully compatible.

In the Build Settings change the plattform to android.

After connecting your Oculus Quest, you can Build and Run the project to test it.

### Project is not working

If your cloning the repository, the following instructions should not be necessary.  
If you still run into problems, make sure to follow the following instructions:

#### I can't install the package on my Quest
[How to connect your quest and setup the build settings](https://developer.oculus.com/documentation/unity/unity-enable-device/)

#### AndroidManifest is missing or hand-tracking does not work
If for some reason the file Assets > Plugins > Android > AdroidManifest.xml does not exist here is how to create it: 

In the Unity Menubar click Oculus > Tools > Create store-compatible AndroidManifest.xml. This will create a folder Plugins > Android with the file AndroidManifest.xml. To enable Handtracking we need to edit this file. Copying and pasting the following text should work:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:installLocation="auto">
  <application android:label="@string/app_name" android:icon="@mipmap/app_icon" android:allowBackup="true">
    <activity android:theme="@android:style/Theme.Black.NoTitleBar.Fullscreen" android:configChanges="locale|fontScale|keyboard|keyboardHidden|mcc|mnc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|touchscreen|uiMode" android:launchMode="singleTask" android:name="com.unity3d.player.UnityPlayerActivity" android:excludeFromRecents="true">
      <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
      </intent-filter>
      <meta-data android:name="com.oculus.vr.focusaware" android:value="false" />
    </activity>
    <meta-data android:name="unityplayer.SkipPermissionsDialog" android:value="false" />
    <meta-data android:name="com.samsung.android.vr.application.mode" android:value="vr_only" />
  </application>
  <uses-feature android:name="android.hardware.vr.headtracking" android:version="1" android:required="true" />
  <uses-feature android:name="oculus.software.handtracking" android:required="false" />
  <uses-permission android:name="oculus.permission.handtracking" />
</manifest>
```

#### Building fails or the app can't be run on the Quest
Make sure the following settings in your Project Settings are set:
  - Under Other Settings > Identification make sure the Minimum API Level is set to "Android 5.0 'Lollipop' (API Level 21)"
  - Under XR Settings, enable Virtual Reality Support.

### Additional Packages/Plugins necessary: 
  - Oculus Integration
  - TMPro

# Controls with TouchController
- Press X on the left controller or A on the right controller to teleport.
- Use the Handtriggers to grab the kitchen elements
- Stretch your index finger (do not touch the index trigger) to interact with the buttons.
- The white ball next to the UI can be grabbed to move the UI arround freely

# Controls with Handtracking
- Do this gesture to activate the teleport arc...
![iwata gesture](http://www.legeekretrogaming.com/geekblog/wp-content/uploads/2015/07/directly.png) 
- ... then pinch with both hands to teleport to the loaction you're targeting. The target can be moved by looking around.
- Make a fist while touching the kitchen element to grab it
- Stretch your index finger to interact with the buttons
- The white ball next to the UI can be grabbed to move the UI arround freely

**Grabbing in handtrack mode is still very unreliable, controller mode is recommended**
