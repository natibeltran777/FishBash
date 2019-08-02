using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Objects with this component will circle the camera at their set radius, with their y component fixed
/// </summary>
public class CircularRotationTracking : MonoBehaviour
{
    private Transform cameraTransform;
    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        Vector3 heading = transform.position - cameraTransform.position;
        radius = heading.magnitude;
    }

    public Vector3 GetHeading(float yComponent)
    {
        Vector3 heading = transform.position - cameraTransform.position;
        heading.y = yComponent;
        heading.Normalize();
        heading *= radius;
        return heading;

    }
}
