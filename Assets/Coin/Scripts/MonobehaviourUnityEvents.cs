using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonobehaviourUnityEvents : MonoBehaviour
{
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onAwake;
    [SerializeField] UnityEvent onEnable;
    [SerializeField] UnityEvent onDisable;
    [SerializeField] UnityEvent onDestroy;

    private void Awake()
    {
        onAwake?.Invoke();
    }

    void Start()
    {
        onStart?.Invoke();
    }

    private void OnEnable()
    {
        onEnable?.Invoke();
    }

    private void OnDisable()
    {
        onDisable?.Invoke();
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke();
    }
}
