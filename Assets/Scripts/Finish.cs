using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject confettiParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Instantiate(confettiParticle, transform.position, confettiParticle.transform.rotation);
            Debug.Log("GAME OVER!");
        }
    }
}
