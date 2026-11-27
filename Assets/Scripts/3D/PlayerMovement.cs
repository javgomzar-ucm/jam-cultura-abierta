using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento (Ejes X y Z)")]
    [SerializeField, Range(1, 10)] private float moveSpeed = 5f;
    [SerializeField] private bool rotateTowardsMovement = true;
    [SerializeField, Range(1, 30)] private float turnSpeed = 12f;

    private float runSpeed => moveSpeed * 2f;
    private float speedMultiplier = 1.0f;

    [Header("Gravedad (Sin salto)")]
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float terminalVelocity = 30f;
    private float verticalVelocity;

    [Header("Cámara Solo X y Z")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector2 cameraOffsetXZ = new Vector2(0f, -6f);
    [SerializeField, Range(1, 20)] private float cameraFollowSmooth = 10f;
    [SerializeField] private bool lockCursor = false;

    private CharacterController _characterController;
    private float _fixedCameraY;

    private void Start()
    {
        if (lockCursor)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }

        _characterController = GetComponent<CharacterController>();

        if (cameraTransform != null)
        {
            _fixedCameraY = cameraTransform.position.y;
            cameraTransform.position = new Vector3(
                transform.position.x + cameraOffsetXZ.x,
                _fixedCameraY,
                transform.position.z + cameraOffsetXZ.y
            );
        }
    }

    private void Update()
    {
        if (GameManager.IsPaused) return;

        HandleVerticalGrounded();
        HandleMovementXZ();
        HandleTopDownCamera();
    }

    private void HandleVerticalGrounded()
    {
        if (!_characterController.isGrounded)
        {
            ApplyGravity();
        }
        else
        {
            verticalVelocity = -1f;
        }
    }

    private void HandleMovementXZ()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Eje X
        float vertical = Input.GetAxis("Vertical");     // Eje Z

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical);
        if (movementDirection.sqrMagnitude > 1f)
        {
            movementDirection.Normalize();
        }

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = (isRunning ? runSpeed : moveSpeed) * speedMultiplier;

        Vector3 velocity = movementDirection * currentSpeed + Vector3.up * verticalVelocity;
        _characterController.Move(velocity * Time.deltaTime);

        if (rotateTowardsMovement && movementDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void HandleTopDownCamera()
    {
        if (cameraTransform == null) return;

        Vector3 desiredPosition = new Vector3(
            transform.position.x + cameraOffsetXZ.x,
            _fixedCameraY,
            transform.position.z + cameraOffsetXZ.y
        );

        cameraTransform.position = Vector3.Lerp(
            cameraTransform.position,
            desiredPosition,
            cameraFollowSmooth * Time.deltaTime
        );
    }

    private void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity <= -terminalVelocity)
        {
            verticalVelocity = -terminalVelocity;
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void ResetSpeed()
    {
        speedMultiplier = 1.0f;
    }
}