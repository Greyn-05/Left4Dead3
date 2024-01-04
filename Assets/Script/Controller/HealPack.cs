using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //플레이어 힐
            PlayerManager.Instance.AddHealthPoint(50f);

            HealSpawnManager.instance.currentHealSpawnCount--;
            Destroy(gameObject);
        }
    }
}
