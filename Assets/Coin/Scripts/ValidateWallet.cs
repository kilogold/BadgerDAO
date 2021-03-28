using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Need to rethink validation on this class.
/// </summary>
public class ValidateWallet : MonoBehaviour
{
    public Faucet faucet;

    [SerializeField] private UnityEvent ValidStart;
    [SerializeField] private UnityEvent<BigInteger> InvalidStart; //TODO: Consider payload pattern. Gotta consider empty faucet.
    [SerializeField] private bool isValidating = false;
    
    public class InvalidReason
    {
        public enum Reason
        {
            TooSoon,
            FaucetDepleted,
        }

        public Reason reason;
        public object data;
    }

    private void Start()
    {
        faucet.OnGetElapsedTime.AddListener(OnGetElapsedTime);
    }

    private void OnDestroy()
    {
        faucet.OnGetElapsedTime.RemoveListener(OnGetElapsedTime);
    }

    public void Validate()
    {
        if (isValidating)
            return;
        
        faucet.GetElapsedTime();
        isValidating = true;
    }

    private void OnGetElapsedTime(BigInteger elapsedTime)
    {
        //HACK: Get from contract.
        int elapsedSecondsRate = 120;

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
            InvalidStart?.Invoke(timeRemaining);
        }

        isValidating = false;
    }
}
