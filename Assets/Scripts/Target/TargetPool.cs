using UnityEngine;

namespace FishBash
{
    namespace Target
    {
        [CreateAssetMenu]
        public class TargetPool : GenericPool<SimpleTarget>
        {
            protected override void SetPool(SimpleTarget obj)
            {
                obj.Pool = this;
            }
        }
    }
}