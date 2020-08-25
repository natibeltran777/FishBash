using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FishBash;

abstract public class TargetBehaviour : MonoBehaviour
{
    public abstract void OnTargetGazed();
    public abstract void OnTargetUngazed();

    public virtual Transform GetTargetMesh
    {
        get
        {
            return this.transform;
        }
    }
}
