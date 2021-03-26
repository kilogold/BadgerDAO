using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySync : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Animator animator;

    private int animatorHash;
    private void Start()
    {
        animatorHash = Animator.StringToHash("Motion");
    }

    void Update()
    {
        animator.SetFloat(animatorHash, Mathf.Abs(rigidBody.velocity.x));
    }
}
