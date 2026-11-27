using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemoWeapon : MonoBehaviour
{
    public float radius, damage, selfDamage, coolDown;
    private float nextUseTime;
    private PlayerHealth playerHealth;
    public float fastDividingMultiplier = 2f;
    public GameObject wavePrefab;
    public bool waveActive = false;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.isPaused) return;
        if (Input.GetMouseButtonDown(1) && Time.time >= nextUseTime && waveActive)
        {
            nextUseTime = Time.time + coolDown;
            Debug.Log("Right Click");
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
            Debug.Log(hits.Length);

            GameObject wave = Instantiate(wavePrefab, transform.position, Quaternion.identity);
            ChemoWave fetchWave = wave.GetComponent<ChemoWave>();
            fetchWave.maxRadius = radius;

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    CancerCell cell = hit.GetComponent<CancerCell>();
                    if (cell != null )
                    {
                        float finalDamage = damage;
                        if (cell.fastDividing)
                        {
                            finalDamage *= fastDividingMultiplier;
                        }
                        cell.TakeDamage(finalDamage);
                    }
                    Debug.Log(hit.name);
                }
            }
            playerHealth.TakeDamage(selfDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
