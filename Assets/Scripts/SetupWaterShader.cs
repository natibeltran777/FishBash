using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupWaterShader : MonoBehaviour
{
    [SerializeField]
    private RenderTexture rt;
    private Camera orthoCam;
    [SerializeField]
    private Transform target;

    // Start is called before the first frame update
    void Awake()
    {
        orthoCam = this.GetComponent<Camera>();
        Shader.SetGlobalTexture("_GlobalEffectRT", rt);
        Shader.SetGlobalFloat("_GlobalOrthographicCameraSize", orthoCam.orthographicSize);
    }

    private void Update()
    {
        Vector3 position = transform.position;
        Shader.SetGlobalVector("_GlobalOrthographicCameraPosition", position);
    }

}
