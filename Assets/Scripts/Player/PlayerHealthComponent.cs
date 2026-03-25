using UnityEngine;

public class PlayerHealthComponent : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Heartbeat")]
    [SerializeField] private Heartbeat heartbeat;
    [SerializeField] private float heartRateIncreaseOnHit = 20f;
    [SerializeField] private float heartRateRecoverySpeed = 10f;

    private float lastHitTime;
    [SerializeField] private float recoveryDelay = 2f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        RecoverHeartRate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("EnemyAttack"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (heartbeat != null)
        {
            heartbeat.heartRate += heartRateIncreaseOnHit;
        }

        lastHitTime = Time.time;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void RecoverHeartRate()
    {
        if (heartbeat == null) return;

        if (Time.time < lastHitTime + recoveryDelay)
            return;

        heartbeat.heartRate = Mathf.MoveTowards(
            heartbeat.heartRate,
            heartbeat.minHeartRate,
            heartRateRecoverySpeed * Time.deltaTime
        );
    }

    void Die()
    {
        Debug.Log("Player muerto");
    }
}
