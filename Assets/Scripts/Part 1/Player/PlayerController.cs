using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private InputMaster controls;

    private Vector2 direction;
    public float runSpeed = 20.0f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        // Instantiate the InputMaster
        controls = new InputMaster();

        // Link the Movement action to the Move method
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += ctx => Move(Vector2.zero); // Stop movement when input is canceled
    }

    void Move(Vector2 direction)
    {
        this.direction = direction;
    }

    void FixedUpdate()
    {
        // Apply movement
        body.linearVelocity = direction * runSpeed;
    }

    private void OnEnable()
    {
        // Enable input system
        controls.Enable();
    }

    private void OnDisable()
    {
        // Disable input system
        controls.Disable();
    }
}
