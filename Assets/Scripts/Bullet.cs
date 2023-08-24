using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody rb;

    private Pool<Bullet> pool;

    public void SetPool(Pool<Bullet> pool)
    {
        this.pool = pool;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        pool.Return(this);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }*/
}
