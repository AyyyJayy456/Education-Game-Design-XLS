using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody2D enemyRigidbody;
    [SerializeField] float requiredPlayerDistance = 5f;
    [SerializeField] float requiredDestroyDistance = 0.5f;

    [SerializeField] float defaultSpeed = 1f;
    [SerializeField] float honingSpeed = 2f;

    enum enemyStates {CLUNG, HONE, DESTROY};
    private enemyStates currentState = enemyStates.CLUNG;

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetermineState();
        SetBehavior();
    }

    void DetermineState()
    {
        float currentDistance = Vector2.Distance(enemyRigidbody.position, playerTransform.position);

        if(currentDistance > requiredPlayerDistance)
        {
            currentState = enemyStates.CLUNG;
        }
        else if(currentDistance > requiredDestroyDistance)
        {
            currentState = enemyStates.HONE;
        }
        else
        {
            currentState = enemyStates.DESTROY;
        }
    }

    void SetBehavior()
    {
        switch(currentState)
        {
            case enemyStates.CLUNG:
            {
                transform.position += Vector3.down * defaultSpeed * Time.deltaTime;
                break;
            }
            case enemyStates.HONE:
            {
                Vector2 target = playerTransform.position;
                transform.position = Vector2.MoveTowards(transform.position, target, honingSpeed * Time.deltaTime);
                break;
            }
            case enemyStates.DESTROY:
            {
                Destroy(gameObject);
                break;
            }
        }
    }

}
