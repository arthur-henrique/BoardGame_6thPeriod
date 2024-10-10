using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private GameObject finishMinigameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player 1"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playersScore[0] = GameObject.Find("GameManager").GetComponent<GameManager>().playersScore[0] + 5;
            Instantiate(finishMinigameCanvas);
            Time.timeScale = 0;
        }

        if (collision.transform.CompareTag("Player 2"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playersScore[1] = GameObject.Find("GameManager").GetComponent<GameManager>().playersScore[1] + 5;
            Instantiate(finishMinigameCanvas);
            Time.timeScale = 0;
        }
    }
}
