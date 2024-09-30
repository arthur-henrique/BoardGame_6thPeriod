using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Off_the_Hook : MonoBehaviour
{
    InputAction fish;
    int score = 0;
    int playerNumber;
    [SerializeField] GameObject manager;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerControl inputsGame = new();
        if (gameObject.CompareTag("Player 1"))
        {
            fish = inputsGame.Controls_Off_the_Hook.FishP1;
            playerNumber = 1;
        }
        if (gameObject.CompareTag("Player 2"))
        {
            fish = inputsGame.Controls_Off_the_Hook.FishP2;
            playerNumber = 2;
        }

    }

    private void OnEnable()
    {
        fish.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (fish.WasPressedThisFrame() && manager.GetComponent<GameManager_Off_the_Hook>().fishBite)
        {
            score++;
            Debug.Log("Player "+playerNumber+" = "+score);
            manager.GetComponent<GameManager_Off_the_Hook>().FishHooked();
        }
        
    }
}
