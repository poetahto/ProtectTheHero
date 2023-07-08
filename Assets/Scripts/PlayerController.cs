using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float acceleration;
    public float deceleration;
    public Rigidbody2D body;
    public Vector2 inputDirection;
    public bool isSprinting;

    public void InputMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void InputSprint(InputAction.CallbackContext context)
    {
        // 1 = button held, 0 = button not held
        isSprinting = context.ReadValue<float>() != 0;
    }

    private void Update()
    {
        Vector2 target = inputDirection * (isSprinting ? sprintSpeed : speed);
        Vector2 current = body.velocity;

        if (inputDirection != Vector2.zero)
        {
            // we are accelerating
            float t = acceleration * Time.deltaTime;
            current = Vector2.MoveTowards(current, target, t);
        }
        else
        {
            // we are decelerating
            float t = deceleration * Time.deltaTime;
            current = Vector2.Lerp(current, Vector2.zero, t);
        }

        body.velocity = current;
    }
}
