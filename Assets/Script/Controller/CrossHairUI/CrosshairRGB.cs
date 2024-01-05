using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairRGB : MonoBehaviour
{
    [Header("Slider")]
    public Slider Red;
    public Slider Green;
    public Slider Blue;

    public Image CrossHair;

    private Color crossHairColor;
    //public Color CrossHairColor;
    private void Awake()
    {
        crossHairColor = CrossHair.color;

        Red.value = PlayerPrefs.GetFloat("R_value");
        Green.value = PlayerPrefs.GetFloat("G_value");
        Blue.value = PlayerPrefs.GetFloat("B_value");
    }

    private void Update()
    {
        crossHairColor.r = Red.value;
        crossHairColor.g = Green.value;
        crossHairColor.b = Blue.value;
        CrossHair.color = crossHairColor;

        PlayerPrefs.SetFloat("R_value", crossHairColor.r);
        PlayerPrefs.SetFloat("G_value", crossHairColor.g);
        PlayerPrefs.SetFloat("B_value", crossHairColor.b);
    }
}
