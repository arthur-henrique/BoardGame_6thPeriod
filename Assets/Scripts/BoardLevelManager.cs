using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLevelManager : MonoBehaviour
{
    //public List<Transform> initialTransforms = new List<Transform>();
    // Start is called before the first frame update
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
