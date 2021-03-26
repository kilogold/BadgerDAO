using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Emitter : MonoBehaviour
{
    public GameObject particle;

    public float minWait;
    public float maxWait;

    public float horRandMin;
    public float horRandMax;
    
    public float vertRandMin;
    public float vertRandMax;

    public UnityEvent OnEmitEvent;
    
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            Emit();
        }
    }

    public void Emit()
    {
        OnEmitEvent?.Invoke();
        var newParticle = Instantiate(particle, transform.position, transform.rotation);
        GenerateRandom(newParticle.GetComponent<Rigidbody2D>());
    }

    public void GenerateRandom(Rigidbody2D instance)
    {
        float horizontalRand = Random.Range(horRandMin, horRandMax);
        float verticalRand = Random.Range(vertRandMin, vertRandMax);
        instance.AddForce(new Vector2(horizontalRand, verticalRand));
    }
}
