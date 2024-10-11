using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class FinishGame : MonoBehaviour
{
    [SerializeField]
    private Image mainImage;

    [SerializeField] 
    private TextMeshProUGUI winnerText;

    [SerializeField]
    private RawImage winnerImage;
    [SerializeField]
    private Material player1Mat;
    [SerializeField]
    private Material player2Mat;

    private Vector3 velocityMainImage = Vector3.zero;

    public float springStrength = 850 * 32;
    public float damping = 250;

    public InputAction roll;
    
    private void Awake()
    {
        PlayerControl inputActions = new PlayerControl();
        roll = inputActions.PlayerControllers.Roll;
    }

    private void OnEnable()
    {
        roll.Enable();
    }

    private void OnDisable()
    {
        roll.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainImage.rectTransform.localScale = new Vector3(0, 1, 1);
        GameObject gm = GameObject.Find("GameManager");
        winnerText.text = string.Format(gm.GetComponent<GameManager>().playerNames[gm.GetComponent<GameManager>().winner]+" wins!");
        if (gm.GetComponent<GameManager>().winner == 0)
            winnerImage.texture = player1Mat.mainTexture;
        else winnerImage.material.mainTexture = player2Mat.mainTexture;

    }

    // Update is called once per frame
    void Update()
    {
        ImageSpring(mainImage, new Vector3(1, 1, 1), springStrength, damping, velocityMainImage);
        if (roll.WasPressedThisFrame())
        {
            Application.Quit();

        }
    }

    private void ImageSpring(Image img, Vector3 targetPosition, float springStrength, float damping, Vector3 velocity)
    {
        Vector3 displacement = targetPosition - img.rectTransform.localScale;
        Vector3 springForce = displacement * springStrength;
        velocity += springForce * Time.unscaledDeltaTime;
        velocity *= Mathf.Exp(-damping * Time.unscaledDeltaTime); // Damping effect
        img.rectTransform.localScale += velocity * Time.unscaledDeltaTime;
    }
}
