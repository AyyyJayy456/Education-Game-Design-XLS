using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerMethodCaller : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerEntered?.Invoke();
            Destroy(gameObject);
        }
    }
}
