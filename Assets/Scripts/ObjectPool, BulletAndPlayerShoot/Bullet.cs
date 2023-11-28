using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private int velocity;   //Velocidad de la bala
    [SerializeField] private int damage;  //Daño de la vala
    [SerializeField] private Rigidbody rb;  //Obtiene el rigid del enemigo

    private Pool<IBullet> pool;

    [SerializeField] ParticleSystem bulletDestroyP;
    private Vector3 initialPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    public void Launch() // Lanza la bala para adelante y se tiene una funcion para devolver la bala a la bolsa luego de 7 segundos
    {
        rb.velocity = transform.forward * velocity;
        StartCoroutine(ReturnAfterSeconds(3f)); // Return after 7 seconds
    }

    void IBullet.SetActive(bool active) => gameObject.SetActive(active);

    void IBullet.SetPool(Pool<IBullet> pool) => this.pool = pool;

    void IBullet.SetPositionRotation(Vector3 position, Quaternion rotation) => transform.SetLocalPositionAndRotation(position, rotation);

    T IBullet.GetComponent<T>() => GetComponent<T>();

    private IEnumerator ReturnAfterSeconds(float seconds) //corrutina para devolver la bala a la bolsa
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
        pool.Return(this);

        PlayBulletDestroyParticles(initialPosition);
    }

    private void OnCollisionEnter(Collision collision) // Se desactiva la bala cuando colisiona con algo
    {
        if(collision.gameObject.TryGetComponent(out IDamage enemy))
        {
            enemy.TakeDamage((int)damage);
        }

        StopCoroutine(ReturnAfterSeconds(3f));
        gameObject.SetActive(false);
        pool.Return(this);

        PlayBulletDestroyParticles(collision.contacts[0].point);
    }

    private void PlayBulletDestroyParticles(Vector3 position)
    {
        ParticleSystem bulletDestroyClone = Instantiate(bulletDestroyP, position, Quaternion.identity);
        bulletDestroyClone.Play();

        ParticleSystem.MainModule mainModule = bulletDestroyClone.main;
        //mainModule.duration = bulletDestroyP.main.duration;

        Destroy(bulletDestroyClone.gameObject, bulletDestroyP.main.duration);
    }
}