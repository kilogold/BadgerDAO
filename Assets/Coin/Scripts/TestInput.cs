using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestInput : MonoBehaviour
{
    public UnityEvent OnKeyDown;
    public KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            OnKeyDown?.Invoke();
        }
    }
}
