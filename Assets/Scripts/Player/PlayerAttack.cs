using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Attack")]
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private float attackDelay = 0.1f;

    [Header("References")]
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private PlayerAnimations animationsScript;

    private float timer;
    private bool isAttacking = false;
    private Coroutine attackCoroutine;

    void Update()
    {
        timer += Time.deltaTime;

        if (ShootButtonPressed() && timer >= fireRate && attackCoroutine == null)
        {
            timer = 0f;
            attackCoroutine = StartCoroutine(ShootWithDelay());
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector2 dir = movementScript.GetLookDirection();

        proj.GetComponent<TextBubbleProjectile>().Initialize(dir);
    }

    IEnumerator ShootWithDelay()
    {
        isAttacking = true;
        animationsScript.OnAttack();

        yield return new WaitForSeconds(attackDelay);

        Shoot();
        isAttacking = false;
        attackCoroutine = null;
    }

    bool ShootButtonPressed()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
