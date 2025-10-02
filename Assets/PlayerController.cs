using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;       // How fast the player moves
    private Rigidbody2D rb;        // We'll use this to move the player
    private Vector2 moveInput;     // Stores WASD/arrow key input

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody on this object
    }

    void Update()
    {
        // Get input from keyboard
        moveInput.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        moveInput.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
    }

    void FixedUpdate()
    {
        // Move the player each physics step
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    public Vector2 CurrentMoveInput => moveInput;
}
