using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeBullet : MonoBehaviour
{
    [SerializeField] private float velocity = 1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 30f;

    [SerializeField] private List<string> destroyOnCollisionLayers;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float currentY = 1;  //Velocidad en 1 porque si no la bala caeee todo el tiempo, por motivos que solo unity entiende y escapan de mi logica
        transform.Translate(new Vector3(0f, 0f, velocity * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (destroyOnCollisionLayers.Contains(LayerMask.LayerToName(other.gameObject.layer)))
        {
            Destroy(gameObject);
        }

    }
}