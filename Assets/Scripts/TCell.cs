using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TCell : MonoBehaviour
{
    public CancerCell target;
    public float speed, angle, padding = 0.25f, orbitSpeed = 20f, damage = 1f, tickRate = 0.5f;
    private float timer;
    private Collider2D targetCol;

    // Start is called before the first frame update
    void Start()
    {
        speed *= Random.Range(0.8f, 1.2f);
        if (Random.value < 0.5f) orbitSpeed = -orbitSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (targetCol == null) targetCol = target.GetComponent<Collider2D>();

        angle += orbitSpeed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Vector2 center = targetCol.bounds.center;
        Vector2 surface = targetCol.ClosestPoint(center + dir * 10f);
        Vector2 destination = surface + dir * padding;

        transform.position = Vector2.MoveTowards(transform.position, 
            destination, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, destination) < 0.15f)
        {
            timer += Time.deltaTime;

            if (timer >= tickRate)
            {
                timer = 0f;
                target.TakeDamage(damage);
            }
        }
    }
}
