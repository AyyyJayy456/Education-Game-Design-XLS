using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancerCell : MonoBehaviour
{
    public float health;
    public bool fastDividing;
    public bool isTagged;
    private SpriteRenderer sr;
    public GameObject tCell;
    public int tCellNum = 3;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        manager = FindAnyObjectByType<GameManager>();
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
            manager.IncreaseKilled();
        }
    }

    [ContextMenu("Tag")]

    public void Tag()
    {
        if (isTagged)
        {
            return;
        }

        isTagged = true;
        sr.color = Color.cyan;
        
        for (int i = 0; i < tCellNum; i++)
        {
            Vector3 worldPoint = Camera.main.ViewportToWorldPoint(RandomEdgePoint());
            worldPoint.z = 0f;
            GameObject tC = Instantiate(tCell, worldPoint, Quaternion.identity);
            TCell tCScript = tC.GetComponent<TCell>();
            tCScript.target = this;
            //Perfect Surrounding Circle
            //tCScript.angle = 360f / tCellNum * i;
            //Varied Surrounding Circle
            tCScript.angle = 360f / tCellNum * i + Random.Range(-15f, 15f);
        }
    }

    private Vector3 RandomEdgePoint()
    {
        int side = Random.Range(0, 4);
        float along = Random.value;

        if (side == 0)
        {
            return new Vector3(-0.1f, along, 0f);
        }
        else if (side == 1)
        {
            return new Vector3(1.1f, along, 0f);
        }
        else if (side == 2)
        {
            return new Vector3(along, -0.1f, 0f);
        }
        else
        {
            return new Vector3(along, 1.1f, 0f);
        }
    }
}
