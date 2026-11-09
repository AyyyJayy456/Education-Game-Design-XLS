using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [Tooltip("Slot 0: Standard Cell (CancerCell), Slot 1: Receptor Cell (enemy2)")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Range(0f, 1f)]
    [Tooltip("Probability of spawning Enemy #2 (Receptor variant). e.g., 0.3 = 30%")]
    [SerializeField] private float receptorEnemyChance = 0.35f;

    Vector2 spawnPoint;


    [Header("Spawn Settings")]
    [SerializeField] float cooldown = 2f;
    float actualCooldown;
    [SerializeField] float spawnRate = 1f;
    float timer;
    bool timeToSpawn;
    [SerializeField] bool keepSpawning = false;

    [Header("Restrictions & Hierarchy")]
    [SerializeField] int maxEnemies = 10;
    [SerializeField] Transform enemyList;

    [Header("Boundaries")]
    [SerializeField] Transform wallOne;
    [SerializeField] Transform wallTwo;
    [SerializeField] Transform topBoundary;

    void Start()
    {
        enemyList = GameObject.Find("EnemyList").transform;
        timeToSpawn = false;  

        actualCooldown = cooldown / spawnRate;
        timer = 0;
    }
    public void StartSpawning()
    {
        keepSpawning = true;
        timeToSpawn = true;
    }


    void Update()
    {
        if (Time.timeScale == 0f) return;

        DetermineSpawning();

        if (timeToSpawn && keepSpawning)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

        RandomizeSpawn();

        // Choose which enemy to spawn based on probability
        GameObject selectedPrefab = enemyPrefabs[0];
        if (enemyPrefabs.Length > 1 && Random.value < receptorEnemyChance)
        {
            selectedPrefab = enemyPrefabs[1];
        }

        Instantiate(selectedPrefab, spawnPoint, Quaternion.identity, enemyList);
        timeToSpawn = false;
    }

    void RandomizeSpawn()
    {
        if (wallOne == null || wallTwo == null || topBoundary == null) return;

        // Spawn along the top horizontal span between left and right boundary walls
        float spawnX = Random.Range(Mathf.Min(wallOne.position.x, wallTwo.position.x), Mathf.Max(wallOne.position.x, wallTwo.position.x));
        spawnPoint = new Vector2(spawnX, topBoundary.position.y);
    }

    void DetermineSpawning()
    {
        timer += Time.deltaTime;

        if (enemyList != null && enemyList.childCount >= maxEnemies)
        {
            timeToSpawn = false;
        }
        else if (timer >= actualCooldown)
        {
            timeToSpawn = true;
            timer = 0;
        }
    }

    public void SetSpawning(bool val)
    {
        keepSpawning = val;
    }
}