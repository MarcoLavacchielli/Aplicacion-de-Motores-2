using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int velocity;   //Velocidad de la bala
    [SerializeField] private int damage;  //Da�o de la vala
    [SerializeField] private Rigidbody rb;  //Obtiene el rigid del enemigo

    public Pool<Bullet> pool;

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