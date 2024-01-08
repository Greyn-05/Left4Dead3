using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSpawnerOnColliderCheck : MonoBehaviour
{
    public List<GameObject> spawners;
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject obj = this.transform.GetChild(i).gameObject;
            spawners.Add(obj);
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach (GameObject obj in spawners)
            {
                obj.SetActive(true);
            }
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject obj in spawners)
            {
                obj.SetActive(false);
            }
        }
    }
    */
}
