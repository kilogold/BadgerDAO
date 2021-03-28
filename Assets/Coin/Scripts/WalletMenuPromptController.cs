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

    public void NotReadyPrompt(ValidateWallet.InvalidStartArgs args)
    {
        StopAllCoroutines();

        string message = null;
        switch (args.code)
        {
            case ValidateWallet.InvalidStartArgs.Code.TooSoon:
                BigInteger value = (BigInteger) args.value;
                message = $"Wait {value.ToString("00")} seconds.";
                break;
            
            case ValidateWallet.InvalidStartArgs.Code.InvalidAddress:
                message = "Invalid address.";
                break;
        }
        StartCoroutine(DisplayPrompt(message, promptDuration));
    }
}
