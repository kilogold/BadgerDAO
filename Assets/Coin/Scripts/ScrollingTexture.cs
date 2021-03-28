using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrollingTexture : MonoBehaviour
{
    public float velocity;
    public SpriteRenderer spriteRenderer;

    private const float MAX = 10.12f;
    private const float MIN = 5.0f;

    void Start()
    {
        spriteRenderer.size = new Vector2(Random.Range(MIN,MAX), spriteRenderer.size.y);
    }

    void Update()
    {
        float newOffset = spriteRenderer.size.x + velocity * Time.deltaTime;

        if (newOffset > MAX)
            newOffset = MIN + (newOffset - MAX);
        else if (newOffset < MIN)
            newOffset = MAX - (MIN - newOffset);
        
        
        spriteRenderer.size = new Vector2(newOffset, spriteRenderer.size.y);
    }
}
