using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using Contracts.Faucet.ContractDefinition;
using Nethereum.JsonRpc.UnityClient;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Faucet : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    [SerializeField] string networkUrl;
    [SerializeField] string networkExplorerUrl;

    // Faucet address
    [SerializeField] string contractAddress;

    // Gas Fee wallet.
    [SerializeField] string gasWalletAddress;
    [SerializeField] string gasWalletAddressPrivate;
    [SerializeField] string grantTransactionHash;

    // User Wallet
    [SerializeField] InputField inputWalletAddress;
    
    // Read-only API access. TODO: Move all config to a ScriptableObject.
    public string ContractAddress => contractAddress;
    public string NetworkUrl => networkUrl;


    public byte grantRequestCurrentScore;
    public byte grantRequestTotalScore;

    public UnityEvent<BigInteger> OnGetElapsedRate;
    public UnityEvent<BigInteger> OnGetElapsedTime;
    public UnityEvent<string> OnRequestGrant;

    public void RequestZeroGrant()
    {
        // Emulate zero-score. Ensure total score is non-zero
        // in case of downstream divide-by-zero exceptions.
        StartCoroutine(RequestGrantCR(0,1));
    }
    
    public void RequestGrant()
    {
        StartCoroutine(RequestGrantCR(grantRequestCurrentScore, grantRequestTotalScore));
    }

    public void GetElapsedTime()
    {
        StartCoroutine(GetElapsedTimeCR());
    }
    
    public void GetElapsedRate()
    {
        StartCoroutine(GetElapsedRateCR());
    }

    public void LaunchEtherscan()
    {
        string url = networkExplorerUrl + grantTransactionHash;
        Debug.Log("Opening URL: "+ url);
        
#if !UNITY_EDITOR && UNITY_WEBGL
        OpenNewTab(url);
        return;
#else
        Application.OpenURL(url);
#endif
    }
    
    public IEnumerator GetElapsedRateCR()
    {
        Debug.Log("Creating Elapsed Rate");
        var queryRequest = new QueryUnityRequest<ParticipantRetryTimeFunction, ParticipantRetryTimeOutputDTO>(networkUrl, contractAddress);

        Debug.Log("Executing Elapsed Rate Query");
        yield return queryRequest.Query(new ParticipantRetryTimeFunction(), contractAddress);

        Debug.Log($"Invoking Elapsed Rate Query Callback With Value: {queryRequest.Result.ReturnValue1}");
        OnGetElapsedRate?.Invoke(queryRequest.Result.ReturnValue1);
    }

    public IEnumerator GetElapsedTimeCR()
    {
        Debug.Log("Creating ElapsedTimeQuery");
        var queryRequest = new QueryUnityRequest<GetElapsedTimeFunction, GetElapsedTimeOutputDTO>(networkUrl, contractAddress);

        Debug.Log("Executing ElapsedTimeQuery");
        yield return queryRequest.Query(new GetElapsedTimeFunction() { FromAddress = gasWalletAddress, Participant = inputWalletAddress.text}, contractAddress);

        Debug.Log($"Invoking ElapsedTimeQuery Callback with value: {queryRequest.Result.ReturnValue1}");
        OnGetElapsedTime?.Invoke(queryRequest.Result.ReturnValue1);
    }

    public IEnumerator RequestGrantCR(byte score, byte fromTotal)
    {
        var transactionTransferRequest = new TransactionSignedUnityRequest(networkUrl, gasWalletAddressPrivate);
        var transactionMessage = new GrantFunction
        {
            FromAddress = gasWalletAddress,
            Recipient = inputWalletAddress.text,
            Score = score,
            FromTotal = fromTotal
        };

        yield return transactionTransferRequest.SignAndSendTransaction(transactionMessage, contractAddress);

        if (transactionTransferRequest.Exception != null)
        {
            Debug.LogError(transactionTransferRequest.Exception.Message);
        }

        string transactionHash = transactionTransferRequest.Result;
        Debug.Log(transactionHash);

        OnRequestGrant?.Invoke(transactionHash);
        grantTransactionHash = transactionHash;
    }
}
