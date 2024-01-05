
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpawnManager : MonoBehaviour
{
    public static HealSpawnManager instance; //Singleton

    public GameObject HealpackPrefab;

    public Transform[] SpawnPoints;

    [SerializeField] 
    private int maxHealSpawnCount = 10;
    public int currentHealSpawnCount;

    [SerializeField] private float maxSpawnDelay = 5f;
    private float spawnDelay;

    private void Awake()
    {        
        instance = this;
        spawnDelay = maxSpawnDelay;
        currentHealSpawnCount = 0;
    }
    //private IEnumerator SpawnHealPack()
    private void Update()
    {
        spawnDelay -= 1 * Time.deltaTime;

        Debug.Log(spawnDelay);
        if (spawnDelay <= 0)
        {
            if (currentHealSpawnCount <= maxHealSpawnCount)
            {
                Debug.Log("ddd");
                HealpackSpawn();
                currentHealSpawnCount++;
                spawnDelay = maxSpawnDelay;
                Debug.Log(currentHealSpawnCount);
            }
        }
    }

    private void HealpackSpawn()
    {
        int randomSpawnPoint = Random.Range(0, SpawnPoints.Length);
        Transform SpawnPosition = SpawnPoints[randomSpawnPoint];
        Instantiate(HealpackPrefab, SpawnPosition.position, SpawnPosition.rotation, SpawnPosition);
    }
}
