using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private CircularRotationTracking cameraHeading;
    [SerializeField] float speed = 0.5f;


    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = transform.position;
        Vector3 targetPos = cameraHeading.GetHeading(0);
        transform.position = Vector3.Lerp(currPos, targetPos, speed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
