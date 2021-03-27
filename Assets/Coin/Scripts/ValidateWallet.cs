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
            // TODO: Check if there is enough balance in the contract.
            //          We already check this in Solidity code... Check here too?
            //          Other players could be playing at the same time. What do they get?
            //          Maybe only allow players if maximum possible gain < contract balance * active players. 
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
