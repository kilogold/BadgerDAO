using System;
using UnityEngine;
using UnityEngine.UI;

public class WalletValidationOffChain : MonoBehaviour
{
    [SerializeField] private InputField walletInputField;

    public WalletValidationCoordinator.ValidationOutcome.Code Validate()
    {
        if (walletInputField.text.Length != 42 || !walletInputField.text.StartsWith("0x", StringComparison.Ordinal))
        {
            return WalletValidationCoordinator.ValidationOutcome.Code.InvalidAddress;
        }

        return WalletValidationCoordinator.ValidationOutcome.Code.Success;
    }
}
