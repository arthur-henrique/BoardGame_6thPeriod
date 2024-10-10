using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventStarter : MonoBehaviour
{
    public int timeToWait = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int randonMinigame = Random.Range(2, 5);
            StartCoroutine(MinigameTimerCountdown(randonMinigame));
        }
    }

    private IEnumerator MinigameTimerCountdown(int sceneToLoad)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        GameManager.Instance.needsToUpdateTurn = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}
