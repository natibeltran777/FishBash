
/*using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FishBash
{
    [CustomEditor(typeof(WaveScriptable))]
    public class WaveScriptableEditor : Editor
    {
        SerializedProperty sProp;
        SerializedProperty rProp;


        private void OnEnable()
        {
            sProp = serializedObject.FindProperty("speed");
            rProp = serializedObject.FindProperty("radius");
        }

        public override void OnInspectorGUI()
        {

            WaveScriptable ws = (WaveScriptable)target;

            ws.waveName = EditorGUILayout.TextField("Wave label", ws.waveName);

            SerializedProperty fiw = serializedObject.FindProperty("fishInWave");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(fiw, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            
            ws.randomFish = EditorGUILayout.Toggle("Spawn fish in random order?", ws.randomFish);
            if (ws.randomFish)
            {
                ws.fishCount = EditorGUILayout.IntField("How many fish to spawn?", ws.fishCount);
            }
            else
            {
                EditorGUILayout.HelpBox("Enter the order as a list of ordered indices. For example, if you would like to spawn the fish in index 1 five times, enter \"11111\"", MessageType.None);
                ws.order = EditorGUILayout.TextField("Order", ws.order);
            }


            EditorGUILayout.PropertyField(sProp);
            EditorGUILayout.PropertyField(rProp);


            ws.timeBetweenFish = EditorGUILayout.FloatField("Spawn time between fish", ws.timeBetweenFish);

            serializedObject.ApplyModifiedProperties();

        }

    }
}
*/