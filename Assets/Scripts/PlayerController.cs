using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public AudioClip throwAudio;
    public AudioClip grabAudio;
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
    private bool _grabLock;
    private bool _hasItem;

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
        if (!_hasItem && context.started && ItemController.InPickupRange(transform.position, out ItemController nearest) && nearest.TryPickUp(gameObject))
        {
            AudioSource.PlayClipAtPoint(grabAudio, transform.position);
            _grabLock = true;
            _hasItem = true;
        }
    }

    public void InputThrow(InputAction.CallbackContext context)
    {
        if (_hasItem && !_grabLock && context.started && ItemController.InPickupRange(transform.position, out ItemController nearest) && nearest.TryThrow(collider, inputDirection * throwSpeed))
        {
            // some vfx for throwing
            AudioSource.PlayClipAtPoint(throwAudio, transform.position);
            _hasItem = false;
        }
    }

    private void Update()
    {
        _grabLock = false;
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
