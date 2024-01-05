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
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnEnemy()
    {
        int enemyCount = Random.Range(enemyCountMin, enemyCountMax+1);

        for (int cnt = 0; cnt < enemyCount; cnt++)
        {
            Vector3 initPosition = new Vector3(Random.Range(transform.position.x - 20, gameObject.transform.position.x + 20), enemySpawnYposition, Random.Range(transform.position.z - 20, gameObject.transform.position.z + 20));
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
        Gizmos.DrawWireCube(transform.position, new Vector3(enemySpawnXrange, enemySpawnYposition, enemySpawnZrange));

    }


}
