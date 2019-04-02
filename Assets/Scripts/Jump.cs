using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Sprite[] states;
    public float[] stateForces;
    int currentState = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D ballBody = collision.gameObject.GetComponent<Rigidbody2D>();
            ballBody.AddForce(Vector2.up * stateForces[currentState],ForceMode2D.Impulse);
        }
    }
    public void ChangeState()
    {
        currentState++;
        if (currentState > 2)
            currentState = 0;
        Debug.Log(currentState);
        GetComponent<SpriteRenderer>().sprite = states[currentState];
    }
}
