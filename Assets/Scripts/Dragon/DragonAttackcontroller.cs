using UnityEngine;

public class DragonAttackcontroller : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject silencePrefab;
    [SerializeField] private GameObject curvedPrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Attack")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float specialAttackChance = 0.2f;

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
    }

    void ShootCurved()
    {
        GameObject bubble = Instantiate(curvedPrefab, shootPoint.position, Quaternion.identity);

        Vector2 dir = Vector2.left;

        bubble.GetComponent<CurvedProjectile>().Initialize(dir);
    }
}
