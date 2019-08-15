# FishBash

## Building

### Oculus Quest 

- Navigate to Assets/Oculus/OculusProjectConfig
- Ensure the target device is set to Quest
- Click Oculus > Tools > Remove AndroidManifest.xml
- Click Oculus > Tools > Create store-compatible AndroidManifest.xml
- Navigate to Assets/Scenes/OculusBuild.scene
- Ensure GameManager "isOculusGo" is left unchecked
- Open build settings and ensure platform is set to Android
- Build the scene

### Oculus Go 

- Navigate to Assets/Oculus/OculusProjectConfig
- Ensure the target device is set to Go
- Click Oculus > Tools > Remove AndroidManifest.xml
- Click Oculus > Tools > Create store-compatible AndroidManifest.xml
- Navigate to Assets/Scenes/GoBuild.scene
- Ensure GameManager "isOculusGo" is checked
- Open build settings and ensure platform is set to Android
- Build the scene