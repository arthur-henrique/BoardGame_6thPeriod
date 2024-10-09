using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutDetector : MonoBehaviour
{
    public bool isGate;
    public int shortcutIndex;
    public GameObject arrows;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player 1")||other.CompareTag("Player 2"))
        {
            if(isGate)
            {
                other.gameObject.GetComponent<PlayerMovement>().isOnEvent = true;
                other.gameObject.GetComponent<PlayerMovement>().gateIndex = shortcutIndex;
                arrows.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        arrows.SetActive(false);
    }
}
