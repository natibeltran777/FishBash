using FishBash;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Splash
    {
        public class SplashOnCollision : MonoBehaviour
        {

            [SerializeField] SplashPool pool;
            [SerializeField, MinMaxSlider(0f, 10f)] Vector2 strengthRange;

            private void OnTriggerEnter(Collider other)
            {
                if (other.TryGetComponent(out IFish fish))
                {
                    Debug.Log("fish");
                    if (fish.HasBeenHit)
                    {
                        //Note: this means fish will only splash if the player hits them, fish leaping and hitting the player will reenter the water splashless-ly
                        // To me this makes sense, since splashing (will) damage fish but it could be a little odd visually
                        SplashObj splash = pool.Get();

                        float strength = Mathf.Lerp(strengthRange.x, strengthRange.y, Mathf.InverseLerp(0, 50f, fish.Distance));

                        splash.InitializeSplash(strength, other.transform.position);

                        FishManager.instance.DestroyFish(fish, 1);
                    }
                    else
                    {
                        Debug.Log("fish was not hit");
                    }
                }
                else
                {
                    Debug.Log("not a fish");
                }
            }
        }
    }
}