using System.Collections;
using System.Collections.Generic;
using SlimUI.ModernMenu;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairChange_Start : MonoBehaviour
{
    public Image MainCrossHair;
    public Sprite[] CrossHairImages;
    public UIMenuManager uiMenuManager;

    private void Update()
    {
        MainCrossHair.sprite = CrossHairImages[PlayerPrefs.GetInt("CrossHairNum")]; 
    }

    public void ChangeBtn(int crosshairNum)
    {
        MainCrossHair.sprite = CrossHairImages[crosshairNum];
        PlayerPrefs.SetInt("CrossHairNum", crosshairNum);

        DisableAllLineCrossHairs();

        GameObject lineCrossHairToActivate = uiMenuManager.GetLineCrossHairObject(crosshairNum);

        if (lineCrossHairToActivate != null)
        {
            lineCrossHairToActivate.SetActive(true);
        }
    }

    private void DisableAllLineCrossHairs()
    {
        foreach (var lineCrossHair in uiMenuManager.lineCrossHairs)
        {
            if (lineCrossHair != null)
            {
                lineCrossHair.SetActive(false);
            }
        }
    }

}
