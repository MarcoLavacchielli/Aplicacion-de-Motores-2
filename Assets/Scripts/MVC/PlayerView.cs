using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Charview view;
    private PlayerShoot shootChecking;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        view = GetComponent<Charview>();
        shootChecking = GetComponent<PlayerShoot>();
        rb = GetComponent<Rigidbody>();
    }

    public void RotatePlayer(Vector3 move)
    {
        if (move != Vector3.zero && !shootChecking.shootingPriority)
        {
            float rotationSpeed = 15f;
            Quaternion targetRotation = Quaternion.LookRotation(move);
            Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(newRotation);

            // Verificar si el vector de movimiento es lo suficientemente grande
            if (move.magnitude > 0.1f)
            {
                view.Isrunning(true);
            }
            else
            {
                view.Isrunning(false);
            }
        }
        else if (move == Vector3.zero)
        {
            view.Isrunning(false);
        }
    }
}