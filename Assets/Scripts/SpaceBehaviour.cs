using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBehaviour : MonoBehaviour
{
    public enum spaceType { minigame,quiz,coinGain,coinLose,shortcut};
    public spaceType tile;
    public Material coinGainMat;
    public Material coinLoseMat;
    public Material shortcutMat;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoTheThing()
    {
        switch (tile)
        {
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
    }

    private void CoinLose()
    {
        Debug.Log("Coin Lose");
        if (gm.playersScore[gm.currentPlayerIndexTurn] > 3)
            gm.playersScore[gm.currentPlayerIndexTurn] -= 3;
        else
            gm.playersScore[gm.currentPlayerIndexTurn] = 0;
        Debug.Log(gm.playersScore[gm.currentPlayerIndexTurn]);
    }

    private void Shortcut()
    {

    }
}
