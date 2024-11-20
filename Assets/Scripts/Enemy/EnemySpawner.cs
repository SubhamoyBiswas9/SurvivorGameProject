using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawner;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int wave;
        public List<EnemyGroup> enemyGroups; // list of groups of enemies to spawn in this wave
        public int waveQuota; // total number of enemies to spawn in this wave
        public float spawnInterval; // interval at which to spawn enemies
        public int spawnCount; // the number of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // number of enemies to spawn in this wave
        public int spawnCount; // number of enemies of this type already spawned in this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // List of all the waves in the game

    public int currentWaveCount; // the index of the current wave

    [Header("Spawner Attributes")]
    public float waveInterval; // interval between each wave

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        CalculateWaveQuota();
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) // check if the wave has ended and the next wave should start
        {
            StartCoroutine(BeginNextWave());
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        // moving on to the next wave if there are more waves to start after the current wave
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(waves[currentWaveCount].spawnInterval);
            SpawnEnemies();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.Log(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        Wave currentWave = waves[currentWaveCount];

        // check if the total number of enemies in this wave have been spawned
        if (currentWave.spawnCount < currentWave.waveQuota)
        {
            // Spawn each type of enemies until the quota is filled
            foreach(var enemyGroup in currentWave.enemyGroups)
            {
                // check if the total number of enemies of this type have been spawned
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    Vector3 spawnPosition = 
                        new Vector3(player.transform.position.x+Random.Range(-10f,10f),
                        player.transform.position.y,
                        player.transform.position.z+Random.Range(-10f,10f));

                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    currentWave.spawnCount++;
                }
            }
        }
    }
}
