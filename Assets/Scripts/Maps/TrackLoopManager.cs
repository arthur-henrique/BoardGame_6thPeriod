using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLoopManager : MonoBehaviour
{
    public static TrackLoopManager instance;

    public GameObject mainTrackParent;
    public List<Transform> mainTrackTransforms;
    public List<GameObject> playersList;
    public List<Cinemachine.CinemachineVirtualCamera> virtualCameras;

    

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
            if (i != GameManager.Instance.currentPlayerIndexTurn)
                playersList[i].gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void TurnTransition()
    {
        if (GameManager.Instance.currentPlayerIndexTurn >= playersList.Count -1 )
        {
            GameManager.Instance.currentPlayerIndexTurn = 0;
        }
        else
        {
            GameManager.Instance.currentPlayerIndexTurn++;
        }

        for (int i = 0; i < playersList.Count; i++)
        {
            if (i != GameManager.Instance.currentPlayerIndexTurn)
            {
                playersList[i].gameObject.GetComponent<PlayerMovement>().enabled = false;
                virtualCameras[i].Priority = 0;
            }
        }

        playersList[GameManager.Instance.currentPlayerIndexTurn].gameObject.GetComponent<PlayerMovement>().enabled = true;
        virtualCameras[GameManager.Instance.currentPlayerIndexTurn].Priority = 1;
    }

    public void UpdatePositionByforce()
    {
        for (int i = 0;i < playersList.Count;i++)
        {
            playersList[i].transform.position = mainTrackTransforms[playersList[i].GetComponent<PlayerMovement>().currentIndex].position;
        }
    }
}
