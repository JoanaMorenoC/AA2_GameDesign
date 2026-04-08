using UnityEngine;
using UnityEngine.UI;

public class PlayerBlock : MonoBehaviour
{
    [Header("Shield")]
    [SerializeField] private ShieldSize shield;
    [SerializeField] private Image shieldIndicator;

    [Header("Block Settings")]
    [SerializeField] private float blockDuration = 1.0f;
    [SerializeField] private float blockCooldown = 2.0f;

    private bool isBlocking = false;
    private float blockTimer = 0f;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (isBlocking)
        {
            blockTimer += Time.deltaTime;
            if (blockTimer >= blockDuration)
            {
                EndBlock();
            }
        }

        shieldIndicator.enabled = cooldownTimer <= 0f;

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (BlockButtonPressed() && !isBlocking && cooldownTimer <= 0f)
        {
            StartBlock();
        }
    }

    void StartBlock()
    {
        isBlocking = true;
        blockTimer = 0f;
        shield.gameObject.SetActive(true);
        cooldownTimer = blockCooldown;
    }

    void EndBlock()
    {
        isBlocking = false;
        shield.gameObject.SetActive(false);
        blockTimer = 0f;
    }

    bool BlockButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift);
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    public void ResetBlockCooldown()
    {
        EndBlock();
        cooldownTimer = 0f;
    }
}