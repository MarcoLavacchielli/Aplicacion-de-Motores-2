using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Player playerInput;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootController;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private List<Bullet> bulletPool = new List<Bullet>();
    [SerializeField] private int bulletIndex = 0;

    public bool shootingPriority;
    [SerializeField] private float shootingCooldown = 0.5f;
    [SerializeField] private float lastShootTime;
    [SerializeField] private float anticipationTime = 1f;

    private void Awake()
    {
        playerInput = new Player();
        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < 12; i++)
        {
            Bullet newBullet = Instantiate(bulletPrefab);
            newBullet.gameObject.SetActive(false);
            bulletPool.Add(newBullet);
        }
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
                lastShootTime = Time.time;
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
        Bullet newBullet = GetNextBullet();
        newBullet.transform.position = shootController.position;
        newBullet.transform.rotation = shootController.rotation;
        newBullet.gameObject.SetActive(true);
        newBullet.Launch(); // Added line to launch the bullet
    }

    private Bullet GetNextBullet()
    {
        Bullet bullet = bulletPool[bulletIndex];
        bulletIndex = (bulletIndex + 1) % bulletPool.Count;
        return bullet;
    }
}