using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public CinemachineVirtualCamera[] cinemachineVirtualCameras;
    public CinemachineTransposer[] transposer;

    public Vector3 offsetToGo;
    public Vector3 currentOffset;
    public float lerpDuration = 2f;

    private Coroutine lerpCoroutine;

    private void Start()
    {
        for (int i = 0; i < cinemachineVirtualCameras.Length; i++)
        {
            transposer[i] = cinemachineVirtualCameras[i].GetCinemachineComponent<CinemachineTransposer>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Collided");
        if (other.tag == "Player")
        {
            Debug.LogWarning("CollidedIf");
            StartOffsetLerp(offsetToGo, GameManager.Instance.currentPlayerIndexTurn);
        }
    }

    public void StartOffsetLerp(Vector3 newOffset, int currentTurn)
    {
        // If a lerp coroutine is already running, stop it first
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
        }

        // Start a new coroutine to smoothly lerp the offset
        lerpCoroutine = StartCoroutine(LerpOffset(newOffset, currentTurn));
    }

    private IEnumerator LerpOffset(Vector3 targetOffset, int whichCamera)
    {
        float elapsedTime = 0f;
        Vector3 initialOffset = transposer[whichCamera].m_FollowOffset;

        while (elapsedTime < lerpDuration)
        {
            currentOffset = Vector3.Lerp(initialOffset, targetOffset, elapsedTime / lerpDuration);
            transposer[whichCamera].m_FollowOffset = currentOffset;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transposer[whichCamera].m_FollowOffset = targetOffset;
    }
}