using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int currentIndex = 0;
    public int speedMod = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TrackLoopManager.instance.mainTrackTransforms[currentIndex].position, Time.deltaTime * speedMod);

        if (Vector3.Distance(transform.position, TrackLoopManager.instance.mainTrackTransforms[currentIndex].position) <= 0.1)
        {
            currentIndex++;
            if(currentIndex >= TrackLoopManager.instance.mainTrackTransforms.Count)
            {
                currentIndex = 0;
            }
        }
    }
}
