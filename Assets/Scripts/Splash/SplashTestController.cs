using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Splash
    {

        public class SplashTestController : MonoBehaviour
        {

            [SerializeField] SplashPool pool;

            [SerializeField] Camera cam;

            Collider col;

            float strength = 1.0f;

            private void Start()
            {
                col = GetComponent<Collider>();
            }

            public void Update()
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Clicked();
                }
            }

            public void Clicked()
            {
                Ray clickRay = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(clickRay, out RaycastHit hitinfo))
                {

                    if (hitinfo.collider == col)
                    {
                        SplashObj splash = pool.GetSplash();
                        splash.InitializeSplash(strength, hitinfo.point);
                    }

                }
            }

            void OnGUI()
            {
                strength = GUI.HorizontalSlider(new Rect(25, 45, 100, 30), strength, 0.0F, 10.0F);
            }

        }
    }
}
