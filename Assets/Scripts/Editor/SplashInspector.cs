using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FishBash
{
    namespace Splash
    {
        [CustomEditor(typeof(SplashObj))]
        public class SplashInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                SplashObj s = target as SplashObj;

                if (GUILayout.Button(new GUIContent("Apply Splash Strength")))
                {
                    Undo.RecordObject(s, "Set splash value");
                    s.SetSplashVals();
                }
            }
        }
    }
}
