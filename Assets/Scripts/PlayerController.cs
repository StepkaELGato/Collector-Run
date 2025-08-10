using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public InputActionReference jumpAction;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public float mouseSensitivity = 0.1f;
    public Transform cameraTransform;

    [Header("References")]
    [SerializeField] private CharacterController characterController;

    private Vector3 velocity;
    public bool isGrounded;
    private float pitch = 0f;

    private void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
        jumpAction.action.Disable();
    }

    void Start()
    {
        if (!cameraTransform)
            Debug.LogWarning("Camera Transform is not assigned.");

        characterController.Move(Vector3.down * 0.1f);
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 2f)
            velocity.y = 0f;

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = transform.TransformDirection(move);
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (jumpAction.action.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void ResetPlayer()
    {
        Debug.Log("ResetPlayer called");

        characterController.enabled = false;

        transform.position = new Vector3(15f, 0f, 15f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        velocity = Vector3.zero;
        pitch = 0f;

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        characterController.enabled = true;
    }
}
