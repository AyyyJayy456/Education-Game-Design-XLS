using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for if the enemy is NOT targeting the player.
public class EnemyMovement : MonoBehaviour
{
    // Enemy rigidbody and default speed.
    [SerializeField] Rigidbody2D enemyRigidbody;
    [SerializeField] float defaultSpeed = 1f;

    [SerializeField] GameManager manager;

    // Bottom Collider
    [SerializeField] Transform bottomCollider;

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bottomCollider = GameObject.FindGameObjectWithTag("Bottom").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * defaultSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the enemy reaches the bottom of the screen
        if(collision.Equals(bottomCollider.GetComponent<BoxCollider2D>()))
        {
            manager.IncreaseBreached();
        }
        // else if(collision.CompareTag("Bullet"))
        // {
        //     manager.IncreaseKilled();
        // }

        Destroy(gameObject);
    }
}
