using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public WeaponCrate weaponCrate;
    MapGenerator mapGenerator;
    public Transform playerTransform;
    public TMP_Text waveStartEndText;
    public TMP_Text enemyRemainText;

    public Wave[] waves;
    public EnemyAI[] enemiesInCurrentWave;

    Wave currentWave;
    int currentWaveIndex;

    public int enemyRemainingToSpawn;
    public int enemyRemainingAlive;
    float nextSpawnTime;
    public int enemySpawnCounter = 0;

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
        waveStartEndText.enabled = false;
        enemyRemainText.text = enemyRemainingAlive.ToString();
        canSpawn = true;
    }

    void NextWave(bool isFirstRound)
    {
        enemySpawnCounter = 0;
        waveStartEndText.text =  "WAVE" + " " + currentWaveIndex + 1;
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
        Debug.Log("enemyRemainingToSpawn: "+enemyRemainingToSpawn);
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
        enemySpawnCounter++;
        Debug.Log("SpawnerCounter" + enemySpawnCounter);
    }

    public void OnEnemyDeath()
    {
        enemyRemainingAlive--;
        enemyRemainText.text = enemyRemainingAlive.ToString();
        if (enemyRemainingAlive == 0)
        {
            canSpawn = false;
            StartCoroutine(NextWaveCoroutine());
        }
    }

    public IEnumerator NextWaveCoroutine()
    {
        waveStartEndText.text = "WAVE CLEARED \n GRAB YOUR REWARD";
        waveStartEndText.enabled = true;
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
