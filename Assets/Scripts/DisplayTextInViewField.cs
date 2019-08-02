using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTextInViewField : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI uiText;

    [SerializeField]
    private GameObject follower;

    /// <summary>
    /// Displays given text on screen for a set period of time
    /// </summary>
    /// <param name="text">Text to display</param>
    /// <param name="timeToDisplay">Time to display text</param>
    /// <returns></returns>
    public IEnumerator DisplayText(string text, float timeToDisplay)
    {
        uiText.text = text;
        transform.position = follower.transform.position;
        transform.LookAt(Vector3.zero);
        uiText.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeToDisplay);
        uiText.gameObject.SetActive(false);
        yield return null;
    }
}
