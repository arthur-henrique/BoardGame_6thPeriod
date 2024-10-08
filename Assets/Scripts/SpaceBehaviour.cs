using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceBehaviour : MonoBehaviour
{
    public enum spaceType { minigame,quiz,coinGain,coinLose,shortcut};
    public spaceType tile;
    public Material minigameMat;
    public Material coinGainMat;
    public Material coinLoseMat;
    public Material shortcutMat;

    private GameManager gm;
    public int timeToWait = 2;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        ChooseColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoTheThing()
    {
        switch (tile)
        {
            case spaceType.minigame:
                StartMinigame();
                break;
            case spaceType.coinGain:
                CoinGain();
                break;
            case spaceType.coinLose:
                CoinLose();
                break;
            case spaceType.shortcut:
                Shortcut();
                break;
        }
        
    }


    [ContextMenu("Mudar de Cor")]
    public void ChooseColor()
    {
        switch (tile)
        {
            case spaceType.minigame:
                gameObject.GetComponent<MeshRenderer>().sharedMaterial = minigameMat;
                break;
            case spaceType.coinGain:
                gameObject.GetComponent<MeshRenderer>().sharedMaterial = coinGainMat;
                break;
            case spaceType.coinLose:
                gameObject.GetComponent<MeshRenderer>().sharedMaterial = coinLoseMat;
                break;
            case spaceType.shortcut:
                gameObject.GetComponent<MeshRenderer>().sharedMaterial = shortcutMat;
                break;
        }
    }

    private void CoinGain()
    {
        Debug.Log("Coin Gain");
        gm.playersScore[gm.currentPlayerIndexTurn] += 3;
        Debug.Log(gm.playersScore[gm.currentPlayerIndexTurn]);
        TrackLoopManager.instance.TurnTransition();
    }

    private void CoinLose()
    {
        Debug.Log("Coin Lose");
        if (gm.playersScore[gm.currentPlayerIndexTurn] > 3)
            gm.playersScore[gm.currentPlayerIndexTurn] -= 3;
        else
            gm.playersScore[gm.currentPlayerIndexTurn] = 0;
        Debug.Log(gm.playersScore[gm.currentPlayerIndexTurn]);
        TrackLoopManager.instance.TurnTransition();
    }

    private void Shortcut()
    {
        TrackLoopManager.instance.TurnTransition();
    }

    private void StartMinigame()
    {
        int randonMinigame = Random.Range(2, 5);
        StartCoroutine(MinigameTimerCountdown(randonMinigame));
    }

    private IEnumerator MinigameTimerCountdown(int sceneToLoad)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        GameManager.Instance.needsToUpdateTurn = true;
        BoardLevelManager.Instance.CallChangeLevel(sceneToLoad);
    }
}
