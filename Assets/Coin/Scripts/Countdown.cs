using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    [SerializeField] UnityEvent onCountdownComplete;
    [SerializeField] float lengthInSeconds;
    public float TimeRemaining { get; private set; }

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        TimeRemaining = lengthInSeconds;
    }

    void Update()
    {
        if (TimeRemaining <= 0)
            return;

        TimeRemaining = Mathf.Max(0, TimeRemaining - Time.deltaTime);
        if (TimeRemaining <= 0)
        {
            onCountdownComplete?.Invoke();
        }
    }
}
