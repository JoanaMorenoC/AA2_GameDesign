using UnityEngine;

public class DragonFases : MonoBehaviour
{
    [System.Serializable]
    public class Fase
    {
        public string name = "Fase 1";
        public float speed = 3f;
        public float fireRate = 1f;
        public float specialAttackChance = 0.2f;
    }

    [Header("Configuración de Fases")]
    public Fase[] phases = new Fase[3];

    [Header("References")]
    public DragonMovement dragonMovement;
    public DragonAttackController dragonAttackController;

    [Header("Debug")]
    public int currentPhase = 0;
    public bool autoStart = true;

    private bool phasesCompleted = false;

    void Start()
    {
        if (dragonMovement == null)
            dragonMovement = GetComponent<DragonMovement>();

        if (dragonAttackController == null)
            dragonAttackController = GetComponent<DragonAttackController>();

        if (dragonMovement == null || dragonAttackController == null) return;

        if (autoStart)
        {
            InitializePhase(0);
        }
    }

    public void InitializePhase(int phaseIndex)
    {
        if (phaseIndex >= phases.Length)
        {
            Debug.Log("DragonFases: ¡Todas las fases completadas!");
            phasesCompleted = true;
            return;
        }

        currentPhase = phaseIndex;
        Fase phase = phases[currentPhase];

        Debug.Log($"DragonFases: Iniciando {phase.name} - Speed: {phase.speed}, FireRate: {phase.fireRate}, SpecialChance: {phase.specialAttackChance}");

        dragonMovement.speed = phase.speed;
        dragonAttackController.fireRate = phase.fireRate;
        dragonAttackController.specialAttackChance = phase.specialAttackChance;
    }

    public void NextPhase()
    {
        if (phasesCompleted)
        {
            Debug.Log("DragonFases: Ya se completaron todas las fases");
            return;
        }
        int nextPhase = currentPhase + 1;

        if (nextPhase < phases.Length)
        {
            InitializePhase(nextPhase);
            SFXManager.Instance.PlayGlobalSound("Roar", 0.5f);
        }
        else
        {
            Debug.Log("DragonFases: ¡Felicidades! Has completado todas las fases del dragón");
            phasesCompleted = true;
        }
    }

    public void VerifyPhaseChange(float currentHealth, float maxHealth, float phaseThreshold)
    {
        if (phasesCompleted) return;

        float healthPercentage = currentHealth / maxHealth;
        int expectedPhase = CalculatePhaseByHealth(healthPercentage);

        if (expectedPhase > currentPhase)
        {
            NextPhase();
        }
    }

    int CalculatePhaseByHealth(float healthPercentage)
    {
        if (healthPercentage > 0.66f)
            return 0;
        else if (healthPercentage > 0.33f)
            return 1;
        else
            return 2;
    }

    // Public methods to manually change stats
    public void ChangeSpeed(float newSpeed)
    {
        if (dragonMovement != null)
            dragonMovement.speed = newSpeed;
    }

    public void ChangeFireRate(float newFireRate)
    {
        if (dragonAttackController != null)
            dragonAttackController.fireRate = newFireRate;
    }

    public void ChangeSpecialChance(float newChance)
    {
        if (dragonAttackController != null)
            dragonAttackController.specialAttackChance = newChance;
    }

    // Method to get current phase information
    public string GetCurrentPhaseInfo()
    {
        if (currentPhase >= phases.Length)
            return "All phases completed";

        Fase phase = phases[currentPhase];
        return $"Phase {currentPhase + 1}: {phase.name}\n" +
               $"Speed: {phase.speed}\n" +
               $"Fire Rate: {phase.fireRate}\n" +
               $"Special Chance: {phase.specialAttackChance * 100}%";
    }

    void Update()
    {
        // Debug: Key to manually advance phases (for testing only)
        if (Input.GetKeyDown(KeyCode.F))
        {
            NextPhase();
            Debug.Log(GetCurrentPhaseInfo());
        }
    }
}
