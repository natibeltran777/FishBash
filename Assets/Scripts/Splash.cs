using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    new ParticleSystem particleSystem;

    private SplashPool _pool = null;
    ParticleSystem.ShapeModule shape;
    ParticleSystem.MainModule main;

    ParticleSystem.MinMaxCurve speed;

    public SplashPool Pool
    {
        set
        {
            if (_pool == null) _pool = value;
            else
            {
                Debug.LogError("Can't reassign pool");
            }
        }
    }



    // Start is called before the first frame update
    void Awake()
    {
        SetupParticleSystem();
        
    }

    void SetupParticleSystem()
    {
        particleSystem = GetComponent<ParticleSystem>();
        shape = particleSystem.shape;
        main = particleSystem.main;
        main.simulationSpeed = 3.0f;

        speed = main.startSpeed;
    }

    private void OnParticleSystemStopped()
    {
        RecycleSplash();
    }

    public void ResetSplash()
    {
        particleSystem.Clear();
        transform.position = Vector3.zero;
    }

    public void InitializeSplash(float strength, Vector3 position)
    {
        position.y = 0;
        shape.radius = strength;
        transform.position = position;

        ParticleSystem.MinMaxCurve _speed = speed;
        _speed.constantMin *= strength;
        _speed.constantMax *= strength;

        main.startSpeed = speed;


        //\todo: Also experiment with setting emission speed here

        particleSystem.Play();
    }

    private void RecycleSplash()
    {
        _pool.RecycleSplash(this);
    }

}
