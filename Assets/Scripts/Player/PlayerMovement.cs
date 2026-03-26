using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private Vector2 movement;
    private Vector2 lookDirection;

    private void Start()
    {
        lookDirection = Vector2.right;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement != Vector2.zero)
            lookDirection = new Vector2(Mathf.Sign(movement.x), 0);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public Vector2 GetMovement()
    {
        return movement;
    }

    public Vector2 GetLookDirection()
    {
        return lookDirection;
    }
}
