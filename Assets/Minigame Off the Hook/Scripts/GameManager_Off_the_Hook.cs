using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Off_the_Hook : MonoBehaviour
{
    [SerializeField] private GameObject alert;
    private float totalTime = 3;
    public bool fishBite = false;
    private int playerNumber;
    public bool gameOver = false;
    [SerializeField] private GameObject[] pointHolder;
    [SerializeField] private Camera cameraFish;

    [SerializeField] private GameObject fish;

    private Vector3 ogCameraPos;

    private Coroutine fishCoroutine;

    [SerializeField]
    private GameObject finishMinigameCanvas;

    // Start is called before the first frame update
    void Start()
    {

        totalTime = Random.Range(3, 10);
        alert.SetActive(false);
        fishCoroutine = StartCoroutine(FishCountdown());

        //Hides scores
        for(int i = 0;i < 3;i++)
        {
            pointHolder[0].transform.GetChild(i).gameObject.SetActive(false);
            pointHolder[1].transform.GetChild(i).gameObject.SetActive(false);
        }

        ogCameraPos = Camera.main.transform.position;
    }

    IEnumerator FishCountdown()
    {
        StartCoroutine(moveCamera(totalTime)); //starts moving camera towards fish
        yield return new WaitForSeconds(totalTime); 

        alert.SetActive(true); //Sends fish alert
        StartCoroutine(moveCameraBack()); //Quickly moves camera back
        fishBite = true; //Enables player input

        while (fishBite) { yield return null; } //waits for player input

        //Fish animation
        SpawnFish(playerNumber);
        yield return new WaitForSeconds(0.5f);

        //Sets up next round
        alert.SetActive(false); 
        totalTime = Random.Range(3, 10); 
        if(!gameOver)
            StartCoroutine(FishCountdown());
    }

    //Called by Player Controller when a fish is hooked
    public void FishHooked(int player, int score)
    {
        playerNumber = player;
        fishBite = false;
        pointHolder[player-1].transform.GetChild(score-1).gameObject.SetActive(true); //Adds the point to the UI
    }

    //Called by Player at the end of the game
    public void EndGame()
    {
        gameOver = true;
        StopCoroutine(fishCoroutine);
        Instantiate(finishMinigameCanvas);
    }

    private void SpawnFish(int n)
    {
        fish = Instantiate(fish, transform.position, transform.rotation); //summons new fish
        fish.GetComponent<MeshRenderer>().material = new Material(fish.GetComponent<MeshRenderer>().material); //Random material color

        //Rotates and launches the fish at the correct player
        if (n == 1)
        {
            fish.GetComponent<Rigidbody>().AddForce(220 * new Vector3(-0.2f, 1, Random.Range(-0.1f,0.1f)), ForceMode.Impulse);
            fish.transform.eulerAngles = new Vector3(70,90,0);
        }
        else
        {
            fish.GetComponent<Rigidbody>().AddForce(220 * new Vector3(0.2f, 1, Random.Range(-0.1f, 0.1f)), ForceMode.Impulse);
            fish.transform.eulerAngles = new Vector3(70, -90, 0);
        }
    }

    //Moves camera towards fish
    IEnumerator moveCamera(float totalTime)
    {
        Vector3 newPos = new Vector3(cameraFish.transform.position.x, cameraFish.transform.position.y, cameraFish.transform.position.z + totalTime );

        while (cameraFish.transform.position != newPos)
        {
            cameraFish.transform.position = Vector3.MoveTowards(cameraFish.transform.position, newPos, Time.deltaTime);
            yield return null;
        }
    }

    //Returns camera to original position
    IEnumerator moveCameraBack()
    {
        while (cameraFish.transform.position != ogCameraPos)
        {
            cameraFish.transform.position = Vector3.MoveTowards(cameraFish.transform.position, ogCameraPos, 25*Time.deltaTime);
            yield return null;
        }
    }

}
