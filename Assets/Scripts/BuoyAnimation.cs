using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyAnimation : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0.0f, 2.0f)] Vector2 speedRange;

    float speed;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        speed = Random.Range(speedRange.x, speedRange.y);

        animator.speed = speed;
    }

}
