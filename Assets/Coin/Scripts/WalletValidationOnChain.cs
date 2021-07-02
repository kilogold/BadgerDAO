using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Contracts.Faucet.ContractDefinition;
using Contracts.WalletValidator.ContractDefinition;
using Nethereum.JsonRpc.UnityClient;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.UI;
using ContractAddress = System.String;
using BlockchainNodeUrl = System.String;

[System.Serializable] public class ContractAddressCollection { public List<ContractAddress> collection; }
[System.Serializable] public class CrossChainContractEndpoints : SerializableDictionaryBase<BlockchainNodeUrl, ContractAddressCollection>{}


public class WalletValidationOnChain : MonoBehaviour
{
    [SerializeField] private CrossChainContractEndpoints crossChainContractEndpoints;
    [SerializeField] private Faucet faucet;

    public event Action<WalletValidationCoordinator.ValidationOutcome> validationComplete;

    public IEnumerator ValidateCR(string userWalletAddress)
    {
        // Check if user is past retry-cooldown time. 
        yield return CheckCooldownCR(userWalletAddress);

        // Check if user clears cross-chain token holder validation.
        foreach (var endpoint in crossChainContractEndpoints.Keys)
        {
            foreach (var contract in crossChainContractEndpoints[endpoint].collection)
            {
                yield return ContractValidationCallCR(endpoint, contract, userWalletAddress);
            }
        }

        TerminateCoroutineExecution(
            new WalletValidationCoordinator.ValidationOutcome(
                WalletValidationCoordinator.ValidationOutcome.Code.Success, null));
    }

    private IEnumerator ContractValidationCallCR(BlockchainNodeUrl networkUrl, ContractAddress contractAddress,
        string userWalletAddress)
    {
        Debug.Log("Creating ValidateFunction Query");
        var queryRequest = new QueryUnityRequest<ValidateFunction, ValidateOutputDTO>(networkUrl, contractAddress);

        Debug.Log("Executing ValidateFunction Query");
        yield return queryRequest.Query(
            new ValidateFunction() {FromAddress = userWalletAddress, Wallet = userWalletAddress}, contractAddress);

        Debug.Log($"Invoking ValidateFunction Query Callback with value: {queryRequest.Result.ReturnValue1}");
        if (!queryRequest.Result.ReturnValue1)
        {
            TerminateCoroutineExecution(
                new WalletValidationCoordinator.ValidationOutcome(
                    WalletValidationCoordinator.ValidationOutcome.Code.DoesNotQualify, null));
        }
    }

    private void TerminateCoroutineExecution(WalletValidationCoordinator.ValidationOutcome outcome)
    {
        validationComplete?.Invoke(outcome);
    }

    
    // TODO:
    // This logic was ripped from the Faucet class. We need to refactor so we can elegantly reuse the Faucet
    // in order to retain a single source of logic and reduce failure point complexity. Fortunately these are simply
    // read-only calls to the network, but it should be fixed sooner than later. 
    private IEnumerator CheckCooldownCR(string userWalletAddress)
    {
        BigInteger elapsedRate;
        BigInteger elapsedTime;
        
        {
            Debug.Log("Creating Elapsed Rate");
            var queryRequest = new QueryUnityRequest<ParticipantRetryTimeFunction, ParticipantRetryTimeOutputDTO>(faucet.NetworkUrl, faucet.ContractAddress);

            Debug.Log("Executing Elapsed Rate Query");
            yield return queryRequest.Query(new ParticipantRetryTimeFunction(), faucet.ContractAddress);

            Debug.Log($"Invoking Elapsed Rate Query Callback With Value: {queryRequest.Result.ReturnValue1}");
            elapsedRate = queryRequest.Result.ReturnValue1;
        }
        
        {
            Debug.Log("Creating ElapsedTimeQuery");
            var queryRequest = new QueryUnityRequest<GetElapsedTimeFunction, GetElapsedTimeOutputDTO>(faucet.NetworkUrl, faucet.ContractAddress);

            Debug.Log("Executing ElapsedTimeQuery");
            yield return queryRequest.Query(new GetElapsedTimeFunction() { FromAddress = userWalletAddress, Participant = userWalletAddress}, faucet.ContractAddress);

            Debug.Log($"Invoking ElapsedTimeQuery Callback with value: {queryRequest.Result.ReturnValue1}");
            elapsedTime = queryRequest.Result.ReturnValue1;
        }

        if (elapsedTime < elapsedRate)
        {
            var timeRemaining = elapsedRate - elapsedTime;
            
            TerminateCoroutineExecution(new WalletValidationCoordinator.ValidationOutcome(
                WalletValidationCoordinator.ValidationOutcome.Code.TooSoon,
                timeRemaining
                ));
        }
        else
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
        }
    }
}