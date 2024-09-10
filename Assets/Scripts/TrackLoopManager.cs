using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLoopManager : MonoBehaviour
{
    public static TrackLoopManager instance;

    public GameObject mainTrackParent;
    public List<Transform> mainTrackTransforms;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        for (int i = 0; i < mainTrackParent.transform.childCount; i++)
        {
            mainTrackTransforms.Add(mainTrackParent.transform.GetChild(i).transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
