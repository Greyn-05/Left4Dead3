using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Image gunImage;
    public TMP_Text bulletInfoText;

    public void UpdateWeaponUI(GunData gunData)
    {
        gunImage.sprite = gunData.icon;
        bulletInfoText.text = $"{gunData.nowBulletInTheGun} / {gunData.maxBulletAmount}"; // 장전된 총알 / 전체 총알
    }
}
