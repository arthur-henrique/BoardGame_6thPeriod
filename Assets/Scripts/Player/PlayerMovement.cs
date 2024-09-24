using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public InputAction forward, side, roll;

    public int currentIndex = 0;
    public int indexToGo = 0;
    public int gateIndex = 0;
    public int speedMod = 1;

    public bool isOnTurn = false;
    public bool isMoving = false;
    public bool isOnEvent = false;
    public bool canRoll = true;

    public int playerIndex;

    private void Awake()
    {
        instance = this;
        PlayerControl inputActions = new PlayerControl();
        forward = inputActions.PlayerControllers.Forward;
        side = inputActions.PlayerControllers.Sideward;
        roll = inputActions.PlayerControllers.Roll;
    }

    private void OnEnable()
    {
        forward.Enable();
        side.Enable();
        roll.Enable();
        isOnTurn = true;
        canRoll = true;

    }

    private void OnDisable()
    {
        forward.Disable();
        side.Disable();
        roll.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnTurn)
        {
            if (!isOnEvent && isMoving)
            {
                if (indexToGo != 0)
                {
                    float variance_x = Random.Range(1.0f, 1.5f);
                    float variance_z = Random.Range(1.0f, 1.5f);

                    Vector3 variance = new Vector3(variance_x, 0f, variance_z);
                    transform.position = Vector3.MoveTowards(transform.position, TrackLoopManager.instance.mainTrackTransforms[currentIndex].position + variance, Time.deltaTime * speedMod);

                    if (Vector3.Distance(transform.position, TrackLoopManager.instance.mainTrackTransforms[currentIndex].position + variance) <= 0.1)
                    {
                        currentIndex++;
                        indexToGo -= 1;
                        if (currentIndex >= TrackLoopManager.instance.mainTrackTransforms.Count)
                        {
                            currentIndex = 0;
                        }
                    }

                }
                else
                {
                    isMoving = false;
                    TrackLoopManager.instance.TurnTransition();
                }

            }

            if (isOnEvent)
            {
                if (forward.ReadValue<float>() != 0)
                {
                    print("Go Forward");
                    isOnEvent = false;
                }
                else if (side.ReadValue<float>() != 0)
                {
                    print("Go Sideward");
                    currentIndex += gateIndex;
                    isOnEvent = false;
                }
            }

            if (canRoll)
            {
                if (roll.ReadValue<float>() != 0)
                {
                    canRoll = false;
                    indexToGo = Random.Range(1, 7) + Random.Range(1, 7);
                    print(indexToGo);
                    isMoving = true;
                }
            }

        }
    }
        

        
}
