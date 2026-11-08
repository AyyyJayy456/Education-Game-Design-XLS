using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    // The Enemy Prefab and it's spawn location
    [SerializeField] GameObject prefab;
    Vector2 spawnPoint;

    // Cooldown, timer, and rate for spawning.
    [SerializeField] float cooldown;
    float actualCooldown;
    [SerializeField] float spawnRate = 1;
    float timer;
    bool timeToSpawn;
    [SerializeField] bool keepSpawning = true;

    // Enemy List, and Restriction
    [SerializeField] int maxEnemies;
    [SerializeField] Transform enemyList;

    // Walls and Boundary
    [SerializeField] Transform wallOne;
    [SerializeField] Transform wallTwo;
    [SerializeField] Transform topBoundary;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = GameObject.Find("EnemyList").transform;
        timeToSpawn = true;

        actualCooldown = cooldown / spawnRate;
        timer = actualCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        DetermineSpawning();
        
        if(timeToSpawn && keepSpawning)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        RandomizeSpawn();
        Instantiate(prefab, spawnPoint, Quaternion.identity, enemyList);
        timeToSpawn = false;
    }
    
    void RandomizeSpawn()
    {
        int maxAttempts = 10;
        Vector2 wallWidth = wallOne.GetComponent<SpriteRenderer>().bounds.size;

        for(int i = 0; i < maxAttempts; i++)
        {
            //spawnPoint = new Vector2(Random.Range(wallOne.position.x, wallTwo.position.x), topBoundary.position.y);
            spawnPoint = new Vector2(topBoundary.position.x, Random.Range(wallOne.position.y, wallTwo.position.y));


            Collider2D collider = Physics2D.OverlapBox(wallOne.transform.position, wallWidth, 0);

            if(collider == null)
            {
                break;
            }
        }
    }

    void DetermineSpawning()
    {
        timer += Time.deltaTime;

        if(enemyList.childCount >= maxEnemies)
        {
            timeToSpawn = false;
        }
        else if(timer >= actualCooldown)
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
