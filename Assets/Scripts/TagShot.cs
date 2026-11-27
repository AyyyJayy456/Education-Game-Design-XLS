using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagShot : MonoBehaviour
{
    public float speed, lifetime;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CancerCell cell = other.gameObject.GetComponent<CancerCell>();

            if (cell != null)
            {
                cell.Tag();
            }
            Destroy(gameObject);
        }

        //Instantiate(impactEffect, transform.position, Quaternion.identity);
    }
}
