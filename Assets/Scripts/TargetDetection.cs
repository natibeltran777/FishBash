using FishBash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In charge of updating which Target is currently being selected 
/// </summary>
public class TargetDetection : MonoBehaviour
{
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private Camera cam;

    private TargetBehaviour m_currentTarget;
    private TargetBehaviour m_previousTarget;
    private TargetBehaviour m_selectedTarget;

    private IEnumerator m_coolDown;


    private bool m_targetIsSelected = false;
    private bool m_cooldownActive = false;
    private float m_targetPickCoolDown = 2.0f;

    void Update()
    {
        GetGazePos(cam);
    }

    private void GetMousePos(Camera cam)
    {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        RaycastToTarget(r);
    }

    // Replace this with eye tracking 
    private void GetGazePos(Camera cam)
    {
        Vector3 point = new Vector3(0.5f, 0.5f, 0f);
        Ray r = cam.ViewportPointToRay(point, Camera.MonoOrStereoscopicEye.Mono);
        RaycastToTarget(r);
    }

    private void SelectTarget(TargetBehaviour target)
    {
        target.OnTargetGazed();
        BattingManager.instance.SetNewTarget(target.GetTargetMesh);
        m_selectedTarget = target;
    }

    private void DeselectTarget(TargetBehaviour target)
    {
        if (target != null) target.OnTargetUngazed();
        BattingManager.instance.SetNewTarget(null);
        m_selectedTarget = null;
    }

    private void RaycastToTarget(Ray r)
    {
        if (Physics.Raycast(r, out RaycastHit hit, 100f, m_layerMask.value))
        {
            TargetBehaviour target;
            target = hit.collider.gameObject.GetComponent<TargetBehaviour>();

            m_currentTarget = target;

            // if curr is not null
            if (m_currentTarget != null)
            {   
                // if when curr is lit, prev is a different target that isn't null, 
                if (m_currentTarget != m_previousTarget)
                {
                    if (m_coolDown != null)
                    {
                        StopCoroutine(m_coolDown);
                        m_coolDown = null;
                        m_cooldownActive = false;
                    }
                    if (m_previousTarget == m_selectedTarget)
                        DeselectTarget(m_previousTarget);

                    // we interrupt prev's cooldown and unlight prev
                }
                // curr is lit
                if (m_currentTarget != m_selectedTarget)
                {
                    SelectTarget(m_currentTarget);
                }
                
            }
            m_previousTarget = m_currentTarget;
            return;
        }
        {
            if (!m_cooldownActive && m_selectedTarget != null)
            {
                m_coolDown = GazeCoolDown(m_selectedTarget);
                StartCoroutine(m_coolDown);
                m_cooldownActive = true;
                //we intiate cooldown 
            }
            m_currentTarget = null;
        }
    }

    private IEnumerator GazeCoolDown(TargetBehaviour target)
    {
        TargetBehaviour targetToCool = target;
        yield return new WaitForSeconds(m_targetPickCoolDown);
        
        if (m_currentTarget == target)
        {
            m_coolDown = GazeCoolDown(target);
            StartCoroutine(m_coolDown);
        }
        else
        {
            m_coolDown = null;
            m_cooldownActive = false;
            DeselectTarget(target);
        }
    }

}
