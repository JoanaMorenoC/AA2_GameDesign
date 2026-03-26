using Unity.VisualScripting;
using UnityEngine;

public class ShieldBlocking : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            if (collision.gameObject.GetComponent<CurvedProjectile>() != null)
            {
                Destroy(collision.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
