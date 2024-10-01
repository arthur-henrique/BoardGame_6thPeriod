using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> players = new List<GameObject>();
    public List<string> playerNames = new List<string>();
    public List<int> playersScore = new List<int>();

    public List<int> playersIndexi = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    
    //public void SetPlayersInitialPosition(Vector3 playerOnePos, Vector3 playerTwoPos)
    //{
    //    players[0].transform.position = new Vector3(playerOnePos.x, playerOnePos.y + 1, playerOnePos.z);
    //    players[1].transform.position = new Vector3(playerTwoPos.x, playerTwoPos.y + 1, playerTwoPos.z);
    //}

    public void ChangeLevels(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
