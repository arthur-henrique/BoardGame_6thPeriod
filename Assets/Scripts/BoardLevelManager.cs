using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLevelManager : MonoBehaviour
{
    //public List<Transform> initialTransforms = new List<Transform>();
    public CinemachineVirtualCamera[] cinemachineVirtualCameras;
    public CinemachineTransposer[] transposer;
    void Start()
    {
        //if(GameManager.Instance != null)
        //{
        //    GameManager.Instance.SetPlayersInitialPosition(initialTransforms[0].position, initialTransforms[1].position);
        //    for (int i = 0; i < GameManager.Instance.players.Count; i++)
        //        TrackLoopManager.instance.playersList.Add(GameManager.Instance.players[i]);
        //}

        if(GameManager.Instance != null && GameManager.Instance.needsToUpdateBoardLevel)
        {
            GetIndexFromManager();
            TrackLoopManager.instance.UpdatePositionByforce();

            if(GameManager.Instance.needsToUpdateTurn)
            {
                GameManager.Instance.needsToUpdateTurn = false;
                TrackLoopManager.instance.TurnTransition();
            }
        }

        for (int i = 0; i < cinemachineVirtualCameras.Length; i++)
        {
            transposer[i] = cinemachineVirtualCameras[i].GetCinemachineComponent<CinemachineTransposer>();
            if (GameManager.Instance.playerCamerasRotation.Count != 0)
            {
                transposer[i].m_FollowOffset = GameManager.Instance.playerCamerasRotation[i];
            }
        }
    }

    public void UpdateIndex()
    {
        for (int i = 0; i < GameManager.Instance.playersIndexi.Count; i++)
        {
            GameManager.Instance.playersIndexi[i] = TrackLoopManager.instance.playersList[i].GetComponent<PlayerMovement>().currentIndex;
        }
        GameManager.Instance.needsToUpdateBoardLevel = true;
    }
    public void GetIndexFromManager()
    {
        for (int i = 0; i < GameManager.Instance.playersIndexi.Count; i++)
        {
            TrackLoopManager.instance.playersList[i].GetComponent<PlayerMovement>().currentIndex = GameManager.Instance.playersIndexi[i];
        }
    }

    public void CallChangeLevel(int levelToGo)
    {
        UpdateIndex();
        GameManager.Instance.ChangeLevels(levelToGo);
    }
}
