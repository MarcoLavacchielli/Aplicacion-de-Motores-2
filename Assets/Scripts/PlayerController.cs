using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player playerInput;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    [SerializeField] private Rotation shootChecking;

    private void Awake()
    {
        playerInput = new Player();
        rb = GetComponent<Rigidbody>();

        shootChecking = GetComponent<Rotation>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

        // Aplicamos la velocidad constante en la dirección del movimiento.
        rb.velocity = move * playerSpeed;

        // Rotamos al jugador hacia la dirección del movimiento.
        if (move != Vector3.zero && shootChecking.shootingPriority==false)
        {
            Quaternion newRotation = Quaternion.LookRotation(move);
            rb.MoveRotation(newRotation);
        }

        /*if (groundedPlayer && playerInput.PlayerMain.Jump.triggered) //esto en caso de agregar un salto
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravityValue), ForceMode.VelocityChange);
        }*/

        // Aplicamos la gravedad.
        rb.AddForce(Vector3.up * gravityValue * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}