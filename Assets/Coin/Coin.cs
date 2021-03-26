using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    [SerializeField]
    AudioSource sfxHitGround;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gameObject.layer = LayerMask.NameToLayer("FreeFall");
            sfxHitGround.pitch += Random.Range(-0.2f, 0.5f);
            sfxHitGround.Play();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
