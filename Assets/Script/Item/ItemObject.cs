using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public GunData item;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.gunName);
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}