using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private Camera cam;

    private TargetBehaviour m_currentTarget;
    private TargetBehaviour m_previousTarget;

    void Update()
    {
        GetGazePos(cam);
    }

    private bool GetMousePos(Camera cam)
    {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(r, out RaycastHit hit, 50f, m_layerMask.value))
        {
            m_currentTarget = hit.collider.gameObject.GetComponent<TargetBehaviour>();
            if (m_currentTarget != m_previousTarget)
            {
                if (m_previousTarget != null) m_previousTarget.OnTargetGazed();
            }
            m_currentTarget.OnTargetGazed();
            m_previousTarget = m_currentTarget;
            return true;
        }
        else {
            if(m_currentTarget != null)
                m_currentTarget.OnTargetUngazed();

            return false; }
    }


    // Replace this with eye tracking 
    private bool GetGazePos(Camera cam)
    {
        Vector3 point = new Vector3(0.5f, 0.5f, 0f);

        // Haven't really tested Mono vs left here
        Ray r = cam.ViewportPointToRay(point, Camera.MonoOrStereoscopicEye.Mono);

        if (Physics.Raycast(r, out RaycastHit hit, 100f, m_layerMask.value))
        {
            m_currentTarget = hit.collider.gameObject.GetComponent<TargetBehaviour>();
            if (m_currentTarget != m_previousTarget)
            {
                if (m_previousTarget != null) m_previousTarget.OnTargetUngazed();
            }
            if (m_currentTarget != null) m_currentTarget.OnTargetGazed();
            m_previousTarget = m_currentTarget;
            return true;
        }
        else
        {
            if (m_currentTarget != null)
                m_currentTarget.OnTargetUngazed();

            return false;
        }
    }

}
