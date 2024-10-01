using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager_Fruit_Drop : MonoBehaviour
{
    public List<GameObject> objects = new();
    public GameObject spike;
    [SerializeField] private TextMeshProUGUI UI;
    [SerializeField] private float totalTime;
    private float time;
    public float spawnRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObject());
        StartCoroutine(Countdown());
        time = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnRate = 0.2f + 0.8f * (time /totalTime);
    }

    IEnumerator SpawnObject()
    {
        Instantiate(objects[Random.Range(0, 2)], new Vector3(Random.Range(-5, 6), 20, 0), Quaternion.identity);
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnObject());
    }
    IEnumerator Countdown()
    {
        UI.text = string.Format("{0}", time);
        time--;
        yield return new WaitForSeconds(1);
        if(time >= 0)
        {
            StartCoroutine(Countdown());
        }
        else 
            Time.timeScale = 0;
    }
}
