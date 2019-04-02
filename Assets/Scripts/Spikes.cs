using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject ballDestroyed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            PipesManager.Instance.HideBall();
            Instantiate(ballDestroyed, collision.transform.position, ballDestroyed.transform.rotation);
        }
    }
}
