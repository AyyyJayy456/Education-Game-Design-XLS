using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public float damage;

    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            
            if (player != null)
            {
                player.TakeDamage(damage);
                player.TakeKnockback(transform.position, 100f);
            }
        }

        Destroy(gameObject);

        //Instantiate(impactEffect, transform.position, Quaternion.identity);
    }
}
