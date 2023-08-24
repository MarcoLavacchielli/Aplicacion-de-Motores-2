using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Player playerInput; //Pide el mapa
    [SerializeField] private GameObject bullet; //pide el prefab
    [SerializeField] private Transform shootController; // spot de donde salen las balas
    [SerializeField] private Rigidbody rb; // rigid del personaje para rotarlo
    public bool shootingPriority; //checkea si esta disparando
    [SerializeField] private float shootingCooldown = 0.5f;
    [SerializeField] private float lastShootTime;
    [SerializeField] private float anticipationTime = 1f; // Nuevo tiempo de anticipación
    [SerializeField] private Bullet prefab;

    private Pool<Bullet> pool = new Pool<Bullet>();

    private void Awake()
    {
        playerInput = new Player();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 movementInput = playerInput.PlayerMain.Look.ReadValue<Vector2>();

        if (movementInput.magnitude > 0.1f)
        {
            if (!shootingPriority && Time.time - lastShootTime >= anticipationTime)
            {
                shootingPriority = true;
                lastShootTime = Time.time; // Actualizar el tiempo de último movimiento
            }

            if (shootingPriority && Time.time - lastShootTime >= shootingCooldown)
            {
                Shoot();
                lastShootTime = Time.time;
            }

            RotatePlayer(movementInput);
        }
        else
        {
            shootingPriority = false;
        }
    }

    private void RotatePlayer(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y).normalized;

        if (moveDirection != Vector3.zero)
        {
            float rotationSpeed = 15f;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(newRotation);
        }
    }

    private void Shoot()
    {
        /*if (pool.TryRent(out Bullet bullet))
        {
            bullet = Instantiate(prefab);
            bullet.SetPool(pool);
        }
        bullet.gameObject.SetActive(true);
        bullet.transform.position = shootController.transform.position;
        bullet.transform.rotation = shootController.transform.rotation;*/

        GameObject newBullet = Instantiate(bullet, shootController.position, shootController.rotation);
        Destroy(newBullet, 3f);
    }
}