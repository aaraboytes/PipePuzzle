using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool antiremoveable = false;
    public enum Entrances
    {
        Right,Left,Top,Bottom
    }
    public Color successColor;
    public Color failureColor;

    public Entrances[] entrances;
    public Detector[] detectors;

    Detector enter;
    [SerializeField]
    Detector exit;
    bool activated = false;

    public bool Enter(Entrances entranceNeeded)
    {
        if (activated)
            return false;
        else
            activated = true;
        foreach (Entrances entrance in entrances)
            if (entranceNeeded.Equals(entrance))
            {
                if (entrances[0] == entranceNeeded) {
                    enter = detectors[0];
                    exit = detectors[1];
                }
                else
                {
                    enter = detectors[1];
                    exit = detectors[0];
                }
                Invoke("Travel", 0.1f);
                return true;
            }
        return false;
    }
    public void Travel()
    {
        if(exit.Travel())
            GetComponent<SpriteRenderer>().color = successColor;
        else
            GetComponent<SpriteRenderer>().color = failureColor;
    }
    public void Reset()
    {
        activated = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
