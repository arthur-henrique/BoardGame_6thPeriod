using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController_Fruit_Drop : MonoBehaviour
{
    InputAction movement, jump;
    public int score = 0;
    Rigidbody rb;
    Animator animator;
    [SerializeField] float jumpStrength;
    bool canTakeDamage = true;
    TextMeshProUGUI UI;
    [SerializeField] TextMeshProUGUI nameDisplay;
    [SerializeField] RawImage avatarDisplay;
    int playerNumber;

    private void Awake()
    {
        PlayerControl inputsGame = new();
        if (gameObject.CompareTag("Player 1"))
        {
            movement = inputsGame.Controls_Fruit_Drop.WalkP1;
            jump = inputsGame.Controls_Fruit_Drop.JumpP1;
            UI = GameObject.Find("ScoreNumber1").GetComponent<TextMeshProUGUI>();
            playerNumber = 1;
        }
        else if (gameObject.CompareTag("Player 2"))
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
        animator.SetBool("Fruit Game", true);
        avatarDisplay.texture = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[1].mainTexture;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement.ReadValue<float>() * 5 * Time.deltaTime, 0, 0);
        if(movement.ReadValue<float>() < 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        if (jump.WasPressedThisFrame() && rb.velocity.y==0.0f)
        {
            rb.AddForce(100 * jumpStrength * Vector3.up,ForceMode.Impulse);
            
        }
        if(movement.WasPressedThisFrame())
        {
            animator.SetBool("IsWalking", true);
        }
        if (movement.WasReleasedThisFrame())
        {
            animator.SetBool("IsWalking", false);
        }

        //UI.text = (GameObject.Find("GameManager").GetComponent<GameManager>().playersScore[playerNumber - 1] + score).ToString();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playersScore[playerNumber-1]++;
            Destroy(other.gameObject);
            //UI.text = string.Format("Player {0}: {1}",playerNumber, score);
        }
        if (other.gameObject.CompareTag("Spike") && canTakeDamage)
        {
            movement.Disable();
            jump.Disable();
            canTakeDamage = false;
            Destroy(other.gameObject);
            animator.SetBool("IsHit", true);
            StartCoroutine(TimerWait());
        }
    }

    IEnumerator TimerWait()
    {
        yield return new WaitForSeconds(2.0f);
        movement.Enable();
        jump.Enable();
        animator.SetBool("IsHit", false);
        yield return new WaitForSeconds(1.0f);
        canTakeDamage = true;
    }

}
