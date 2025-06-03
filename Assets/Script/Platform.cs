using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 velocity = new Vector2(1, 0);

    private float screenLeft;
    private float screenRight;

    private float platformWidth;
    private Vector2 direction = Vector2.right;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 screenBoundsLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 screenBoundsRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        screenLeft = screenBoundsLeft.x;
        screenRight = screenBoundsRight.x;

        platformWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }
    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + direction * velocity * Time.fixedDeltaTime;

        if (newPosition.x - platformWidth <= screenLeft || newPosition.x + platformWidth >= screenRight)
        {
            direction *= -1;
            newPosition = rb.position + direction * velocity * Time.fixedDeltaTime;
        }

        rb.MovePosition(newPosition);
    }
}
