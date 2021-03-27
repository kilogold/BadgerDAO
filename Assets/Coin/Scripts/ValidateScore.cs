using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValidateScore : MonoBehaviour
{
    [SerializeField] UnityEvent OnValidScore;
    [SerializeField] UnityEvent OnInvalidScore;
    [SerializeField] ScoreKeep score;
    
    public void Validate()
    {
        if(score.CurrentScore == 0)
            OnInvalidScore?.Invoke();
        else
            OnValidScore?.Invoke();
    }

}
