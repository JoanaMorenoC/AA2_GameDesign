using UnityEngine;

public class DragonAttackcontroller : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject silencePrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Attack")]
    [SerializeField] private float fireRate = 1f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            timer = 0f;
            ShootStraight();
        }
    }

    void ShootStraight()
    {
        GameObject bubble = Instantiate(silencePrefab, shootPoint.position, Quaternion.identity);

        Vector2 dir = Vector2.left;

        bubble.GetComponent<TextBubbleProjectile>().Initialize(dir);
    }
}
