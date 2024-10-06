using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StartMinigame : MonoBehaviour
{
    [SerializeField]
    private Image mainImage;

    [SerializeField]
    private TextMeshProUGUI minigameNameText, minigameDescText;

    private Vector3 velocityMainImage = Vector3.zero;

    public InputAction forward, side, roll;

    [TextArea]
    public string minigameName, minigameDesc;

    public float springStrength = 850 * 32;
    public float damping = 250;


    private void Awake()
    {
        PlayerControl inputActions = new PlayerControl();
        forward = inputActions.PlayerControllers.Forward;
        side = inputActions.PlayerControllers.Sideward;
        roll = inputActions.PlayerControllers.Roll;
        minigameNameText.text = minigameName;
        minigameDescText.text = minigameDesc;
    }

    private void OnEnable()
    {
        forward.Enable();
        side.Enable();
        roll.Enable();
    }

    private void OnDisable()
    {
        forward.Disable();
        side.Disable();
        roll.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainImage.rectTransform.localScale = new Vector3(0, 1, 1);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ImageSpring(mainImage, new Vector3(1, 1, 1), springStrength, damping, velocityMainImage);
        if (forward.WasPressedThisFrame() || side.WasPressedThisFrame() || roll.WasPressedThisFrame())
        {
            Time.timeScale = 1;
            Destroy(this.gameObject);
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

    private IEnumerator GoToIsland()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        GameManager.Instance.ChangeLevels(1);
    }
}
