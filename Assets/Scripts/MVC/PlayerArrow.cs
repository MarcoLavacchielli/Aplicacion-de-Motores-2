using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArrow : MonoBehaviour
{
    /*private Charview view;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 playerVelocity;*/
    [SerializeField] public float playerSpeed = 5.0f;
    /*[SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private PlayerShoot shootChecking;*/
    [SerializeField] Controller _controller;

    [SerializeField] private ParticleSystem runningParticle;

    void Update()
    {
        transform.position += _controller.GetmovementInput() * playerSpeed * Time.deltaTime;
    }
}
