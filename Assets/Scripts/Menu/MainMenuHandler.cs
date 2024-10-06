using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuHandler : MonoBehaviour
{
    // -- External objects

    [SerializeField]
    private GameObject[] playerObjs;

    [SerializeField]
    private GameObject[] groupSections;

    // -- Variables

    public static MainMenuHandler instance;
    public InputAction forward, side, roll;

    private int menuIndex = 0;

    private Vector3 velocityPlayer1 = Vector3.zero;
    private Vector3 velocityPlayer2 = Vector3.zero;

    private Vector3[] groupSectionsVelocities = new Vector3[10];

    private Coroutine groupSectionsCoroutine;

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
    }

    private void OnDisable()
    {
        forward.Disable();
        side.Disable();
        roll.Disable();
    }

    void Start()
    {
        for (int i = 0; i < groupSections.Length + 1; i++)
        {
            groupSectionsVelocities[i] = Vector3.zero;
        }

        MoveMenu(menuIndex);
    }

    void Update()
    {
        switch (menuIndex)
        {
            case 0:

                if (forward.WasPressedThisFrame() || side.WasPressedThisFrame() || roll.WasPressedThisFrame())
                {
                    MoveMenu(menuIndex + 1);
                }//

                break;
        }

        PlayerSpring(playerObjs[0], (menuIndex == 2 ? new Vector3(3, 0, -2.5f) : new Vector3(3, -8, -2.5f)), 350, 5, velocityPlayer1);
        PlayerSpring(playerObjs[1], (menuIndex == 3 ? new Vector3(3, 0, -2.5f) : new Vector3(3, -8, -2.5f)), 350, 5, velocityPlayer1);
    }

    private void MoveMenu(int index, float speed = 0.5f)
    {
        menuIndex = index;
        try { StopCoroutine(groupSectionsCoroutine); } catch { }
        groupSectionsCoroutine = StartCoroutine(MoveGroupSectionsCoroutine(speed));
    }

    public void ButtonMoveMenu(int index)
    {
        MoveMenu(index);
    }

    private void PlayerSpring(GameObject player, Vector3 targetPosition, float springStrength, float damping, Vector3 velocity)
    {
        Vector3 displacement = targetPosition - player.transform.position;
        Vector3 springForce = displacement * springStrength;
        velocity += springForce * Time.deltaTime;
        velocity *= Mathf.Exp(-damping * Time.deltaTime); // Damping effect
        player.transform.position += velocity * Time.deltaTime;
    }

    IEnumerator MoveGroupSectionsCoroutine(float transitionDuration)
    {
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[groupSections.Length];
        float[] startAlphas = new float[groupSections.Length];

        for (int i = 0; i < groupSections.Length; i++)
        {
            startPositions[i] = groupSections[i].GetComponent<RectTransform>().anchoredPosition;
            startAlphas[i] = groupSections[i].GetComponent<CanvasGroup>().alpha;
        }

        while (elapsedTime < transitionDuration)
        {
            // Calculate the time factor
            float t = elapsedTime / transitionDuration;
            float easedT = EaseOutQuad(t); // Use easing for decelerating motion

            for (int i = 0; i < groupSections.Length; i++)
            {
                // Interpolate positions using Lerp with eased time factor
                Vector3 targetPosition = new Vector3(800 * (i - menuIndex), 0, 0);
                groupSections[i].GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPositions[i], targetPosition, easedT);

                // Interpolate alphas using Lerp with eased time factor
                float targetAlpha = (menuIndex == i ? 1f : 0f);
                CanvasGroup canvasGroup = groupSections[i].GetComponent<CanvasGroup>();
                canvasGroup.alpha = Mathf.Lerp(startAlphas[i], targetAlpha, easedT);
            }

            // Increment time
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure final positions and alphas are set
        for (int i = 0; i < groupSections.Length; i++)
        {
            groupSections[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(800 * (i - menuIndex), 0, 0);
            groupSections[i].GetComponent<CanvasGroup>().alpha = (menuIndex == i ? 1f : 0f);
        }

    }
    float EaseOutQuad(float t)
    {
        return t * (2f - t); // Simple quadratic ease-out function
    }

    public void ToggleFullscreen()
    {
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;//
        }
        else 
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
