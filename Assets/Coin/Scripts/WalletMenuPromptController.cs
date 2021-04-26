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

    public void NotReadyPrompt(WalletValidationCoordinator.ValidationOutcome args)
    {
        StopAllCoroutines();

        string message = null;
        switch (args.code)
        {
            case WalletValidationCoordinator.ValidationOutcome.Code.TooSoon:
                BigInteger value = (BigInteger) args.value;
                message = $"Wait {value.ToString("00")} seconds.";
                break;
            
            case WalletValidationCoordinator.ValidationOutcome.Code.InvalidAddress:
                message = "Invalid address.";
                break;
            
            case WalletValidationCoordinator.ValidationOutcome.Code.DoesNotQualify:
                message = "You don't qualify.";
                break;
        }
        StartCoroutine(DisplayPrompt(message, promptDuration));
    }
}
