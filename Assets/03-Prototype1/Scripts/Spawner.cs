using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public WeaponCrate weaponCrate;
    MapGenerator mapGenerator;
    public Transform playerTransform;

    public Wave[] waves;
    public EnemyAI[] enemiesInCurrentWave;

    Wave currentWave;
    int currentWaveIndex;

    public int enemyRemainingToSpawn;
    public int enemyRemainingAlive;
    float nextSpawnTime;

    bool canSpawn = false;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        currentWaveIndex = 0;
        mapGenerator = FindObjectOfType<MapGenerator>();
        canSpawn = false;
        NextWave(true);
    }

    private void Update()
    {
        if (!canSpawn)
            return;
        if (enemyRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemyRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            StartCoroutine(SpawnEnemy());
        }
    }

    void StartSpawning()
    {
        canSpawn = true;
    }

    void NextWave(bool isFirstRound)
    {
        canSpawn = false;
        if (!isFirstRound)
            currentWaveIndex++;
        if (currentWaveIndex >= waves.Length)
        {
            currentWaveIndex = 0;
        }
        currentWave = waves[currentWaveIndex];

        enemiesInCurrentWave = currentWave.enemyTypesInWave;
        enemyRemainingToSpawn = currentWave.enemyCount;
        enemyRemainingAlive = enemyRemainingToSpawn;

        Invoke("StartSpawning", 3f);
    }

    IEnumerator SpawnEnemy()
    {
        float spawnerDelay = 1;
        float tileFlashSpeed = 4;
        Transform randomTile = mapGenerator.GetRandomOpenTIle();
        while (Vector3.Distance(randomTile.position, playerTransform.position) < 4)
        {
            // can't spawn near player
            randomTile = mapGenerator.GetRandomOpenTIle();
        }
        Material tileMat = randomTile.GetComponent<Renderer>().material;
        Color initialColor = tileMat.color;
        Color flashColor = Color.red;
        float spawnTimer = 0;

        while (spawnTimer < spawnerDelay)
        {
            tileMat.color = Color.Lerp(initialColor, flashColor, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));
            spawnTimer += Time.deltaTime;
            yield return null;
        }


        EnemyAI spawnedEnemy = Instantiate(enemiesInCurrentWave[Random.Range(0, enemiesInCurrentWave.Length)], randomTile.position, Quaternion.identity);
        spawnedEnemy.GetComponent<EnemyHealthManager>().OnDeath += OnEnemyDeath;
    }

    public void OnEnemyDeath()
    {
        enemyRemainingAlive--;
        if (enemyRemainingAlive == 0)
        {
            StartCoroutine(NextWaveCoroutine());
        }
    }

    public IEnumerator NextWaveCoroutine()
    {
        mapGenerator.GenerateNextLevel();
        DropWeaponCrate();
        yield return new WaitForSeconds(3f);
        NextWave(false);
    }

    public void DropWeaponCrate()
    {
        WeaponCrate newCrate = Instantiate(weaponCrate, mapGenerator.GetRandomOpenTIle().position, Quaternion.identity);
        Destroy(newCrate, 60f);
    }

    [System.Serializable]
    public class Wave
    {
        public EnemyAI[] enemyTypesInWave;
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
