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
    }

    private void Update()
    {
        crossHairColor.r = Red.value;
        crossHairColor.g = Green.value;
        crossHairColor.b = Blue.value;
        CrossHair.color = crossHairColor;
    }
    //public void ChangeR()
    //{
    //}
    //public void ChangeG()
    //{
    //    CrossHair.color = crossHairColor;
    //}
    //public void ChangeB()
    //{
    //    CrossHair.color = crossHairColor;
    //}

}
