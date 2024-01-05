using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBox: MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //플레이어 탄약 보충
            Destroy(gameObject);
        }
    }
}
