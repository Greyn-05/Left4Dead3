using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private int enemyCountMin;

    [SerializeField]
    private int enemyCountMax;

    [SerializeField]
    private float enemySpawnYposition;

    [SerializeField]
    private int enemySpawnXrange;

    [SerializeField]
    private int enemySpawnZrange;

    public GameObject Player;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> enemyList = new List<GameObject> { };
    private int enemyCount;
    void Start()
    {
        enemyCount = Random.Range(enemyCountMin, enemyCountMax + 1);
        spawnEnemy();
       
    }

    private void spawnEnemy()
    {
        for (int cnt = 0; cnt < enemyCount; cnt++)
        {
            Vector3 initPosition = new Vector3(Random.Range(transform.position.x - enemySpawnXrange, gameObject.transform.position.x + enemySpawnXrange), enemySpawnYposition, Random.Range(transform.position.z - enemySpawnZrange, gameObject.transform.position.z + enemySpawnZrange));
            Vector3 initRotate = new Vector3(0, 0, 0);
            GameObject newPrefabInstance = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], initPosition, Quaternion.Euler(initRotate), transform);
            EnemyBehavior behavior = newPrefabInstance.GetComponent<EnemyBehavior>();
            
            behavior.SetTargetPlayer(Player);
            
            enemyList.Add(newPrefabInstance);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(enemySpawnXrange*2, enemySpawnYposition, enemySpawnZrange*2));

    }


}
