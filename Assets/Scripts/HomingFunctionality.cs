using FishBash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingFunctionality : MonoBehaviour
{
    [SerializeField] private Rigidbody m_cannonClubRigidbody;
    [SerializeField] private Transform m_target;

    private Rigidbody m_clubRigidBody;
    private Rigidbody m_fruit;

    private Vector3 m_directionOfImpact;
    private Vector3 m_midPointBezier;
    private Vector3 m_initialPosition;
    private Vector3 m_finalPosition;


    private const float m_speedToActivateHoming = 10.0f;
    private float m_timeVal;
    private float m_midMagnitude;
    private bool m_activateBezierCurve;

    private void Start()
    {
        m_clubRigidBody = this.transform.parent.GetComponent<Rigidbody>();
        m_midMagnitude = GetMidDistance();
        BattingManager.instance.OnTargetChanged += SetNewHomingTarget;
    }

    private void FixedUpdate()
    {
        if (m_activateBezierCurve)
        {
            Vector3 newPosition = m_initialPosition * 1 * Mathf.Pow(m_timeVal, 0) * Mathf.Pow((1 - m_timeVal), 2)
                    + m_midPointBezier * 2 * Mathf.Pow(m_timeVal, 1) * Mathf.Pow((1 - m_timeVal), 1)
                    + m_target.position * 1 * Mathf.Pow(m_timeVal, 2) * Mathf.Pow((1 - m_timeVal), 0);

            m_fruit.position = newPosition;
            m_timeVal += Time.deltaTime * 0.5f;
            m_fruit.AddRelativeTorque(1, 0.8f, 0.2f);
        }
        if (m_timeVal > 1)
        {
            m_timeVal = 0;
            m_activateBezierCurve = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_target != null)
        {
            if (m_clubRigidBody.velocity.magnitude > m_speedToActivateHoming && other.attachedRigidbody.velocity.magnitude != 0)
            {
                m_finalPosition = m_target.position;
                m_directionOfImpact = m_clubRigidBody.velocity.normalized;
                m_initialPosition = other.transform.position;
                Vector3 finalPosDir = m_finalPosition - m_initialPosition;
                if (Vector3.Dot(finalPosDir.normalized, m_directionOfImpact.normalized) > 0.05)
                {
                    m_midPointBezier = m_initialPosition + m_directionOfImpact * m_midMagnitude;
                    m_fruit = other.attachedRigidbody;
                    m_activateBezierCurve = true;
                }
            }
        }
    }


    /// <summary>
    /// Get's the distance between the fruit and the target naval enemy
    /// </summary>
    /// <returns> Magnitude of vector to mid point </returns>
    private float GetMidDistance()
    {
        Vector3 midpoint = (m_target.position - this.transform.position) / 2;
        return midpoint.magnitude;
    }

    private void SetNewHomingTarget()
    {
        m_target = BattingManager.instance.Target;
    }

    /// <summary>
    /// Cancel the Bezier once target is destroyed
    /// </summary>
    public void CancelBezier()
    {
        m_activateBezierCurve = false;
        m_timeVal = 1;
    }

}
