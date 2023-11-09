using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] private Player playerInput;
    private PlayerModel model;
    private PlayerView view;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();
        view = GetComponent<PlayerView>();
        playerInput = new Player();
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
        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        model.Move(movementInput);

        view.RotatePlayer(new Vector3(movementInput.x, 0f, movementInput.y).normalized);

        /*if (playerInput.PlayerMain.Jump.triggered)
        {
            model.Jump();
        }*/
    }
}