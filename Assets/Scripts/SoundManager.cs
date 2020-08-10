using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioSource vfxSource;
    public AudioSource musicSource;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

    }

    /// <summary>
    /// Change background music to specified clip
    /// </summary>
    /// <param name="song">Clip to play</param>
    /// \todo - Improve transition
    public void ChangeMusic(AudioClip song)
    {
        musicSource.Stop();
        musicSource.clip = song;
        musicSource.Play();
        musicSource.loop = true;
    }


    /// <summary>
    /// Play a fixed sound effect
    /// </summary>
    /// <param name="sound">Sound to play</param>
    public void PlaySingle(AudioClip sound)
    {
        PlaySingleOnObject(sound, vfxSource);
    }

    /// <summary>
    /// Play a fixed sound effect on the specified object
    /// </summary>
    /// <param name="sound">Sound to play</param>
    /// <param name="obj">Source of the sound</param>
    public void PlaySingleOnObject(AudioClip sound, AudioSource obj)
    {
        obj.clip = sound;
        obj.Play();
    }

    /// <summary>
    /// Play a fixed sound effect on the specified object
    /// </summary>
    /// <param name="sound">Sound to play</param>
    /// <param name="obj">Source of the sound</param>
    /// <param name="source">AudioSource created on the object</param>
    public void PlaySingleOnObject(AudioClip sound, GameObject obj, out AudioSource source)
    {
        source = obj.GetComponent<AudioSource>();

        if (source == null)
        {
            source = obj.AddComponent<AudioSource>();
        }
        PlaySingleOnObject(sound, source);
    }

    /// <summary>
    /// Play a randomly selected sound effect
    /// </summary>
    /// <param name="clips">Sounds to select from</param>
    public void RandomizeSfx(params AudioClip[] clips)
    {
        RandomizeSfxOnObject(vfxSource, clips);
    }

    /// <summary>
    /// Play a randomly selected sound effect on the specified object
    /// </summary>
    /// <param name="obj">Source of the sound</param>
    /// <param name="clips">Sounds to select from</param>
    public void RandomizeSfxOnObject(AudioSource obj, params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        obj.pitch = randomPitch;
        obj.clip = clips[randomIndex];
        if(obj.enabled)
            obj.Play();
    }

    /// <summary>
    /// Play a randomly selected sound effect on the specified object
    /// </summary>
    /// <param name="obj">Source of the sound</param>
    /// <param name="source">AudioSource on the object</param>
    /// <param name="clips">Sounds to select from</param>
    public void RandomizeSfxOnObject(GameObject obj, out AudioSource source, params AudioClip[] clips)
    {
        source = obj.GetComponent<AudioSource>();

        if (source == null)
        {
            source = obj.AddComponent<AudioSource>();
        }
        RandomizeSfxOnObject(source, clips);
    }
}
