using System.Collections.Generic;
using UnityEngine;

public class CurvedProjectile : MonoBehaviour
{
    public List<Sprite> textBubbles;   // imágenes para la burbuja
    public float destroyTime;

    public float speed = 5f;

    private float amplitude;
    public float minAmplitude = 5f;
    public float maxAmplitude = 8f;

    private float frequency;
    public float minFrequency = 3f;
    public float maxFrequency = 7f;

    private Vector2 direction;
    private float time;

    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (textBubbles.Count > 0)
        {
            int randomTextBubble = Random.Range(0, textBubbles.Count);
            spriteRenderer.sprite = textBubbles[randomTextBubble];
        }

        Destroy(gameObject, destroyTime);

        amplitude = Random.Range(minAmplitude, maxAmplitude);
        frequency = Random.Range(minFrequency, maxFrequency);
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