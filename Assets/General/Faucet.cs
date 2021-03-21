using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Contracts.Contracts.Faucet.ContractDefinition;
using Nethereum.JsonRpc.UnityClient;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Faucet : MonoBehaviour
{

    [SerializeField] string networkUrl = "https://ropsten.infura.io/v3/e95812f105a340cda6bfd2c67bc22f69";

    // Faucet address
    [SerializeField] string contractAddress;

    // Gas Fee wallet.
    [SerializeField] string gasWalletAddress;
    [SerializeField] string gasWalletAddressPrivate;
    [SerializeField] string grantTransactionHash;

    // User Wallet
    [SerializeField] InputField inputWalletAddress;
    
    public uint grantRequestAmount;
    public UnityEvent<BigInteger> OnGetElapsedTime;
    public UnityEvent OnCanParticipate_Success;
    public UnityEvent OnCanParticipate_Fail;
    public UnityEvent<string> OnRequestGrant;

    public void RequestGrant(uint score)
    {
        StartCoroutine(RequestGrantCR(score));
    }
    public void RequestGrant()
    {
        StartCoroutine(RequestGrantCR(grantRequestAmount));
    }

    public void GetElapsedTime()
    {
        StartCoroutine(GetElapsedTimeCR());
    }

    public void CanParticipate()
    {
        StartCoroutine(CanParticipateCR());
    }
    
    public void LaunchEtherscan()
    {
        string url = "https://ropsten.etherscan.io/tx/" + grantTransactionHash;
        Debug.Log("Opening URL: "+ url);
        Application.OpenURL(url);
    }


    private IEnumerator CanParticipateCR()
    {
        var queryRequest = new QueryUnityRequest<CanParticipateFunction, CanParticipateOutputDTO>(networkUrl, contractAddress);
        yield return queryRequest.Query(new CanParticipateFunction() {FromAddress = gasWalletAddress}, contractAddress);
        
        if(queryRequest.Result.ReturnValue1)
            OnCanParticipate_Success?.Invoke();
        else
            OnCanParticipate_Fail?.Invoke();
    }


    private IEnumerator GetElapsedTimeCR()
    {
        var queryRequest = new QueryUnityRequest<GetElapsedTimeFunction, GetElapsedTimeOutputDTO>(networkUrl, contractAddress);

        yield return queryRequest.Query(new GetElapsedTimeFunction() { FromAddress = gasWalletAddress }, contractAddress);

        OnGetElapsedTime?.Invoke(queryRequest.Result.ReturnValue1);
    }

    private IEnumerator RequestGrantCR(uint score)
    {
        var transactionTransferRequest = new TransactionSignedUnityRequest(networkUrl, gasWalletAddressPrivate);
        var transactionMessage = new GrantFunction
        {
            FromAddress = gasWalletAddress,
            Recipient = inputWalletAddress.text,
            Score = score
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
