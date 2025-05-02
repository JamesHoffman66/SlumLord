using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject[] PowerUps; 
    public int enemyCount;
    public int waveNumber = 0;
    public int waves;
    public TextMeshProUGUI WavesText;
    public Transform player;
    public Vector3 playerPosition;
    //public bool isGameActive;
    public PlayerMovement playerScript;
    public GameObject currentItem;
   


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = player.position;
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        playerScript = player.gameObject.GetComponent<PlayerMovement>();
        SpawnEnemyWave(waveNumber);
        WaveNumber(0);
    }

    // Update is called once per frame
    void Update()
    {
        // enemy waves
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 )
        {
            WaveNumber(0);
            waveNumber++;
            SpawnEnemyWave(waveNumber); 
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        // Spawns enemy in random location
        float spawnPosX = Random.Range(18,3);
        float spawnPosY = Random.Range(8, 17);
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, 0.3f);
        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // only spawns enemies while isGameActive is set to true. isGameActive becomes false when there is a game over
        if (playerScript.isGameActive)
        {
            Destroy(currentItem);
            int index = Random.Range(0, PowerUps.Length);
            currentItem = Instantiate(PowerUps[index], GenerateSpawnPosition(), PowerUps[index].transform.rotation);
            player.position = playerPosition;
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
                waves++;
            }
        }
      
    }

    private void WaveNumber(int wavesToChange)
    {
        waves += wavesToChange;
        
        WavesText.text = "Waves: " + waveNumber;
    }

   
}
