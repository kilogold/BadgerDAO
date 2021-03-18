using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float force;

    public float hMagnitude;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }

        float axis = Input.GetAxis("Horizontal") * hMagnitude;
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(axis, 0), ForceMode2D.Force);
        
        Debug.Log(axis);

    }
}
