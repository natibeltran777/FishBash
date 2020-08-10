using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FishBash
{
    namespace Splash
    {
        [CreateAssetMenu]
        public class SplashPool : ScriptableObject
        {

            [SerializeField] SplashObj prefab;

            private Scene poolScene;

            [System.NonSerialized] List<SplashObj> pool;

            void CreatePool()
            {
                pool = new List<SplashObj>();
                if (Application.isEditor)
                {
                    poolScene = SceneManager.GetSceneByName(name);
                    if (poolScene.isLoaded)
                    {
                        GameObject[] rootObjects = poolScene.GetRootGameObjects();
                        for (int i = 0; i < rootObjects.Length; i++)
                        {
                            SplashObj pooledShape = rootObjects[i].GetComponent<SplashObj>();
                            if (!pooledShape.gameObject.activeSelf)
                            {
                                pool.Add(pooledShape);
                            }
                        }
                        return;
                    }
                }

                poolScene = SceneManager.CreateScene(name);
            }

            public SplashObj GetSplash()
            {
                SplashObj instance;
                if (pool == null || !poolScene.IsValid()) CreatePool();

                int lastIndex = pool.Count - 1;

                if (lastIndex >= 0)
                {
                    instance = pool[lastIndex];
                    instance.gameObject.SetActive(true);
                    pool.RemoveAt(lastIndex);
                }
                else
                {
                    instance = Instantiate(prefab);
                    instance.Pool = this;
                    SceneManager.MoveGameObjectToScene(instance.gameObject, poolScene);
                }

                return instance;
            }


            public void RecycleSplash(SplashObj toRecycle)
            {
                if (pool == null || !poolScene.IsValid()) CreatePool();

                pool.Add(toRecycle);
                toRecycle.gameObject.SetActive(false);
                toRecycle.ResetSplash();
            }

        }
    }
}
