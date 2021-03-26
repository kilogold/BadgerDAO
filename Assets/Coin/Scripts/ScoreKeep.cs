using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeep : MonoBehaviour
{
    [SerializeField] uint currentScore;
    [SerializeField] Faucet faucet;
    
    public uint CurrentScore => currentScore;

    public void Reset()
    {
        currentScore = 0;
        faucet.grantRequestAmount = 0;
    }

    public void IncrementScore()
    {
        currentScore = CurrentScore + 1;
        faucet.grantRequestAmount = currentScore;
    }
    
}
