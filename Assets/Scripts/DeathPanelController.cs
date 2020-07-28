using FishBash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanelController : MonoBehaviour
{
    private Image panel;


    [SerializeField]
    private float time;

    void Awake()
    {
        panel = GetComponent<Image>();
    }

    private void Start()
    {
        EventManager.StartListening("PLAYERHIT", StartTurnOn);
    }

    void StartTurnOn()
    {
        gameObject.SetActive(true);
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        panel.enabled = true;
        panel.canvasRenderer.SetAlpha(0.0f);
        panel.CrossFadeAlpha(1.0f, time / 2, false);
        while(panel.canvasRenderer.GetAlpha() != 1.0f)
        {
            yield return null;
        }
        panel.CrossFadeAlpha(0.0f, time / 2, false);
        panel.enabled = false;
    }
}
