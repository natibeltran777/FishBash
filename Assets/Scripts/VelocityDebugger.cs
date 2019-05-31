using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityDebugger : MonoBehaviour
{

    [SerializeField]
    private float maxVelocity = 20f;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = ColorForVelocity();
    }

    private Color ColorForVelocity()
    {
        float velocity = GetComponent<Rigidbody>().velocity.magnitude;

        return Color.Lerp(Color.green, Color.red, velocity / maxVelocity);
    }
}
