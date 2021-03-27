using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrollingTexture : MonoBehaviour
{
    public float velocity;
    public SpriteRenderer renderer;

    private const float MAX = 10.12f;
    private const float MIN = 5.0f;

    void Start()
    {
        renderer.size = new Vector2(Random.Range(MIN,MAX), renderer.size.y);
    }

    void Update()
    {
        float newOffset = renderer.size.x + velocity * Time.deltaTime;

        if (newOffset > MAX)
            newOffset = MIN + (newOffset - MAX);
        else if (newOffset < MIN)
            newOffset = MAX - (MIN - newOffset);
        
        
        renderer.size = new Vector2(newOffset, renderer.size.y);
    }
}
