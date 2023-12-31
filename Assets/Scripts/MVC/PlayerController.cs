using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player playerInput;
    private Charview view;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] public float playerSpeed = 2.0f;
    //[SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private PlayerShoot shootChecking;

    private void Awake()
    {
        view = GetComponent<Charview>();
        playerInput = new Player();
        rb = GetComponent<Rigidbody>();

        shootChecking = GetComponent<PlayerShoot>();
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

        /*if (move.magnitude > 0.3f)
        {
            view.Isrunning(true);
        }
        else
        {
            view.Isrunning(false);
        }*/

        // Aplicamos la velocidad constante en la direcci�n del movimiento.
        rb.velocity = move * playerSpeed;

        // Rotamos al jugador hacia la direcci�n del movimiento.
        if (move != Vector3.zero && shootChecking.shootingPriority==false)
        {
            float rotationSpeed = 15f;

            Quaternion targetRotation = Quaternion.LookRotation(move);
            Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(newRotation);

            view.Isrunning(true);
        }
        else
        {
            view.Isrunning(false);
        }

        /*if (groundedPlayer && playerInput.PlayerMain.Jump.triggered) //esto en caso de agregar un salto
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravityValue), ForceMode.VelocityChange);
        }*/

        // Aplicamos la gravedad.
        rb.AddForce(Vector3.up * gravityValue * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}