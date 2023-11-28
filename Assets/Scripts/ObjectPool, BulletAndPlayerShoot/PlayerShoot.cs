using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Player playerInput;  //Direccion del joystick
    [SerializeField] private Bullet bulletPrefab;   //Llama al prefab de la bala
    [SerializeField] private Transform shootController;   // Ubicacion de la bala (donde se instancia)
    [SerializeField] private Rigidbody rb;  //RigidBody de la bala

    //private List<Bullet> bulletPool = new List<Bullet>();  //Lista de balas prehechas
    //private int bulletIndex = 0; //Inidice para saber que bala se va a utilizar
    private Pool<IBullet> bulletsPool = new Pool<IBullet>();

    public bool shootingPriority;   // Esto es un booleano para sobreescribir la rotacion
    [SerializeField] private float shootingCooldown = 0.5f;  //Tiempo entre disparos
    [SerializeField] private float lastShootTime;  //Controlar el tiempo de disparo
    [SerializeField] private float anticipationTime = 1f;  //El primer disparo va a salir despues de esta funcion

    [SerializeField] private ParticleSystem shootParticle;

    private void Awake()
    {
        playerInput = new Player();
        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < 12; i++)   //instancia 12 balas que son las de la bolsa
        {
            IBullet item = CreateBullet();
            item.SetActive(false);
            bulletsPool.Return(item);
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
        shootParticle.Play();
        IBullet newBullet = GetNextBullet();
        newBullet.SetPositionRotation(shootController.position, shootController.rotation);
        newBullet.SetActive(true);
        newBullet.Launch();
    }

    private IBullet GetNextBullet() // Obtiene la siguiente bala desde el pool
    {
        if(bulletsPool.TryRent(out IBullet item))
        {
            item.SetActive(true);
            return item;
        }
        return CreateBullet();
    }
    private IBullet CreateBullet()
    {
        IBullet bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletsPool);
        return new SomeBulletDecorator(bullet, null);
    }
}

public class SomeBulletDecorator : IBullet
{
    private readonly IBullet bullet;
    private readonly Material colorMaterial;

    public SomeBulletDecorator(IBullet bullet, Material colorMaterial)
    {
        this.bullet = bullet;
        this.colorMaterial = colorMaterial;
    }

    public void Launch()
    {
        bullet.GetComponent<Renderer>().material = colorMaterial;
        bullet.Launch();
    }

    public void SetActive(bool active) => bullet.SetActive(active);

    public void SetPool(Pool<IBullet> pool) => bullet.SetPool(pool);

    public void SetPositionRotation(Vector3 position, Quaternion rotation) => bullet.SetPositionRotation(position, rotation);

    T IBullet.GetComponent<T>() => bullet.GetComponent<T>();
}
public interface IBullet
{
    void Launch();
    void SetPool(Pool<IBullet> pool);
    void SetActive(bool active);
    void SetPositionRotation(Vector3 position, Quaternion rotation);
    T GetComponent<T>();
}