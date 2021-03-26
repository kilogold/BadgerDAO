using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class LoadMetamask : MonoBehaviour
{
    // use WalletAddress function from web3.jslib
    [DllImport("__Internal")] private static extern string WalletAddress();

    [SerializeField] InputField input;
    
    public void InputMetamaskWalletAddress()
    {
        input.text = WalletAddress();
    }
}


