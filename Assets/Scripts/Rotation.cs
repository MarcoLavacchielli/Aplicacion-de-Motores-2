using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Player playerInput;  //Direccion del joystick
    [SerializeField] private Bullet bulletPrefab;   //Llama al prefab de la bala
    [SerializeField] private Transform shootController;   // Ubicacion de la bala (donde se instancia)
    [SerializeField] private Rigidbody rb;  //RigidBody de la bala

    [SerializeField] private List<Bullet> bulletPool = new List<Bullet>();  //Lista de balas prehechas
    [SerializeField] private int bulletIndex = 0; //Inidice para saber que bala se va a utilizar

    public bool shootingPriority;   // Esto es un booleano para sobreescribir la rotacion
    [SerializeField] private float shootingCooldown = 0.5f;  //Tiempo entre disparos
    [SerializeField] private float lastShootTime;  //Controlar el tiempo de disparo
    [SerializeField] private float anticipationTime = 1f;  //El primer disparo va a salir despues de esta funcion

    private void Awake()
    {
        playerInput = new Player();
        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < 12; i++)   //instancia 12 balas que son las de la bolsa
        {
            Bullet newBullet = Instantiate(bulletPrefab);
            bulletPool.Add(newBullet);
            //newBullet.gameObject.SetActive(false); // SeInstancia las balas al inicio (al pedo)
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

    private void Start()
    {
        lastShootTime = Time.time;
    }

    private void FixedUpdate()
    {
        Vector2 movementInput = playerInput.PlayerMain.Look.ReadValue<Vector2>();

        if (movementInput.magnitude > 0.1f)  //Checkea si se movio el joystick
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

    private void RotatePlayer(Vector2 direction)  //rota al jugador segun la entrada obtenida
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

    private void Shoot() //Metodo de disparo, obtiene una bala del pool y la lanza desde la posicion del controller
    {
        Bullet newBullet = GetNextBullet();
        newBullet.transform.position = shootController.position;
        newBullet.transform.rotation = shootController.rotation;
        newBullet.gameObject.SetActive(true);
        newBullet.Launch();
    }

    private Bullet GetNextBullet() // Obtiene la siguiente bala desde el pool
    {
        Bullet bullet = bulletPool[bulletIndex];
        bulletIndex = (bulletIndex + 1) % bulletPool.Count;

        // If the bullet is already active, deactivate it before returning
        if (bullet.gameObject.activeSelf)
        {
            bullet.gameObject.SetActive(false);
        }

        return bullet;
    }
}