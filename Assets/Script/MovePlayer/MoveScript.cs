using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 6f;

    private void FixedUpdate()
    {
        HandleJoystickMovement();
    }

    void HandleJoystickMovement()
    {
        Vector3 direction = JoystickControl.direct;
        direction = new Vector3(direction.x, 0f, direction.z).normalized;

        controller.Move(direction * moveSpeed * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Running", true);
           
            //Rotate player
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle + 180f, 0f);
            Debug.Log("Running = true");
        }
        else
        {
            animator.SetBool("Running", false);
            Debug.Log("Running = false");
        }
    }
}
