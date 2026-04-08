using System;
using System.Collections;
using UnityEngine;

public class DragonAttackController : MonoBehaviour
{
    public static event Action OnDragonReproche;

    [Header("Projectile")]
    [SerializeField] private GameObject silencePrefab;
    [SerializeField] private GameObject curvedPrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Attack")]
    public float fireRate = 1f;
    public float specialAttackChance = 0.2f;

    [Header("Animation")]
    [SerializeField] private DragonAnimations animationsScript;

    private float timer;
    private bool isAttacking = false;
    private bool isSpecialAttack = false;

    void Start()
    {
        if (animationsScript == null)
            animationsScript = GetComponent<DragonAnimations>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!isAttacking && timer >= fireRate)
        {
            timer = 0f;
            isSpecialAttack = UnityEngine.Random.value < specialAttackChance;

            if (isSpecialAttack)
                StartCoroutine(ShootWithAnimation(ShootCurved, 0.5f));
            else
                StartCoroutine(ShootWithAnimation(ShootStraight, 0.3f));
        }
    }

    IEnumerator ShootWithAnimation(System.Action shootAction, float animationDelay)
    {
        isAttacking = true;

        if (animationsScript != null)
        {
            if (isSpecialAttack)
                animationsScript.OnSpecialAttack();
            else
                animationsScript.OnAttack();
        }

        yield return new WaitForSeconds(animationDelay);

        shootAction();

        if (isSpecialAttack)
            OnDragonReproche?.Invoke();

        isAttacking = false;
    }

    void ShootStraight()
    {
        GameObject bubble = Instantiate(silencePrefab, shootPoint.position, Quaternion.identity);

        Vector2 dir = Vector2.left;

        bubble.GetComponent<TextBubbleProjectile>().Initialize(dir);
        bubble.GetComponent<TextBubbleProjectile>().enemyAtttack = true;
    }

    void ShootCurved()
    {
        GameObject bubble = Instantiate(curvedPrefab, shootPoint.position, Quaternion.identity);

        Vector2 dir = Vector2.left;

        bubble.GetComponent<CurvedProjectile>().Initialize(dir);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsSpecialAttack()
    {
        return isSpecialAttack;
    }
}
