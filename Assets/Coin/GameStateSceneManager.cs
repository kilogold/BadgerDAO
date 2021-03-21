using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSceneManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
