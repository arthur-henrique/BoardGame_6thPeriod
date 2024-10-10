using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Platformer : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] float cameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCamera());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Moves camera towards
    IEnumerator MoveCamera()
    {
        Vector3 newPos = targetPos.position;

        while (transform.position != newPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, 010f*cameraSpeed*Time.deltaTime);
            yield return null;
        }
    }

}
