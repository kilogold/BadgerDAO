using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreToTextBinding : MonoBehaviour
{
    public ScoreKeep scoreKeep;
    public Text textUI;
    private void OnEnable()
    {
        textUI.text = scoreKeep.CurrentScore.ToString("00");
    }
}
