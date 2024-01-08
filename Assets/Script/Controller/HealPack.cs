using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{ 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");

        if (other.gameObject.tag == "Player") 
        {
            //플레이어 힐
            PlayerManager.Instance.AddHealthPoint(50f);

            //HealSpawnManager.instance.currentHealSpawnCount--;
            Destroy(gameObject);
        }
    }
}
