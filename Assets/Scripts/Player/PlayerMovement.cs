using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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

    private Vector3 startingPos;
    private float jumpDistance;
    private float currentPos;

    Animator animator;

    public TextMeshPro diceDisplay;

    private void Awake()
    {
        instance = this;
        PlayerControl inputActions = new();
        forward = inputActions.PlayerControllers.Forward;
        side = inputActions.PlayerControllers.Sideward;
        roll = inputActions.PlayerControllers.Roll;
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
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
            //Rolls dice
            if (canRoll && roll.WasPressedThisFrame())
            {
                canRoll = false;
                indexToGo = Random.Range(1, 7) + Random.Range(1, 7);
                diceDisplay.text = indexToGo.ToString();
                print(indexToGo);
                isMoving = true;
            }

            if (!isOnEvent && isMoving)
            {
                //If player hasn't reached final space, move one space
                if (indexToGo != 0)
                {
                    StartCoroutine(MoveOneSpace());
                }

                //When the player reaches their final space
                else
                {
                    isMoving = false;
                    //Loops back
                    if (currentIndex >= TrackLoopManager.instance.mainTrackTransforms.Count) { currentIndex = 0; }
                    if (currentIndex == 0) { currentIndex = TrackLoopManager.instance.mainTrackTransforms.Count + 1; }
                    TrackLoopManager.instance.mainTrackTransforms[currentIndex-1].GetComponentInChildren<SpaceBehaviour>().DoTheThing();
                    //TrackLoopManager.instance.TurnTransition();
                }

            }

            //when the player reaches a fork
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
        }
    }        

    IEnumerator MoveOneSpace()
    {
        //private float variance_x = Random.Range(0, 1.0f);
        //private float variance_z = Random.Range(0, 1.0f);
        //Vector3 variance = new(variance_x, 0f, variance_z);

        //if player has arrived at the next space, set the next space to go
        if (Vector3.Distance(transform.position, TrackLoopManager.instance.mainTrackTransforms[currentIndex].position /*+ variance*/) <= 0.1)
        {
            startingPos = transform.position;
            currentIndex++;
            jumpDistance = Vector3.Distance(transform.position, TrackLoopManager.instance.mainTrackTransforms[currentIndex].position /*+ variance*/);
            indexToGo--;
            diceDisplay.text = indexToGo.ToString();
            if (indexToGo == 0) { diceDisplay.text = ""; }

            //Loops back
            if (currentIndex >= TrackLoopManager.instance.mainTrackTransforms.Count) { currentIndex = 0; }
        }

        currentPos = Mathf.PI * (Vector3.Distance(transform.position,startingPos)/ jumpDistance - 0.5f); //-PI/2 to PI/2
        
        //Moves pawn up and down
        gameObject.transform.GetChild(0).transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f + 5 * Mathf.Cos(currentPos), transform.position.z);

        //Moves pawn towards next space
        transform.position = Vector3.MoveTowards(transform.position, TrackLoopManager.instance.mainTrackTransforms[currentIndex].position /*+ variance*/, Time.deltaTime * speedMod);

        animator.SetBool("IsJumping", true);

        if (currentPos > 0.6)
        {
            animator.SetBool("IsJumping", false);
        }

        yield return null;
    }
}
