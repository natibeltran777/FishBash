using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlowDebug : MonoBehaviour
{
    [SerializeField] private Material m_glowMaterial;

    private bool m_increaseGlow = false;
    private bool m_decreaseglow = false;

    // Start is called before the first frame update
    void Awake()
    {
        //m_glowMaterial = this.GetComponent<Material>();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10,10,150,100), "Turn on glow"))
        {
            m_glowMaterial.DOFloat(0, "Vector1_604F3D38", 1);
        }
        if (GUI.Button(new Rect(10, 210, 150, 100), "Turn off glow"))
        {
            m_glowMaterial.DOFloat(3, "Vector1_604F3D38", 1);
            
        }
    }
}
