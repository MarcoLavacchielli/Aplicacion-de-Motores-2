using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Charview view;
    private PlayerShoot shootChecking;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private ParticleSystem runningParticle;

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

            // Ta moviendose?
            if (move.magnitude > 0.1f)
            {
                view.Isrunning(true);
                PlayRunningParticles(); // particle.play
            }
            else
            {
                view.Isrunning(false);
                StopRunningParticles(); // particle.stop
            }
        }
        else if (move == Vector3.zero)
        {
            view.Isrunning(false);
            StopRunningParticles(); // aseguramiento porque se tara
        }
    }

    private void PlayRunningParticles()
    {
        if (runningParticle != null && !runningParticle.isPlaying)
        {
            runningParticle.Play();
        }
    }

    private void StopRunningParticles()
    {
        if (runningParticle != null && runningParticle.isPlaying)
        {
            runningParticle.Stop();
        }
    }
}