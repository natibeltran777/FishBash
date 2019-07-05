using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    public class RotatorController : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, -5f, 0);
            }
            if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 5f, 0);
            }
        }
    }
}
