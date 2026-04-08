using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldBlocking : MonoBehaviour
{
    public static event Action OnPlayerBlockReproche;

    [SerializeField] PlayerBlock playerBlockScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Reproche"))
        {
            if (collision.gameObject.GetComponent<CurvedProjectile>() != null)
            {
                playerBlockScript.ResetBlockCooldown();
                SFXManager.Instance.PlayGlobalSound("BreakReproche", 1f);

                OnPlayerBlockReproche?.Invoke();

                Destroy(collision.gameObject);
            }
        }
    }
}
