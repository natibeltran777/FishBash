using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{

    public static SceneControlManager instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
        //StartCoroutine(LoadAsync(index));
    }

    // \todo: make this nicer to end user (ie load screen, etc)
    // \todo: Right now this doesn't work, do to the overuse of singletons w/o dontdestoyonload flags. 
    // Ideally, we want something like this, since it allows object pools to persist across scenes. 
    IEnumerator LoadAsync(int index)
    {
        Debug.Log("Loading scene...");

        AsyncOperation load = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        load.allowSceneActivation = false;

        Scene toLoad = SceneManager.GetSceneByBuildIndex(index);

        while (!load.isDone)
        {

            if(load.progress >= 0.9f)
            {
                Scene currentActiveScene = SceneManager.GetActiveScene();
                SceneManager.UnloadSceneAsync(currentActiveScene);
                load.allowSceneActivation = true;
            }

            yield return null;
        }
        SceneManager.SetActiveScene(toLoad);


    }
}
