using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Image gunImage;
    public TMP_Text bulletInfoText;

    private bool m_toggle = true;

    public void UpdateWeaponUI(GunData gunData)
    {
        gunImage.sprite = gunData.icon;
        bulletInfoText.text = $"{gunData.nowBulletInTheGun} / {gunData.maxBulletAmount}"; // 장전된 총알 / 전체 총알
    }

    public void ToggleUi()
    {
        m_toggle = !m_toggle;

        if (!m_toggle)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

    }
}
