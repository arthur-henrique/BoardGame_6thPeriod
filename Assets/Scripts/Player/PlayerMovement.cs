using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //public static PlayerMovement instance;
    public InputAction forward, side, roll;

    public int currentIndex = 0;
    public int nextIndex = 0;
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

    public int currentLap;

    Animator animator;

    public TextMeshPro diceDisplay;
    

    private void Awake()
    {
        //instance = this;
        PlayerControl inputActions = new();
        if (gameObject.CompareTag("Player 1"))
        {
            forward = inputActions.PlayerControllers.ForwardP1;
            side = inputActions.PlayerControllers.SidewardP1;
            
        }

        if (gameObject.CompareTag("Player 2"))
        {
            forward = inputActions.PlayerControllers.ForwardP2;
            side = inputActions.PlayerControllers.SidewardP2;

        }
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
    private void Start()
    {
        startingPos = transform.position;
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
                    animator.SetBool("IsJumping", false);
                    isMoving = false;
                    //Loops back
                    TrackLoopManager.instance.mainTrackTransforms[currentIndex].GetComponentInChildren<SpaceBehaviour>().DoTheThing();
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
                    currentIndex = gateIndex-1;
                    isOnEvent = false;
                }
            }
        }
    }        

    IEnumerator MoveOneSpace()
    {
        SetNextIndex();

        jumpDistance = Vector3.Distance(TrackLoopManager.instance.mainTrackTransforms[currentIndex].position, TrackLoopManager.instance.mainTrackTransforms[nextIndex].position);

        //if player has arrived at the next space, set the next space to go
        if (Vector3.Distance(transform.position, TrackLoopManager.instance.mainTrackTransforms[nextIndex].position) <= 0.1)
        {
            animator.Play("Armature|JumpStart",-1,0f);
            startingPos = transform.position;
            indexToGo--;

            currentIndex = nextIndex;
        }

        diceDisplay.text = indexToGo.ToString();
        if (indexToGo == 0) { diceDisplay.text = ""; }

        //Moves pawn up and down
        currentPos = Mathf.PI * (Vector3.Distance(transform.position, startingPos) / jumpDistance - 0.5f); //-PI/2 to PI/2
        gameObject.transform.GetChild(0).transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f + 5 * Mathf.Cos(currentPos), transform.position.z);

        //Moves pawn towards next space
        transform.position = Vector3.MoveTowards(transform.position, TrackLoopManager.instance.mainTrackTransforms[nextIndex].position, Time.deltaTime * speedMod);
        var targetRotation = Quaternion.LookRotation(-TrackLoopManager.instance.mainTrackTransforms[nextIndex].position, Vector3.up);
        transform.GetChild(0).rotation = targetRotation;

        animator.SetBool("IsJumping", true);

        yield return null;
    }
    public void SetNextIndex()
    {


        nextIndex = currentIndex + 1;
        if (nextIndex >= TrackLoopManager.instance.mainTrackTransforms.Count) { nextIndex = 0; }

        if (currentIndex == 30)
        {
            nextIndex = 13;
        }

        else if (currentIndex == 38)
        {
            nextIndex = 22;
        }

        else if (currentIndex == 26)
        {
            nextIndex = 0;
        }
    }
}
