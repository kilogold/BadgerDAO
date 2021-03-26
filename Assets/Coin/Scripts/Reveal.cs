using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reveal : MonoBehaviour
{
    public Faucet faucet;
    public SpriteRenderer result;
    public TextMeshPro cubeText;

    public Sprite[] possibilities;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        cubeText.gameObject.SetActive(false);

        var cur = result.sprite;
        do
        {
            result.sprite = possibilities[Random.Range(0, possibilities.Length)];
        } while (cur == result.sprite);
        
        //faucet.RequestGrant();
    }

}
