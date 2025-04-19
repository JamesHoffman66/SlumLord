using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int enemyCount;
    public int waveNumber = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // enemy waves
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 )
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber); 
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        // Spawns enemy in random location
        float spawnPosX = Random.Range(18,3);
        float spawnPosY = Random.Range(8, 17);
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, 0.5f);
        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(),
                enemyPrefab.transform.rotation);
        }
    }
}
