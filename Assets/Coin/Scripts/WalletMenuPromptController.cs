using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class WalletMenuPromptController : MonoBehaviour
{
    public Text promptText;
    public GameObject promptVisual;
    public float promptDuration;
    
    public IEnumerator DisplayPrompt (string message, float duration)
    {
        promptText.text = message;
        promptVisual.SetActive(true);
        yield return new WaitForSeconds(duration);
        promptVisual.SetActive(false);
    }

    public void NotReadyPrompt(BigInteger timeUntilReady)
    {
        StopAllCoroutines();
        string message = $"Wait {timeUntilReady.ToString("00")} seconds.";
        StartCoroutine(DisplayPrompt(message, promptDuration));
    }
}
