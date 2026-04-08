using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthComponent : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 5;
    private float currentHealth;
    [SerializeField] private Image healthBar;

    [Header("Heartbeat")]
    [SerializeField] private Heartbeat heartbeat;
    [SerializeField] private float heartRateIncreaseOnHit = 20f;
    [SerializeField] private float heartRateRecoverySpeed = 10f;

    private float lastHitTime;
    [SerializeField] private float recoveryDelay = 2f;
    [SerializeField] private SceneController sceneController;


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
            TakeDamage(2f);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / 100f;

        if (heartbeat != null)
        {
            heartbeat.heartRate += heartRateIncreaseOnHit;
        }

        lastHitTime = Time.time;

        SFXManager.Instance.PlayGlobalSound("PlayerHurt", 0.2f);

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
        sceneController.GoToGameOver();
    }
}
