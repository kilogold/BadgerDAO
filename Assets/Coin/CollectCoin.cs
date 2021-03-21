using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinCollectSound;
    public GameObject glimmerEffect;

    public UnityEvent CoinCollectedEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            CoinCollectedEvent?.Invoke();
            coinCollectSound.Play();
            Destroy(other.gameObject);
            Instantiate(glimmerEffect, other.transform.position, Quaternion.identity);
        }
    }
}
