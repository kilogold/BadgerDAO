using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinCollectSound;
    public GameObject glimmerEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCollectSound.Play();
            Destroy(other.gameObject);
            Instantiate(glimmerEffect, other.transform.position, Quaternion.identity);
        }
    }
}
