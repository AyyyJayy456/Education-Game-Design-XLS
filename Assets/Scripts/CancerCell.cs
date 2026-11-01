using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancerCell : MonoBehaviour
{
    public float health;
    public bool fastDividing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
