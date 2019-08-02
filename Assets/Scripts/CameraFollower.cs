using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private CircularRotationTracking cameraHeading;


    // Update is called once per frame
    void Update()
    {
        transform.position = cameraHeading.GetHeading(0);
        transform.LookAt(Vector3.zero);
    }
}
