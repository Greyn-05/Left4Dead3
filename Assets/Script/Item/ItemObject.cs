using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public GunData Gun;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", Gun.gunName);
    }

    public void OnInteract()
    {
        //인벤토리에 추가
        Destroy(gameObject);
    }
}