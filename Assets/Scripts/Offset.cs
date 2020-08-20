using UnityEngine;

namespace FishBash
{
    [System.Serializable]
    public struct Offset
    {
#if UNITY_EDITOR
        public string label;
        public Vector3 offset;
        public Quaternion rotation;

        public void ApplyTransform(Transform t)
        {
            UnityEditor.Undo.RecordObject(t, "Apply offset");
            t.SetPositionAndRotation(offset, rotation);
        }

        public Offset GetTransform(Transform t)
        {
            return new Offset
            {
                label = string.IsNullOrEmpty(label) ? "" : label,
                offset = t.position,
                rotation = t.rotation,
            };
        }
#endif
    }

}
