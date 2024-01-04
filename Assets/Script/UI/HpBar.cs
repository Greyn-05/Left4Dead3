using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpSlider;

    public void UpdateHpBar(float health)
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, health, Time.deltaTime * 10);
    }
}
