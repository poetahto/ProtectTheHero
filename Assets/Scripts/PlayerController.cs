using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float deceleration;
    public Rigidbody2D body;
    public Vector2 inputDirection;

    public void InputMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector2 target = inputDirection * speed;
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
