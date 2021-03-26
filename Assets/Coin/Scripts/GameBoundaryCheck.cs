using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameBoundaryCheck : MonoBehaviour
{
    [SerializeField] UnityEvent OnOutsideBounds;
    [SerializeField] LayerMask boundsLayer;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnOutsideBounds?.Invoke();
    }
}
