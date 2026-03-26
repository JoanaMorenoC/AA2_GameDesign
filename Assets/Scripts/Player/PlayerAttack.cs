using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Attack")]
    [SerializeField] private float fireRate = 0.3f;

    [Header("References")]
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private PlayerAnimations animationsScript;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (ShootButtonPressed() && timer >= fireRate)
        {
            timer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Vector2 dir = movementScript.GetLookDirection();

        proj.GetComponent<TextBubbleProjectile>().Initialize(dir);

        animationsScript.OnAttack();
    }

    bool ShootButtonPressed()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z);
    }
}
