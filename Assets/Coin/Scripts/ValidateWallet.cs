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
            InvalidStart?.Invoke(timeRemaining);
        }

        isValidating = false;
    }
}
