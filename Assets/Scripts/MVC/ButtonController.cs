using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : Controller
{
    private PlayerShoot shootChecking;
    private Charview view;

    private void Awake()
    {
        shootChecking = GetComponent<PlayerShoot>();
        view = GetComponent<Charview>();
    }

    public override Vector3 GetmovementInput()
    {
        return _moveDir;
    }

    public void MoveForward()
    {
        _moveDir = Vector3.forward;
    }

    public void MoveBack()
    {
        _moveDir = Vector3.back;
    }

    public void MoveLeft()
    {
        _moveDir = Vector3.left;
    }

    public void MoveRight()
    {
        _moveDir = Vector3.right;
    }

    public void Static()
    {
        _moveDir = Vector3.zero;
    }
}
