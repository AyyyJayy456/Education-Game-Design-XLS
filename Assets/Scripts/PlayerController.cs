using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public Transform firePoint;
    public GameObject bullet;
    public float timeBetweenShots;
    private float shotCounter;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimations();

        if (PauseManager.isPaused) return;

        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * moveSpeed;

        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 mouseDistance = mousePos - screenPos;
        float angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
        }
    }

    private void HandleAnimations()
    {
        //if we are moving and slow down too much
        if (rb.velocity.magnitude < .5f && animator.GetBool("IsMoving"))
        {
            animator.SetBool("IsMoving", false);
        }

        //if we are not moving and start moving
        else if (rb.velocity.magnitude > 0f && !animator.GetBool("IsMoving"))
        {
            animator.SetBool("IsMoving", true);
        }
    }
}
