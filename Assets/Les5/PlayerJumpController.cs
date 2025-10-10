using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerJumpController : MonoBehaviour
{
    private enum PlayerState
    {
        Grounded,
        Jumping
    }

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.2f;

    private PlayerState currentState = PlayerState.Grounded;
    private float startY;
    private float timeInAir;

    void Update()
    {
        switch (currentState)
        {
            case PlayerState.Grounded:
                HandleGroundedState();
                break;

            case PlayerState.Jumping:
                HandleJumpingState();
                break;
        }
    }

    private void HandleGroundedState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = PlayerState.Jumping;
            timeInAir = 0f;
            startY = transform.position.y;
        }
    }

    private void HandleJumpingState()
    {
        timeInAir += Time.deltaTime;

        float verticalOffset = (jumpForce * timeInAir) - (0.5f * gravity * timeInAir * timeInAir);

        Vector3 forwardMove = transform.forward * forwardSpeed * Time.deltaTime;

        Vector3 newPos = transform.position + forwardMove;
        newPos.y = startY + verticalOffset;

        if (Physics.Raycast(transform.position, Vector3.up, 0.5f, groundLayer) && verticalOffset > 0f)
        {
            timeInAir = Mathf.Sqrt(2 * jumpForce / gravity); 
            newPos.y = transform.position.y;
        }

        transform.position = newPos;

        if (IsGrounded() && verticalOffset <= 0f)
        {
            currentState = PlayerState.Grounded;
            Vector3 groundedPos = transform.position;
            groundedPos.y = startY; 
            transform.position = groundedPos;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }
}
