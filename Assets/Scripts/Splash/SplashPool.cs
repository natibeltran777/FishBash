using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FishBash
{
    namespace Splash
    {
        [CreateAssetMenu]
        public class SplashPool : GenericPool<SplashObj>
        {
            protected override void SetPool(SplashObj obj)
            {
                obj.Pool = this;
            }
        }
    }
}
