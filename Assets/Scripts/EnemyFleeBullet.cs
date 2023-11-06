using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeBullet : MonoBehaviour
{
    [SerializeField] private float velocity = 1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 30f;
    [SerializeField] private List<string> destroyOnCollisionLayers;

    [SerializeField] private ParticleSystem bulletDestroyP;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float currentY = 1;
        transform.Translate(new Vector3(0f, 0f, velocity * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            DestroyBulletAndPlayParticles();
        }

        if (destroyOnCollisionLayers.Contains(LayerMask.LayerToName(other.gameObject.layer)))
        {
            DestroyBulletAndPlayParticles();
        }
    }

    private void DestroyBulletAndPlayParticles()
    {
        Vector3 destructionPosition = transform.position;

        Destroy(gameObject);

        PlayBulletDestroyParticles(destructionPosition);
    }

    private void PlayBulletDestroyParticles(Vector3 position)
    {
        ParticleSystem bulletDestroyClone = Instantiate(bulletDestroyP, position, Quaternion.identity);
        bulletDestroyClone.Play();
        Destroy(bulletDestroyClone.gameObject, bulletDestroyP.main.duration);
    }
}