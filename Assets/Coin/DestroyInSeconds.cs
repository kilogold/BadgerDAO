using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    public float seconds;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
