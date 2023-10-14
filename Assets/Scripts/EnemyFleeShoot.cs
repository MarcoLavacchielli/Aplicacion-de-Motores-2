using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeShoot : MonoBehaviour
{
    [SerializeField] private Agent agent;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float shootFrequency = 1f;
    [SerializeField] private float shootRadius;

    private Transform player;
    private float timeSinceLastShot;

    private void Awake()
    {
        agent = GetComponent<Agent>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastShot = 0f;
    }

    private void Update()
    {
        if (agent.isFlee==false && PlayerInDetectionRadius())
        {
            timeSinceLastShot += Time.deltaTime;

            // Disparar balas al jugador si ha pasado el tiempo de frecuencia
            if (timeSinceLastShot >= 1f / shootFrequency)
            {
                ShootAtPlayer();
                timeSinceLastShot = 0f; // Reiniciar el tiempo
            }
        }
    }

    private bool PlayerInDetectionRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, shootRadius, LayerMask.GetMask("Player"));
        return colliders.Length > 0;
    }

    void ShootAtPlayer()
    {
        Vector3 direction = player.position - bulletOrigin.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, rotation);
        //bullet.layer = LayerMask.NameToLayer("EnemyBullet");

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * bulletSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
}