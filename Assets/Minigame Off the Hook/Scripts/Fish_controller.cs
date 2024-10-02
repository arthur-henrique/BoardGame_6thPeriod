using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_controller : MonoBehaviour
{
    [SerializeField] private Material material;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        mr.material.color = Random.ColorHSV(0,1,0.5f,1,0.5f,1);
    }
}
