using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField]
    private Material faceMaterial;

    [SerializeField]
    private Material bodyMaterial;

    [SerializeField]
    private RawImage avatarDisplay;

    [SerializeField]
    private TextMeshProUGUI nameDisplay;

    [SerializeField]
    private int playerIndex;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance)
        {
            nameDisplay.text = GameManager.Instance.playerNames[playerIndex];
        }
        avatarDisplay.texture = faceMaterial.mainTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
