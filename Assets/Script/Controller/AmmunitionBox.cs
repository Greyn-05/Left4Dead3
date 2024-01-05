using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBox: MonoBehaviour, IInteractable
{
    public string bulletBox = "총알 채우기";
    
    public string GetInteractPrompt()
    {
        Debug.Log("BulletData");
        Debug.Log(bulletBox);
        return string.Format(" {0}", bulletBox);
    }

    public void OnInteract()
    {
        PlayerManager.Instance.m_mainWeapon.maxBulletInTheGun = PlayerManager.Instance.m_mainWeapon.maxBulletAmount; //탄약 보충
    }
}
