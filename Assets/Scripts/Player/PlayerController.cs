using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;           // How fast the player moves
    private Rigidbody2D rb;
    private InputReader input;         // InputReader handles WASD / keyboard
    public JoystickUI joystick;        // Drag JoystickBG (with JoystickUI) into this

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputReader>(); // Grab InputReader from same GameObject
    }

    void FixedUpdate()
    {
        // Keyboard input from InputReader
        Vector2 moveInput = input != null ? input.Move : Vector2.zero;

        // Joystick input from JoystickUI
        if (joystick != null && joystick.Value != Vector2.zero)
        {
            moveInput = joystick.Value;  // joystick takes priority if used
        }

        // Normalize so speed is consistent across input types
        if (moveInput.sqrMagnitude > 1f) 
            moveInput.Normalize();

        // Apply movement
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    // Optional property if you still want external access
    public Vector2 CurrentMoveInput =>
        (joystick != null && joystick.Value != Vector2.zero) 
        ? joystick.Value 
        : (input != null ? input.Move : Vector2.zero);
}
