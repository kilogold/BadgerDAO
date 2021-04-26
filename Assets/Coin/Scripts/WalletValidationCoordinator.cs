using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WalletValidationCoordinator : MonoBehaviour
{
    public WalletValidationOffChain offChainValidation;
    public WalletValidationOnChain onChainValidation;

    [SerializeField] private UnityEvent ValidStart;
    [SerializeField] private UnityEvent<ValidationOutcome> InvalidStart; //TODO: Consider payload pattern. Gotta consider empty faucet.
    [SerializeField] private InputField walletAddressInputField;
    [SerializeField] private bool isValidatingCoroutine;

    private Coroutine validationCR = null;
    
    private void Start()
    {
        isValidatingCoroutine = false;
        onChainValidation.validationComplete += OnValidationCoroutineComplete;
    }

    private void OnDestroy()
    {
        onChainValidation.validationComplete -= OnValidationCoroutineComplete;
    }

    public void Validate()
    {
        if (validationCR != null)
            return;
        
        var offChainOutcome = offChainValidation.Validate();
        
        if(offChainOutcome != ValidationOutcome.Code.Success)
        {
            // TODO: Check that input address is not a Contract address. May require callback pattern refactor.
            InvalidStart?.Invoke(new ValidationOutcome(offChainOutcome, null));
        }

        validationCR = StartCoroutine(onChainValidation.ValidateCR(walletAddressInputField.text));
    }
    
    private void OnValidationCoroutineComplete(ValidationOutcome outcome)
    {
        switch (outcome.code)
        {
            case ValidationOutcome.Code.Success:
                ValidStart?.Invoke();
                break;
            
            default:
                InvalidStart?.Invoke(outcome);
                break;
        }
        
        onChainValidation.StopCoroutine(validationCR);
        validationCR = null;
    }

    public class ValidationOutcome
    {
        public enum Code
        {
            Success,
            TooSoon,
            InvalidAddress,
            DoesNotQualify
        }

        public readonly Code code;
        public readonly object value;

        public ValidationOutcome(Code code, object value)
        {
            this.code = code;
            this.value = value;
        }
    }
}
