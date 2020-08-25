using HTC.UnityPlugin.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FishBash
{
    public class GenericPool<T> : ScriptableObject where T : MonoBehaviour, IPoolable
    {

        [SerializeField] T prefab;

        private Scene poolScene;

        [System.NonSerialized] List<T> pool;

        void CreatePool()
        {
            pool = new List<T>();
            if (Application.isEditor)
            {
                poolScene = SceneManager.GetSceneByName(name);
                if (poolScene.isLoaded)
                {
                    GameObject[] rootObjects = poolScene.GetRootGameObjects();
                    for (int i = 0; i < rootObjects.Length; i++)
                    {
                        T pooledShape = rootObjects[i].GetComponent<T>();
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

        public T Get()
        {
            T instance;
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
                SetPool(instance);
                SceneManager.MoveGameObjectToScene(instance.gameObject, poolScene);
            }

            return instance;
        }

        protected virtual void SetPool(T obj)
        {

        }

        public void Recycle(T toRecycle)
        {
            if (pool == null || !poolScene.IsValid()) CreatePool();

            pool.Add(toRecycle);
            toRecycle.gameObject.SetActive(false);
            toRecycle.Reset();
        }

    }
}
