using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutDetector : MonoBehaviour
{
    public bool isGate;
    public int shortcutIndex;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isGate)
            {
                other.gameObject.GetComponent<PlayerMovement>().isOnEvent = true;
                other.gameObject.GetComponent<PlayerMovement>().gateIndex = shortcutIndex;
            }
            else
            {
                //other.gameObject.GetComponent<PlayerMovement>().SetNextIndex(indexFactor);
            }

        }
    }
}
