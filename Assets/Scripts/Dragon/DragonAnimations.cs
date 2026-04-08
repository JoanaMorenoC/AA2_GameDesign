using UnityEngine;

public class DragonAnimations : MonoBehaviour
{
    [Header("Animator References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Script References")]
    [SerializeField] private DragonMovement movementScript;
    [SerializeField] private DragonAttackController attackController;
    [SerializeField] private DragonHealthComponent healthComponent;

    [Header("Animation Settings")]
    [SerializeField] private float deathAnimationDuration = 2f;

    private bool isDead = false;
    private bool isAttacking = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (movementScript == null)
            movementScript = GetComponent<DragonMovement>();

        if (attackController == null)
            attackController = GetComponent<DragonAttackController>();

        if (healthComponent == null)
            healthComponent = GetComponent<DragonHealthComponent>();

        if (healthComponent != null)
        {
            healthComponent.onDeath.AddListener(OnDeath);
            healthComponent.onDamageTaken.AddListener(OnDamageTaken);
        }

        SetIdleAnimation();
    }

    void Update()
    {
        if (isDead) return;

        UpdateMovementAnimation();

        UpdateAttackAnimation();
    }

    void UpdateMovementAnimation()
    {
        if (movementScript == null) return;

        bool isMoving = movementScript.enabled && movementScript.IsMoving();

        animator.SetBool("Moving", isMoving);
    }

    void UpdateAttackAnimation()
    {
        if (attackController == null) return;

        if (attackController.IsAttacking() && !isAttacking)
        {
            isAttacking = true;

            if (attackController.IsSpecialAttack())
            {
                OnSpecialAttack();
            }
            else
            {
                OnAttack();
            }
        }
        else if (!attackController.IsAttacking() && isAttacking)
        {
            isAttacking = false;
            animator.SetBool("Attacking", false);
            animator.SetBool("SpecialAttacking", false);
        }
    }

    void SetIdleAnimation()
    {
        animator.SetBool("Moving", false);
        animator.SetBool("Attacking", false);
        animator.SetBool("SpecialAttacking", false);
    }

    public void OnAttack()
    {
        if (isDead) return;

        animator.SetBool("Attacking", true);
        animator.SetTrigger("Attack");

        Invoke(nameof(ResetAttackAnimation), 0.5f);
    }

    public void OnSpecialAttack()
    {
        if (isDead) return;

        animator.SetBool("SpecialAttacking", true);
        animator.SetTrigger("SpecialAttack");

        Invoke(nameof(ResetSpecialAttackAnimation), 0.7f);
    }

    void ResetAttackAnimation()
    {
        if (!isDead)
        {
            animator.SetBool("Attacking", false);
        }
    }

    void ResetSpecialAttackAnimation()
    {
        if (!isDead)
        {
            animator.SetBool("SpecialAttacking", false);
        }
    }

    void OnDamageTaken()
    {
        if (isDead) return;

        animator.SetTrigger("Hurt");
    }

    void OnDeath()
    {
        if (isDead) return;

        isDead = true;

        animator.SetTrigger("Death");
        animator.SetBool("Dead", true);

        if (movementScript != null)
            movementScript.enabled = false;

        if (attackController != null)
            attackController.enabled = false;

        Destroy(gameObject, deathAnimationDuration);
    }

    public void PlayIdleAnimation()
    {
        if (!isDead)
        {
            animator.Play("Idle");
        }
    }

    public void PlayMovementAnimation()
    {
        if (!isDead)
        {
            animator.SetBool("Moving", true);
        }
    }

    public void PlayAttackAnimation()
    {
        if (!isDead)
        {
            OnAttack();
        }
    }

    public void PlaySpecialAttackAnimation()
    {
        if (!isDead)
        {
            OnSpecialAttack();
        }
    }

    public void PlayDeathAnimation()
    {
        OnDeath();
    }

    public void ResetAnimations()
    {
        isDead = false;
        isAttacking = false;
        animator.SetBool("Moving", false);
        animator.SetBool("Attacking", false);
        animator.SetBool("SpecialAttacking", false);
        animator.SetBool("Dead", false);
        animator.Play("Idle");
    }

    void OnDestroy()
    {
        if (healthComponent != null)
        {
            healthComponent.onDeath.RemoveListener(OnDeath);
            healthComponent.onDamageTaken.RemoveListener(OnDamageTaken);
        }
    }
}