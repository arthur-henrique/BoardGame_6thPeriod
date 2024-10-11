using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<string> playerNames = new List<string>();
    public List<int> playersScore = new List<int>();
    public List<int> playerLaps = new List<int>();

    public List<int> playersIndexi = new List<int>();
    public List<Vector3> playerCamerasRotation = new List<Vector3>();
    public bool needsToUpdateBoardLevel = false;
    public bool needsToUpdateTurn = false;
    public int currentPlayerIndexTurn;
    public int winner = 0;

    [SerializeField]
    private GameObject finishGameCanvas;

    [Header("Player Customizations")]

    [SerializeField]
    private TMP_InputField playerOneName;

    [SerializeField]
    private TMP_InputField playerTwoName;

    [SerializeField]
    private PlayerCustomization playerOne;

    [SerializeField]
    private PlayerCustomization playerTwo;

    private Material materialBodyOne;
    private Material materialBodyTwo;
    private Material materialFaceOne;
    private Material materialFaceTwo;

    public bool hasSetInitialPos = false;

    Scene currentScene;
    string sceneName;
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

    private void OnEnable()
    {
        
        
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "GameplayLevel")
        {
            for (int i = 0; i < playersScore.Count; i++)
            {
                if (playersScore[i] >= 3)
                {
                    print(playersScore[i]);
                    playersScore[i] = 0;
                    winner = i;
                    EndGame();
                }
            }
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

    public void SavePlayerInfo()
    {
        materialBodyOne = playerOne.bodyMaterial;
        materialBodyTwo = playerTwo.bodyMaterial;
        materialFaceOne = playerOne.faceMaterial;
        materialFaceTwo = playerTwo.faceMaterial;

        playerNames.Add(playerOneName.text);
        playerNames.Add(playerTwoName.text);

    }

    public void LoadPlayerInfo()
    {

    }

    public void EndGame()
    {
        Instantiate(finishGameCanvas);
        Time.timeScale = 0.0f;
    }


}
