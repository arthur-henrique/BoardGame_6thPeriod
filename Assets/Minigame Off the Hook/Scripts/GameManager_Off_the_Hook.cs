using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Off_the_Hook : MonoBehaviour
{
    [SerializeField] private GameObject alert;
    private float totalTime = 5;
    public bool fishBite = false;
    // Start is called before the first frame update
    void Start()
    {
        alert.SetActive(false);
        StartCoroutine(FishCountdown());
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
        Debug.Log("FISH!");
        while (fishBite) { yield return null; }
        Debug.Log("Fish Caught!");
        yield return new WaitForSeconds(1);
        alert.SetActive(false);
        totalTime = Random.Range(3, 10);
        StartCoroutine(FishCountdown());
    }

    public void FishHooked()
    {
        fishBite = false;
    }
}
