using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float throwSpeed = 15;
    public float sprintSpeed;
    public float acceleration;
    public float deceleration;
    public Rigidbody2D body;
    public new Collider2D collider;
    public Vector2 inputDirection;
    public bool isSprinting;

    private ItemController _heldItem;

    public void InputMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void InputSprint(InputAction.CallbackContext context)
    {
        // 1 = button held, 0 = button not held
        isSprinting = context.ReadValue<float>() != 0;
    }

    public void InputGrab(InputAction.CallbackContext context)
    {
        if (context.started && ItemController.InPickupRange(transform.position, out ItemController nearest))
        {
            nearest.PickUp(gameObject);
        }
    }

    public void InputThrow(InputAction.CallbackContext context)
    {
        if (context.started && ItemController.InPickupRange(transform.position, out ItemController nearest) && nearest.carriedState.Holder == gameObject)
        {
            nearest.Throw(collider, inputDirection * throwSpeed);
        }
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
