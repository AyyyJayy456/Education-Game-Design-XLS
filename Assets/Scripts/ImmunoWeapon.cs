using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImmunoWeapon : MonoBehaviour
{
    public GameObject tagShot;
    public Transform firePoint;
    public float cooldown;
    private float nextUseTime;
    static bool isUnlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.isPaused) return;
        if (isUnlocked && Input.GetKeyDown(KeyCode.E) && Time.time >= nextUseTime)
        {
            nextUseTime = Time.time + cooldown;

            Instantiate(tagShot, firePoint.position, firePoint.rotation);
        }
    }

    public static void UnlockWeapon()
    {
        isUnlocked = true;
    }
}
