using Unity.VisualScripting;
using UnityEngine;

public class ShieldBlocking : MonoBehaviour
{
    [SerializeField] PlayerBlock playerBlockScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            if (collision.gameObject.GetComponent<CurvedProjectile>() != null)
            {
                playerBlockScript.ResetBlockCooldown();

                Destroy(collision.gameObject);
            }
        }
    }
}
