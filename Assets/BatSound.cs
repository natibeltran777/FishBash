using FishBash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSound : MonoBehaviour
{

    [SerializeField] AudioClip[] batSounds;

    AudioSource batAudioSource;

    private void Start()
    {
        batAudioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        EventManager.StartListening("FISHHIT", PlayCollisionSound);
    }

    void OnDisable()
    {
        EventManager.StopListening("FISHHIT", PlayCollisionSound);
    }

    void PlayCollisionSound()
    {
        Debug.Log("Play sound");
        SoundManager.instance.RandomizeSfxOnObject(batAudioSource, batSounds);
    }
}
