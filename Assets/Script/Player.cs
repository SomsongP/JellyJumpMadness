using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float gravity = 9.81f;
    public float jumpForce = 5f;

    private Vector2 velocity;
    private bool isJumping = false;
    private Rigidbody2D rb;
    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        InputManager.instance.OnTouchDown += Jump;
    }
    private void OnDisable()
    {
        InputManager.instance.OnTouchDown -= Jump;
    }
    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            if (velocity.y < 0f)
                velocity.y = 0f;
        }
        else
        {
            velocity.y -= gravity * Time.fixedDeltaTime;
        }


        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

    }

    public void Jump(Vector2 screenPosition, float time)
    {
        Debug.Log("Jump");
        if (IsGrounded())
        {
            velocity.y = jumpForce;
            Debug.Log("jumping");
        }
        
    }
    private bool IsGrounded()
    {
        float rayLength = 0.1f;
        Vector2 origin = transform.position + Vector3.down * 0.15f;
        LayerMask groundLayer = LayerMask.GetMask("platform");

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);
        Debug.DrawRay(origin, Vector2.down * rayLength, Color.red);
        return hit.collider != null;
    }

}
