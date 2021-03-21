using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeep : MonoBehaviour
{
    [SerializeField] int currentScore;

    public int CurrentScore => currentScore;

    public void Reset()
    {
        currentScore = 0;
    }

    public void IncrementScore()
    {
        currentScore = CurrentScore + 1;
    }
    
}
