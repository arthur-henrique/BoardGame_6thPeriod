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
    private bool isGameWon = false;

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

    private void Update()
    {
        //Checks if its in the main game, then checks if the game was won
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "GameplayLevel")
        {
            for (int i = 0; i < playersScore.Count; i++)
            {
                if (playersScore[i] >= 50 && !isGameWon)
                {
                    print(playersScore[i]);
                    isGameWon = true;
                    winner = i;
                    EndGame();
                }
            }
        }
    }

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
