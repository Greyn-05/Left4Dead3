using System.Collections;
using System.Collections.Generic;
using SlimUI.ModernMenu;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairChange_Setting : MonoBehaviour
{
    public Image MainCrossHair;
    public Sprite[] CrossHairImages;
    public SettingMenu settingMenu;

    private void Start()
    {

    }

    public void ChangeBtn(int crosshairNum)
    {
        MainCrossHair.sprite = CrossHairImages[crosshairNum];
        PlayerPrefs.SetInt("CrossHairNum", crosshairNum);

        DisableAllLineCrossHairs();

        GameObject lineCrossHairToActivate = settingMenu.GetLineCrossHairObject(crosshairNum);

        if (lineCrossHairToActivate != null)
        {
            lineCrossHairToActivate.SetActive(true);
        }
    }

    private void DisableAllLineCrossHairs()
    {
        foreach (var lineCrossHair in settingMenu.lineCrossHairs)
        {
            if (lineCrossHair != null)
            {
                lineCrossHair.SetActive(false);
            }
        }
    }

}
