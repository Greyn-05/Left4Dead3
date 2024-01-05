using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairChange : MonoBehaviour
{
    public Image MainCrossHair;
    public Sprite[] CrossHairImages;

    public void ChangeBtn(int crosshairNum)
    {
        MainCrossHair.sprite = CrossHairImages[crosshairNum];
        PlayerPrefs.SetInt("CrossHairNum", crosshairNum);
    }

}
