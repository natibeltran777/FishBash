using FishBash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FishBash
{
    [CreateAssetMenu]
    public class FishPool : ScriptableObject
    {

        [SerializeField] GameObject[] prefabs;

        private Scene poolScene;

        [System.NonSerialized] List<IFish>[] pools;

        void CreatePools()
        {
            pools = new List<IFish>[prefabs.Length];
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i] = new List<IFish>();
            }
            if (Application.isEditor)
            {
                poolScene = SceneManager.GetSceneByName(name);
                if (poolScene.isLoaded)
                {
                    GameObject[] rootObjects = poolScene.GetRootGameObjects();
                    for (int i = 0; i < rootObjects.Length; i++)
                    {
                        IFish pooledShape = rootObjects[i].GetComponent<IFish>();
                        if (!pooledShape.GameObject.activeSelf)
                        {
                            pools[pooledShape.FishId].Add(pooledShape);
                        }
                    }
                    return;
                }
            }
            poolScene = SceneManager.CreateScene(name);
            //poolScene.
        }

        public IFish GetFish(int fishId = 0)
        {
            IFish instance;
            if (pools == null || !poolScene.IsValid()) CreatePools();
            List<IFish> pool = pools[fishId];
            int lastIndex = pool.Count - 1;

            if (lastIndex >= 0)
            {
                instance = pool[lastIndex];
                pool.RemoveAt(lastIndex);
                //instance.GameObject.SetActive(true);
                
            }
            else
            {
                GameObject go = Instantiate(prefabs[fishId]);
                go.SetActive(false);
                instance = go.GetComponent<IFish>();
                //instance.
                instance.Pool = this;
                SceneManager.MoveGameObjectToScene(instance.GameObject, poolScene);
            }

            return instance;
        }


        public void Recycle(IFish toRecycle)
        {
            if (pools == null || !poolScene.IsValid()) CreatePools();
            Debug.Assert(toRecycle.Pool.Equals(this));
            pools[toRecycle.FishId].Add(toRecycle);
            toRecycle.GameObject.SetActive(false);

        }

    }
}
