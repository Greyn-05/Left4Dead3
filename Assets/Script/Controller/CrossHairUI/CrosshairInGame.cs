using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairInGame : MonoBehaviour
{
    private Image CrossHairImage;
    private Color CrosshairColor;
    public Sprite[] CrossHairImages;
    

    private void Awake()
    {
        CrossHairImage = GetComponent<Image>();
        CrosshairColor = CrossHairImage.color;
    }
    private void Update()
    {        
        CrossHairImage.sprite = CrossHairImages[PlayerPrefs.GetInt("CrossHairNum")];
        CrosshairColor.r = PlayerPrefs.GetFloat("R_value");
        CrosshairColor.g = PlayerPrefs.GetFloat("G_value");
        CrosshairColor.b = PlayerPrefs.GetFloat("B_value");
    }
}
