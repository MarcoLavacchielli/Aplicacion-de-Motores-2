using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{

    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _timer = 2f;
    [SerializeField] private float _range = 10f; // Rango de detección del jugador

    [SerializeField] private int _counter; // Esto está comentado en el for pero es un límite de balas
    [SerializeField] private int _maxCounter = 20;

    private Transform _player;
    private Coroutine _fireCoroutine;

    public ParticleSystem dustTiro;

    private bool _isPreparingToFire = false; // Indica si la torreta está preparándose para disparar

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        // Calcular la distancia entre la torreta y el jugador
        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= _range)
        {
            // El jugador está dentro del rango y no hay corutina en ejecución, iniciar la corutina
            if (_fireCoroutine == null)
            {
                _fireCoroutine = StartCoroutine(FireBullets_CR());

            }
        }
        else
        {
            // El jugador está fuera del rango, detener la corutina si está en ejecución
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
                _fireCoroutine = null;

            }
        }
    }

    IEnumerator FireBullets_CR()
    {
        Debug.Log("Inicio coroutine");
        yield return new WaitForSeconds(0.5f); // Esperar un segundo antes de disparar la primera bala

        for (int i = 0; i < _maxCounter; i++)
        {
            dustTiro.Play();


            yield return new WaitForSeconds(0.2f); // Esperar un segundo para el cambio de material

            Instantiate(_bullet, transform.position, transform.rotation);


            yield return new WaitForSeconds(_timer);
        }


        Debug.Log("Fin coroutine");
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar una esfera gizmo para representar el rango de detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
