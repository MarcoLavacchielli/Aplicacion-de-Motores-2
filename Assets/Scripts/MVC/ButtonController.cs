using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : Controller
{

    public PlayerArrow playerArrow;
    public bool moving;

    [SerializeField] private PlayerShoot shootChecking;

    private void Update()
    {
        if (moving == true)
        {
            playerArrow.PlayParticlesAndAnimation();
            if (!shootChecking.shootingPriority)
            {
                RotatePlayer();
            }
            playerArrow.pp();
        }
        else
        {
            playerArrow.sp();
        }
    }

    private void RotatePlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_moveDir, Vector3.up);

        float rotationSpeed = 10.0f;
        playerArrow.transform.rotation = Quaternion.Slerp(playerArrow.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public override Vector3 GetmovementInput()
    {
        return _moveDir;
    }

    public void MoveForward()
    {
        _moveDir = Vector3.forward;
        moving = true;
    }

    public void MoveBack()
    {
        _moveDir = Vector3.back;
        moving = true;
    }

    public void MoveLeft()
    {
        _moveDir = Vector3.left;
        moving = true;
    }

    public void MoveRight()
    {
        _moveDir = Vector3.right;
        moving = true;
    }

    public void Static()
    {
        _moveDir = Vector3.zero;
        moving = false;
    }
}
