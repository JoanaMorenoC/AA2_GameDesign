using UnityEngine;

public class DragonAttackController : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject silencePrefab;
    [SerializeField] private GameObject curvedPrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Attack")]
    public float fireRate = 1f;
    public float specialAttackChance = 0.2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            timer = 0f;
            if (Random.value < specialAttackChance)
                ShootCurved();
            else
                ShootStraight();
        }
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
}
