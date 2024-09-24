using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomization : MonoBehaviour
{
    [SerializeField]
    private Material faceMaterial;

    [SerializeField]
    private Material bodyMaterial;

    [SerializeField]
    private FlexibleColorPicker colorPicker;

    [SerializeField]
    private Texture defaultFace;

    [SerializeField]
    private RawImage facePreview;

    // Start is called before the first frame update
    void Start()
    {
        faceMaterial.mainTexture = defaultFace;
        facePreview.texture = defaultFace;
    }

    // Update is called once per frame
    void Update()
    {
        bodyMaterial.color = colorPicker.GetColor();
        transform.eulerAngles += new Vector3(0, 0.2f * Mathf.Sin(Time.fixedTime), 0);
    }

    public void ChangeFace(Texture faceTex)
    {
        faceMaterial.mainTexture = faceTex;
    }
}
