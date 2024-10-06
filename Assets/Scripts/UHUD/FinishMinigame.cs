using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishMinigame : MonoBehaviour
{
    [SerializeField]
    private Image mainImage;

    private Vector3 velocityMainImage = Vector3.zero;

    public float springStrength = 850*32;
    public float damping = 250;

    // Start is called before the first frame update
    void Start()
    {
        mainImage.rectTransform.localScale = new Vector3(0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        ImageSpring(mainImage, new Vector3(1, 1, 1), springStrength, damping, velocityMainImage);
    }

    private void ImageSpring(Image img, Vector3 targetPosition, float springStrength, float damping, Vector3 velocity)
    {
        Vector3 displacement = targetPosition - img.rectTransform.localScale;
        Vector3 springForce = displacement * springStrength;
        velocity += springForce * Time.unscaledDeltaTime;
        velocity *= Mathf.Exp(-damping * Time.unscaledDeltaTime); // Damping effect
        img.rectTransform.localScale += velocity * Time.unscaledDeltaTime;
    }
    public void GoToIslandActivator()
    {
        StartCoroutine(GoToIsland());
    }
    private IEnumerator GoToIsland()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        GameManager.Instance.ChangeLevels(1);
    }
}
