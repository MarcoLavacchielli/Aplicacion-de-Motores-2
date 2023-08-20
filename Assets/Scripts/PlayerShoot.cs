using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootController;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootingCooldown = 0.5f;
    [SerializeField] private float lastShootTime;

    private void Start()
    {
        lastShootTime = -shootingCooldown;
    }

    private void Update()
    {
        if (Time.time - lastShootTime >= shootingCooldown && Input.GetButtonDown("Fire1"))
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, shootController.position, shootController.rotation);
        Destroy(newBullet, 3f);
    }
}
