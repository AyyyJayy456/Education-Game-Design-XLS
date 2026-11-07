using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;

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
        Debug.Log(health);

        if (health <= 0)
        {
            Debug.Log("Player is Overwhelmed. Retreat!");
        }
    }
}
