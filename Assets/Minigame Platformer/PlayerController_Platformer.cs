using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Platformer : MonoBehaviour
{
    InputAction movement, jump;
    Rigidbody rb;
    Animator animator;

    [SerializeField] float jumpStrength;
    [SerializeField] LayerMask platform;
    bool isGrounded;
    bool canTakeDamage = true;
    int playerNumber;
    
    [SerializeField] Transform mainCamera;
    [SerializeField] private GameObject finishMinigameCanvas;
    private void Awake()
    {
        PlayerControl inputsGame = new();
        if (gameObject.CompareTag("Player 1"))
        {
            movement = inputsGame.Controls_Fruit_Drop.WalkP1;
            jump = inputsGame.Controls_Fruit_Drop.JumpP1;
            playerNumber = 1;
        }
        else if (gameObject.CompareTag("Player 2"))
        {
            movement = inputsGame.Controls_Fruit_Drop.WalkP2;
            jump = inputsGame.Controls_Fruit_Drop.JumpP2;
            playerNumber = 2;
        }
    }

    private void OnEnable()
    {
        movement.Enable();
        jump.Enable();

    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement.ReadValue<float>() * 10 * Time.deltaTime, 0, 0);
        if (movement.ReadValue<float>() < 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        if (jump.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(100 * jumpStrength * Vector3.up, ForceMode.Impulse);
            animator.SetBool("IsJumping", true);
        }

        //Fall recovery
        if(transform.position.y < -8)
        {
            StartCoroutine(Respawn(transform.position.x));
        }

        if(Mathf.Abs(transform.position.x-mainCamera.position.x) > 25f)
        {
            StartCoroutine(Respawn(mainCamera.position.x - 15));
        }
    }
    IEnumerator Respawn(float posX)
    {
        //teleports to the top of the screen and locks to the camera
        transform.position = new(posX, 15, transform.position.z);
        transform.parent = mainCamera;
        
        //Disables gravity, movement and controls
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        movement.Disable();
        jump.Disable();

        //Detaches from camera and enables controls and gravity
        yield return new WaitForSeconds(2);
        transform.parent = null;
        rb.useGravity = true;
        movement.Enable();
        jump.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Spike"))
        {
            StartCoroutine(Respawn(transform.position.x));
        }
    }

    //Ground Detection
    #region
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Platform"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
    #endregion 
}
