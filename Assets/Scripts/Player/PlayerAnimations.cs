using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private PlayerMovement movementScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = movementScript.GetMovement();

        bool isMoving = movement != Vector2.zero;
        animator.SetBool("Moving", isMoving);

        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
