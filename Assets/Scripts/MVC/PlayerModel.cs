using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private bool groundedPlayer;

    public bool GroundedPlayer => groundedPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 movementInput)
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
        rb.velocity = move * playerSpeed;

        if (groundedPlayer)
        {
            rb.AddForce(Vector3.up * gravityValue * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    public void Jump()
    {
        if (groundedPlayer)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravityValue), ForceMode.VelocityChange);
        }
    }
}