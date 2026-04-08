using UnityEngine;

public class ReprocheColliderArea : MonoBehaviour
{
    [SerializeField] private PlayerHealthComponent healthComponent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Reproche"))
        {
            healthComponent.TakeDamage(5f);
            Destroy(other.gameObject);
        }
    }
}
