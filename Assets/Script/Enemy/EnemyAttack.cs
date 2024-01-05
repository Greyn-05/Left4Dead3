using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool isPlayer;
    void Start()
    {
        isPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Close");
            isPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = false;
        }
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(2f);

        if(isPlayer == true)
        {
            //플레이어 데미지 주기
            Debug.Log("Player Hit!!!");
        }

    }
   
}
