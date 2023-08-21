using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Player playerInput;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootController;

    [SerializeField] private Rigidbody rb;

    public bool shootingPriority;
    [SerializeField] private bool isFirstRotation = true; // Nuevo booleano para rastrear la primera rotación
    [SerializeField] private float rotationCooldown = 0.2f; // Tiempo para la primera rotación

    [SerializeField] private float shootingCooldown = 0.5f;
    [SerializeField] private float lastShootTime;

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
            shootingPriority = true;

            if (isFirstRotation)
            {
                if (Time.time >= rotationCooldown)
                {
                    isFirstRotation = false;
                }
            }
            else if (Time.time - lastShootTime >= shootingCooldown)
            {
                Shoot();
                lastShootTime = Time.time;
            }

            RotatePlayer(movementInput);
        }
        else
        {
            shootingPriority = false;
            isFirstRotation = true; // Reiniciar el booleano de la primera rotación
        }
    }

    private void RotatePlayer(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(newRotation);
        }
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, shootController.position, shootController.rotation);
        Destroy(newBullet, 3f);
    }
}