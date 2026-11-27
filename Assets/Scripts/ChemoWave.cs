using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemoWave : MonoBehaviour
{
    public float maxRadius, duration = 0.5f;
    private float timer;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;
        transform.localScale = Vector3.one * (maxRadius * 2f * t);
        Color color = sr.color;
        color.a = 1f - t;
        sr.color = color;

        if (t >= 1)
        {
            Destroy(gameObject);
        }
    }
}
