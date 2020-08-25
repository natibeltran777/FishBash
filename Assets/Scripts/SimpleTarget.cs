using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FishBash;

public class SimpleTarget : TargetBehaviour
{
    [SerializeField] private Transform m_target;
    private float m_glowTransitionDuration = 0.2f;
    private float m_glowOnVal = 0.5f;
    private float m_glowOffVal = 20f;
    private float m_targetPickCoolDown = 2.5f;
    private bool m_isOn = true;
    private bool m_targetIsSelected = false;
    private Material m_glowMaterial;
    private static int glowId = Shader.PropertyToID("_GlowPower");

    public override Transform GetTargetMesh 
    {
        get
        {
            return m_target;
        }
    }

    private void Start()
    {
        m_glowMaterial = transform.parent.GetComponentInChildren<Renderer>().material;
        OnTargetUngazed();
    }

    public override void OnTargetGazed()
    {
        if (!m_isOn)
        {
            
            m_glowMaterial.DOFloat(m_glowOnVal, glowId, m_glowTransitionDuration);
            m_isOn = true;
        }
    }

    public override void OnTargetUngazed()
    {
        if (m_isOn)
        {
            m_glowMaterial.DOFloat(m_glowOffVal, glowId, m_glowTransitionDuration);
            m_isOn = false;
        }
    }
    

}
