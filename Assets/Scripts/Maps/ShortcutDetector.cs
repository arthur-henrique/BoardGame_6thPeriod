using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutDetector : MonoBehaviour
{
    public bool isGate;
    public int indexFactor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(isGate)
            {
                other.gameObject.GetComponent<PlayerMovement>().isOnEvent = true;
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().currentIndex = indexFactor;
            }
            
        }
    }
}
