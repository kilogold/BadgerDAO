using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TimerTextBinding : MonoBehaviour
{
    [SerializeField] private TextMeshPro textObj;
    [SerializeField] private Countdown countdownOBj;
    void Update()
    {
        textObj.text = countdownOBj.TimeRemaining.ToString("00");
    }
}
