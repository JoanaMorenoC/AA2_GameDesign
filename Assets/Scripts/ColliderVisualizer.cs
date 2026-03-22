using UnityEngine;

public class ColliderVisualizer : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Transform visual;

    void Update()
    {
        visual.localScale = new Vector3(
            boxCollider.size.x,
            boxCollider.size.y,
            1f
        );
    }
}