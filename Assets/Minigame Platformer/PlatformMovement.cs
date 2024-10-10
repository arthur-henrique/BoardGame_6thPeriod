using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private float speed;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed*Time.deltaTime);
        if (transform.position == targetPos.position)
        {
            targetPos.position = originalPos;
            originalPos = transform.position;
        }
    }

    IEnumerator MovePlatform()
    {
        originalPos = transform.position;
        while(transform.position != targetPos.position)
        {
            
        }
        yield return new WaitForSeconds(2);
        targetPos.position = originalPos;
        StartCoroutine(MovePlatform());
    }
}
