using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public float hMagnitude;
    public Rigidbody2D body;
    
    void Update()
    {
        float axis = Input.GetAxis("Horizontal") * hMagnitude;
        body.AddForce(new Vector2(axis, 0), ForceMode2D.Force);
    }
}
