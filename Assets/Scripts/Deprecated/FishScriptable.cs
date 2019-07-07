using UnityEngine;

namespace FishBash {

    [CreateAssetMenu(fileName = "Enemy", menuName = "Waves/New Enemy", order = 3)]
    [System.Serializable]
    public class FishScriptable : ScriptableObject
    {
        public GameObject fishPrefab;

        public bool randomSpeed = true;
        public bool randomPosition = true;

        public Vector2 spawnPosition;
        public float speed;
    }
}