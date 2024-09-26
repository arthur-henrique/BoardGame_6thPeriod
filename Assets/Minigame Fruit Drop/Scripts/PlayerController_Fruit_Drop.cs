using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Fruit_Drop : MonoBehaviour
{
    InputAction movement, jump;
    int score = 0;
    Rigidbody rb;
    Animator animator;
    [SerializeField] float jumpStrength;
    bool canTakeDamage = true;
    TextMeshProUGUI UI;
    int playerNumber;

    private void Awake()
    {
        PlayerControl inputsGame = new PlayerControl();
        if (gameObject.tag == "Player 1")
        {
            movement = inputsGame.Controls_Fruit_Drop.WalkP1;
            jump = inputsGame.Controls_Fruit_Drop.JumpP1;
            UI = GameObject.Find("ScoreNumber1").GetComponent<TextMeshProUGUI>();
            playerNumber = 1;
        }
        else if (gameObject.tag == "Player 2")
        {
            movement = inputsGame.Controls_Fruit_Drop.WalkP2;
            jump = inputsGame.Controls_Fruit_Drop.JumpP2;
            UI = GameObject.Find("ScoreNumber2").GetComponent<TextMeshProUGUI>();
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
        transform.Translate(0, 0, movement.ReadValue<float>() * 5 * Time.deltaTime);
        if (jump.WasPressedThisFrame() && rb.velocity.y==0.0f)
        {
            rb.AddForce(Vector3.up*100*jumpStrength,ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fruit" && canTakeDamage)
        {
            score++;
            Destroy(other.gameObject);
            UI.text = string.Format("Player {0}: {1}",playerNumber, score);
        }
        if (other.gameObject.tag == "Spike" && canTakeDamage)
        {
            movement.Disable();
            jump.Disable();
            canTakeDamage = false;
            Destroy(other.gameObject);
            animator.SetBool("Stun", true);
            StartCoroutine(TimerWait());
        }
    }

    IEnumerator TimerWait()
    {
        yield return new WaitForSeconds(2.0f);
        movement.Enable();
        jump.Enable();
        canTakeDamage = true;
        animator.SetBool("Stun", false);
    }

}
