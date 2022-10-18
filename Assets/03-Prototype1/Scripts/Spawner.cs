using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public EnemyAI[] enemies;
    public Transform[] spawnPoints;

    Wave currentWave;
    int currentWaveIndex;

    int enemiesRemaining;
    public int enemiesAlive;
    float nextSpawnTime;

    bool loadingLevel;

    private void Start()
    {
        currentWaveIndex = 0;
        loadingLevel = true;
        LoadFirstLevel();
    }

    private void Update()
    {
        if (loadingLevel)
        {
            return;
        }
        if (enemiesRemaining > 0 && Time.time >= nextSpawnTime)
        {
            enemiesRemaining--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            EnemyAI enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }

        enemiesAlive = FindObjectsOfType<EnemyAI>().Length;
        if (enemiesRemaining == 0 && enemiesAlive == 0)
        {
            if (!loadingLevel)
                loadingLevel = true;
                StartCoroutine(NextWave());
        }
    }

    public void LoadFirstLevel()
    {
        currentWave = waves[currentWaveIndex];
        enemiesRemaining = currentWave.enemyCount;
        enemies = currentWave.enemyTypesInWave;
        spawnPoints = currentWave.spawnPoints;
        currentWave.waveWalls.SetActive(true);
        loadingLevel = false;
    }

    IEnumerator NextWave()
    {
        if (currentWaveIndex > -1)
        {
            currentWave.waveWalls.SetActive(false);
        }

        yield return new WaitForSeconds(2f);

        if (currentWaveIndex != waves.Length - 1)
        {
            currentWaveIndex++;
        }
        else
        {
            currentWaveIndex = 0;
        }
        
        currentWave = waves[currentWaveIndex];
        enemiesRemaining = currentWave.enemyCount;
        enemies = currentWave.enemyTypesInWave;
        spawnPoints = currentWave.spawnPoints;
        currentWave.waveWalls.SetActive(true);
        loadingLevel = false;
    }

    [System.Serializable]
    public class Wave
    {
        public Transform[] spawnPoints;
        public EnemyAI[] enemyTypesInWave;
        public int enemyCount;
        public float timeBetweenSpawns;
        public GameObject waveWalls;
    }
}
