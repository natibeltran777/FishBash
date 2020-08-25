using UnityEngine;

namespace FishBash
{
    namespace Target
    {
        abstract public class TargetBehaviour : MonoBehaviour
        {
            public abstract void OnTargetGazed();
            public abstract void OnTargetUngazed();
        }
    }
}
