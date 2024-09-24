using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLoopManager : MonoBehaviour
{
    public static TrackLoopManager instance;

    public GameObject mainTrackParent;
    public List<Transform> mainTrackTransforms;
    public List<GameObject> playersList;
    public int currentPlayerIndexTurn;

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

        for (int i = 0; i < playersList.Count; i++)
        {
            if (i != currentPlayerIndexTurn)
                playersList[i].gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void TurnTransition()
    {
        if (currentPlayerIndexTurn >= playersList.Count -1 )
        {
            currentPlayerIndexTurn = 0;
        }
        else
        {
            currentPlayerIndexTurn++;
        }

        for (int i = 0; i < playersList.Count; i++)
        {
            if (i != currentPlayerIndexTurn)
                playersList[i].gameObject.GetComponent<PlayerMovement>().enabled = false;
        }

        playersList[currentPlayerIndexTurn].gameObject.GetComponent<PlayerMovement>().enabled = true;
    }
}
