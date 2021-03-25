using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputFieldValidation : MonoBehaviour
{
    [SerializeField] UnityEvent<bool> IsInputFieldPopulatedEvent;

    [SerializeField] InputField inputField;
    
    public void Validate()
    { 
        IsInputFieldPopulatedEvent?.Invoke(!string.IsNullOrEmpty(inputField.text));
    }

    private void OnEnable()
    {
        Validate();
    }
}
