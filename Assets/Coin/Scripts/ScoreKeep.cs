using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeep : MonoBehaviour
{
    [SerializeField] byte totalScore;
    [SerializeField] byte currentScore;
    [SerializeField] Faucet faucet;
    
    public uint CurrentScore => currentScore;

    public void Reset()
    {
        totalScore = 0;
        currentScore = 0;
        faucet.grantRequestCurrentScore = 0;
    }

    public void IncrementScore()
    {
        faucet.grantRequestCurrentScore = ++currentScore;
    }

    public void IncrementTotal()
    {
        faucet.grantRequestTotalScore = ++totalScore;
    }
    
}
