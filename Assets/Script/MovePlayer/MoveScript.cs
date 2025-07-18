using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveScript : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float gravity = -9.81f;

    [Header("References")]
    [SerializeField] private Animator animator;

    private CharacterController controller;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controller.stepOffset = 0.4f; // Giúp leo bậc gạch mượt hơn
    }

    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Lấy hướng từ joystick
        Vector3 input = JoystickControl.direct;
        Vector3 moveDirection = new Vector3(input.x, 0f, input.z).normalized;

        // Di chuyển theo hướng joystick
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Xoay nhân vật theo hướng di chuyển
        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle + 180f, 0f); // giống script 1

            if (animator) animator.SetBool("Running", true);
        }
        else
        {
            if (animator) animator.SetBool("Running", false);
        }

        // Gravity thủ công
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // giữ nhân vật dính mặt đất
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}