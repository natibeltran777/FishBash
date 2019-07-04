using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FishBash
{
    [CustomEditor(typeof(FishScriptable))]
    public class EnemyScriptableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            FishScriptable fs = (FishScriptable)target;

            fs.fishPrefab = (GameObject) EditorGUILayout.ObjectField("Fish Prefab", fs.fishPrefab, typeof(GameObject), false);

            EditorGUILayout.HelpBox("Set these to true if the position and speed should be randomly set based on the current wave", MessageType.Info);

            fs.randomPosition = EditorGUILayout.Toggle("Random Position", fs.randomPosition);

            if (!fs.randomPosition)
            {
                fs.spawnPosition = EditorGUILayout.Vector2Field("Spawn Position", fs.spawnPosition);
            }

            fs.randomSpeed = EditorGUILayout.Toggle("Random Speed", fs.randomSpeed);

            if (!fs.randomSpeed)
            {
                fs.speed = EditorGUILayout.FloatField("Speed", fs.speed);
            }
            serializedObject.ApplyModifiedProperties();

        }

    }
}
