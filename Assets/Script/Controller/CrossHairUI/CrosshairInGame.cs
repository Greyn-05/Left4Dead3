using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairInGame : MonoBehaviour
{
    private Image CrossHairImage;
    [SerializeField] private Color CrosshairColor;
    public Sprite[] CrossHairImages;
    
    float red;
    float green;
    float blue;
    

    private void Awake()
    {
        CrossHairImage = GetComponent<Image>();
        //CrosshairColor = GetComponent<Color>();
    }
    private void Start()
    {
        CrosshairColor = CrossHairImage.GetComponent<Color>();
    }
    private void Update()
    {
        CrossHairImage.sprite = CrossHairImages[PlayerPrefs.GetInt("CrossHairNum")];
        red = PlayerPrefs.GetFloat("R_value");
        green = PlayerPrefs.GetFloat("G_value");
        blue = PlayerPrefs.GetFloat("B_value");

        CrosshairColor.r = red;
        CrosshairColor.g = green;
        CrosshairColor.b = blue;
        CrosshairColor.a = 1;

        CrossHairImage.color = CrosshairColor;
    }
}
