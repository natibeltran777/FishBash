using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Screenshot))]
public class ScreenshotEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if(GUILayout.Button("Take Screenshot")) {
            ((Screenshot)serializedObject.targetObject).TakeScreenshot();
        }
    }
}
