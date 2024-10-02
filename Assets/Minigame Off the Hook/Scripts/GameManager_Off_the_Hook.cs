using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Off_the_Hook : MonoBehaviour
{
    [SerializeField] private GameObject alert;
    private float totalTime = 3;
    public bool fishBite = false;
    private int playerNumber;

    [SerializeField] private GameObject fish;

    private Coroutine fishCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        alert.SetActive(false);
        fishCoroutine = StartCoroutine(FishCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FishCountdown()
    {
        yield return new WaitForSeconds(totalTime);
        alert.SetActive(true);
        fishBite = true;

        while (fishBite) { yield return null; } //waits for player input

        SpawnFish(playerNumber);
        yield return new WaitForSeconds(0.5f);
        alert.SetActive(false);
        totalTime = Random.Range(3, 10);
        StartCoroutine(FishCountdown());
    }

    public void FishHooked(int n)
    {
        playerNumber = n;
        fishBite = false;
    }

    public void EndGame()
    {
        StopCoroutine(fishCoroutine);
        Debug.Log("EndGame");
    }

    private void SpawnFish(int n)
    {
        fish = Instantiate(fish, transform.position, transform.rotation);
        fish.GetComponent<MeshRenderer>().material = new Material(fish.GetComponent<MeshRenderer>().material);
        if (n == 1)
        {
            fish.GetComponent<Rigidbody>().AddForce(220 * new Vector3(-0.2f, 1, Random.Range(-0.1f,0.1f)), ForceMode.Impulse);
            fish.transform.eulerAngles = new Vector3(70,90,0);
        }

        else
        {
            fish.GetComponent<Rigidbody>().AddForce(220 * new Vector3(0.2f, 1, 0), ForceMode.Impulse);
            fish.transform.eulerAngles = new Vector3(70, -90, 0);
        }
    }
}
