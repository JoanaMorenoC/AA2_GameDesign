using System.Collections.Generic;
using UnityEngine;

public class CurvedProjectile : MonoBehaviour
{
    public List<Sprite> textBubbles;
    public float destroyTime;

    public float speed = 5f;
    public float amplitude = 2f;
    public float frequency = 3f;

    private Vector2 direction;
    private float time;

    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        int randomTextBubble = Random.Range(0, textBubbles.Count);

        spriteRenderer.sprite = textBubbles[randomTextBubble];
        Destroy(gameObject, destroyTime);

    }

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        time += Time.deltaTime;

        Vector2 forward = direction * speed * Time.deltaTime;

        Vector2 perpendicular = new Vector2(-direction.y, direction.x);
        Vector2 offset = perpendicular * Mathf.Sin(time * frequency) * amplitude * Time.deltaTime;

        transform.position += (Vector3)(forward + offset);
    }
}
