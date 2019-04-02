using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Pipe.Entrances directionToShootRay;
    public bool Travel()
    {
        RaycastHit2D hit;
        Pipe touchedPipe = null;
        switch (directionToShootRay)
        {
            case Pipe.Entrances.Right:
                hit = Physics2D.Raycast(transform.position, Vector2.right, 0.1f);
                if (hit.collider)
                {
                    if (hit.collider.CompareTag("End"))
                        break;
                    Debug.Log(name+"->Collision with"+hit.collider.name);
                    touchedPipe = hit.collider.GetComponent<Pipe>();
                    return touchedPipe.Enter(Pipe.Entrances.Left);
                }
                break;
            case Pipe.Entrances.Left:
                hit = Physics2D.Raycast(transform.position, Vector2.left, 0.1f);
                if (hit.collider)
                {
                    if (hit.collider.CompareTag("End"))
                        break;
                    Debug.Log(name + "->Collision with" + hit.collider.name);
                    touchedPipe = hit.collider.GetComponent<Pipe>();
                    return touchedPipe.Enter(Pipe.Entrances.Right);
                }
                break;
            case Pipe.Entrances.Top:
                hit = Physics2D.Raycast(transform.position, Vector2.up, 0.1f);
                if (hit.collider)
                {
                    if (hit.collider.CompareTag("End"))
                        break;
                    Debug.Log(name + "->Collision with" + hit.collider.name);
                    touchedPipe = hit.collider.GetComponent<Pipe>();
                    return touchedPipe.Enter(Pipe.Entrances.Bottom);
                }
                break;
            case Pipe.Entrances.Bottom:
                hit = Physics2D.Raycast(transform.position, Vector2.down,0.1f);
                if (hit.collider)
                {
                    if (hit.collider.CompareTag("End"))
                        break;
                    Debug.Log(name + "->Collision with" + hit.collider.name);
                    touchedPipe = hit.collider.GetComponent<Pipe>();
                    return touchedPipe.Enter(Pipe.Entrances.Top);
                }
                break;
        }
        GameObject ball = null;
        switch (directionToShootRay)
        {
            case Pipe.Entrances.Right:
                ball = Instantiate(PipesManager.Instance.ball, transform.position, Quaternion.identity);
                ball.GetComponent<Rigidbody2D>().AddForce(Vector2.right * PipesManager.Instance.ballForce);
                break;
            case Pipe.Entrances.Left:
                ball = Instantiate(PipesManager.Instance.ball, transform.position, Quaternion.identity);
                ball.GetComponent<Rigidbody2D>().AddForce(Vector2.left * PipesManager.Instance.ballForce);
                break;
            case Pipe.Entrances.Top:
                ball = Instantiate(PipesManager.Instance.ball, transform.position, Quaternion.identity);
                ball.GetComponent<Rigidbody2D>().AddForce(Vector2.up * PipesManager.Instance.ballForce);
                break;
            case Pipe.Entrances.Bottom:
                ball = Instantiate(PipesManager.Instance.ball, transform.position, Quaternion.identity);
                ball.GetComponent<Rigidbody2D>().AddForce(Vector2.down * PipesManager.Instance.ballForce);
                break;
        }
        return false;
    }
}
