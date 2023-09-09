using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;   //Velocidad de la bala
    [SerializeField] private float damage;  //Daño de la vala
    [SerializeField] private Rigidbody rb;  //Obtiene el rigid del enemigo

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    private void OnCollisionEnter(Collision collision) // Se desactiva la bala cuando colisiona con algo
    {
        gameObject.SetActive(false);
    }
}