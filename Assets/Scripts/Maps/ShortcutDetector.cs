using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutDetector : MonoBehaviour
{
    public bool isGate;
    public int indexFactor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isGate)
            {
                other.gameObject.GetComponent<PlayerMovement>().isOnEvent = true;
                other.gameObject.GetComponent<PlayerMovement>().gateIndex = indexFactor;
            }
            else
            {
                //other.gameObject.GetComponent<PlayerMovement>().SetNextIndex(indexFactor);
            }

        }
    }
}
