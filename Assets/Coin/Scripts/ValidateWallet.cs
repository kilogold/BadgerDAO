using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Need to rethink validation on this class.
/// </summary>
public class ValidateWallet : MonoBehaviour
{
    public Faucet faucet;

    [SerializeField] private UnityEvent ValidStart;
    [SerializeField] private UnityEvent<InvalidStartArgs> InvalidStart; //TODO: Consider payload pattern. Gotta consider empty faucet.
    [SerializeField] private InputField walletInputField;
    [SerializeField] private bool isValidating = false;

    // TODO:
    // Ensure we can conduct wallet validation only after having initialized rate via Smart contract query.
    private BigInteger elapsedSecondsRate = BigInteger.MinusOne;

    private void Start()
    {
        faucet.OnGetElapsedTime.AddListener(OnGetElapsedTime);
        faucet.OnGetElapsedRate.AddListener(OnGetElapsedRate);
        faucet.GetElapsedRate();
    }
    
    private void OnDestroy()
    {
        faucet.OnGetElapsedTime.RemoveListener(OnGetElapsedTime);
        faucet.OnGetElapsedTime.RemoveListener(OnGetElapsedRate);
    }

    public void Validate()
    {
        if (isValidating)
            return;

        if (walletInputField.text.Length != 42 || !walletInputField.text.StartsWith("0x", StringComparison.Ordinal))
        {
            // TODO: Check that input address is not a Contract address. May require callback pattern refactor.
            InvalidStart?.Invoke(new InvalidStartArgs(InvalidStartArgs.Code.InvalidAddress, null));
            return;
        }
        
        
        faucet.GetElapsedTime();
        isValidating = true;
    }
    
    private void OnGetElapsedRate(BigInteger elapsedRate)
    {
        elapsedSecondsRate = elapsedRate;
    }

    private void OnGetElapsedTime(BigInteger elapsedTime)
    {
        if (elapsedTime >= elapsedSecondsRate)
        {
            // TODO:
            //       Check if there is enough ERC20 balance in the contract.
            //       Other players could be playing at the same time, we need to ensure everyone gets the correct payout,
            //       e.g. payout transaction is not reverted due to lack of funds.
            // Solution:
            //       Scan the mempool for pending relevant transactions using https://www.blocknative.com/.
            //       This way, we can calculate if there are enough funds for a game session based on
            //       [contract amount] - [collective transaction total from all pending payouts].
            // Bonus:
            //       We can prevent an request spam attack where the same user plays multiple rounds in the same block. 
            ValidStart?.Invoke();
        }
        else
        {
            var timeRemaining = elapsedSecondsRate - elapsedTime;
            InvalidStart?.Invoke(new InvalidStartArgs(InvalidStartArgs.Code.TooSoon, timeRemaining));
        }

        isValidating = false;
    }

    public class InvalidStartArgs
    {
        public enum Code
        {
            TooSoon,
            InvalidAddress,
        }

        public readonly Code code;
        public readonly object value;

        public InvalidStartArgs(Code code, object value)
        {
            this.code = code;
            this.value = value;
        }
    }
}
