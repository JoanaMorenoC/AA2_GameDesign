using UnityEngine;
using UnityEngine.Events;

public class DragonHealthComponent : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Phase Settings")]
    [SerializeField] private bool usePhaseSystem = true;
    [SerializeField] private DragonFases dragonFases;
    [SerializeField] private float[] phaseThresholds = new float[] { 0.66f, 0.33f };

    [Header("Events")]
    public UnityEvent onDamageTaken;
    public UnityEvent onHealthChanged;
    public UnityEvent onDeath;
    public UnityEvent<float> onHealthPercentageChanged;

    [Header("Debug")]
    [SerializeField] private bool showDebugLogs = true;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (usePhaseSystem && dragonFases == null)
        {
            dragonFases = GetComponent<DragonFases>();
            if (dragonFases == null && showDebugLogs)
                Debug.LogWarning("Health: DragonFases component not found. Phase system disabled.");
        }

        onHealthChanged?.Invoke();
        onHealthPercentageChanged?.Invoke(GetHealthPercentage());
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (showDebugLogs)
            Debug.Log($"Health: Took {damage} damage. Current health: {currentHealth}/{maxHealth}");

        onDamageTaken?.Invoke();
        onHealthChanged?.Invoke();

        float healthPercentage = GetHealthPercentage();
        onHealthPercentageChanged?.Invoke(healthPercentage);

        if (usePhaseSystem && dragonFases != null)
        {
            CheckPhaseTransition(healthPercentage);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (showDebugLogs)
            Debug.Log($"Health: Healed {amount} health. Current health: {currentHealth}/{maxHealth}");

        onHealthChanged?.Invoke();
        onHealthPercentageChanged?.Invoke(GetHealthPercentage());
    }

    private void CheckPhaseTransition(float healthPercentage)
    {
        int targetPhase = GetPhaseByHealthPercentage(healthPercentage);

        if (targetPhase > dragonFases.currentPhase)
        {
            if (showDebugLogs)
                Debug.Log($"Health: Health at {healthPercentage * 100}% - Advancing to phase {targetPhase + 1}");

            dragonFases.NextPhase();
        }
    }

    private int GetPhaseByHealthPercentage(float healthPercentage)
    {
        if (healthPercentage > phaseThresholds[0])
            return 0;
        else if (healthPercentage > phaseThresholds[1])
            return 1;
        else
            return 2;
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        if (showDebugLogs)
            Debug.Log("Health: Dragon has died!");

        onDeath?.Invoke();

        DragonMovement movement = GetComponent<DragonMovement>();
        if (movement != null)
            movement.enabled = false;

        DragonAttackController attack = GetComponent<DragonAttackController>();
        if (attack != null)
            attack.enabled = false;
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public float GetHealthPercentage() => currentHealth / maxHealth;
    public bool IsDead() => isDead;

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onHealthChanged?.Invoke();
        onHealthPercentageChanged?.Invoke(GetHealthPercentage());
    }

    public void SetHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        onHealthChanged?.Invoke();
        onHealthPercentageChanged?.Invoke(GetHealthPercentage());

        if (currentHealth <= 0)
            Die();
    }

    public void ResetHealth()
    {
        isDead = false;
        currentHealth = maxHealth;

        if (showDebugLogs)
            Debug.Log("Health: Health has been reset!");

        onHealthChanged?.Invoke();
        onHealthPercentageChanged?.Invoke(GetHealthPercentage());

        DragonMovement movement = GetComponent<DragonMovement>();
        if (movement != null)
            movement.enabled = true;

        DragonAttackController attack = GetComponent<DragonAttackController>();
        if (attack != null)
            attack.enabled = true;
    }

    public void SyncWithPhaseSystem()
    {
        if (dragonFases != null)
        {
            float healthPercentage = GetHealthPercentage();
            int expectedPhase = GetPhaseByHealthPercentage(healthPercentage);

            if (expectedPhase != dragonFases.currentPhase)
            {
                dragonFases.InitializePhase(expectedPhase);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal(10f);
        }
    }
}