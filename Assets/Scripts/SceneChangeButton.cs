using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton : MonoBehaviour
{
    
    public void LoadScene(int index)
    {
        SceneControlManager.instance.LoadScene(index);
    }

}
